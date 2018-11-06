using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoundEnginePluginWrapper;

public class SpawnManager : MonoBehaviour
{
    public enum GameState
    {
        stock,
        score,
        winscreen
    }

    public GameState gameState = GameState.stock;

	public GameObject[] spawns = new GameObject[4];
	
    GameObject[] playerCopies = new GameObject[4];

	Player[] players = new Player[4];

    int numAlive;

    public GameObject Player;

	public Material[] playerMaterials = new Material[4];

    bool initiated = false;

	public ParticleSystem spawnEmitter;

	private ParticleSystem[] seCopies = new ParticleSystem[4];

	// Use this for initialization
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!initiated)
        {
            Initiate();
        }

        numAlive = 4;

		for (int i = 0; i < 4; i++)
		{
			if (players[i].GetComponent<Info>().isDead())
			{
				numAlive -= 1;
			}
		}

		for (int i = 0; i < 4; i++)
		{
			if (!players[i].isAlive() && !players[i].GetComponent<Info>().isDead())
			{
				players[i].decreaseRespawnTimer(Time.deltaTime);

				if (players[i].getRespawnTimer() <= 0.0f)
				{
					players[i].respawn(playerMaterials[i], spawns[i]);
					seCopies[i] = Instantiate(spawnEmitter, spawns[i].transform.position, spawns[i].transform.rotation) as ParticleSystem;
					seCopies[i].GetComponent<GeneralEmitter>().KillEmitter();
				}
			}
		}

        if (numAlive <= 1)
        {
            gameState = GameState.winscreen;
            GameObject.Find("WinScreen").GetComponent<Text>().text = "Congratulations! You Win!";
        }
        else
        {
            gameState = GameState.stock;
            GameObject.Find("WinScreen").GetComponent<Text>().text = " ";
        }

        for (int i = 0; i < 4; i++)
        {
            int temp = i + 1;
            playerCopies[i] = GameObject.FindGameObjectWithTag(temp.ToString());
            GameObject.Find("Player" + temp + "UI").GetComponentInChildren<Text>().text = players[i].GetComponent<Player>().getPoints().ToString();
		}
    }

    void Initiate()
    {
		for (int i = 0; i < 4; i++)
		{
			playerCopies[i] = Instantiate(Player, spawns[i].transform.position, spawns[i].transform.rotation) as GameObject;
			playerCopies[i].tag = (i + 1).ToString();
			playerCopies[i].GetComponent<Renderer>().material = playerMaterials[i];
			players[i] = playerCopies[i].GetComponent<Player>();
		}

        initiated = true;
    }
}