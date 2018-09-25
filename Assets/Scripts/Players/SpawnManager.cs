using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public GameObject Spawn1;
	public GameObject Spawn2;
	public GameObject Spawn3;
	public GameObject Spawn4;

	GameObject player1Copy;
	GameObject player2Copy;
	public GameObject Player;

	public Material player1Material;
	public Material player2Material;
	public Material player3Material;
	public Material player4Material;

	bool[] playerAlive;

	int[] scores;


	// Use this for initialization
	void Start ()
	{
		playerAlive = new bool[4];
		scores = new int[4];
		for (int i = 0; i < 4; i++)
		{
			scores[i] = 0;
			playerAlive[i] = false;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (playerAlive[0] == false)
		{
			player1Copy = Instantiate(Player, Spawn1.transform.position, Spawn1.transform.rotation) as GameObject;
			player1Copy.tag = "1";
			player1Copy.GetComponent<Renderer>().material = player1Material;
			playerAlive[0] = true;
		}

		if (playerAlive[1] == false)
		{
			player2Copy = Instantiate(Player, Spawn2.transform.position, Spawn2.transform.rotation) as GameObject;
			player2Copy.tag = "2";
			player2Copy.GetComponent<Renderer>().material = player2Material;
			playerAlive[1] = true;
		}
	}

	public void playerDead(int player)
	{
		playerAlive[player] = false;
	}

	public bool[] getPlayersAlive()
	{
		return playerAlive;
	}
}
