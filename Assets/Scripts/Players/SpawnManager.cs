using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
	public GameObject Spawn1;
	public GameObject Spawn2;
	public GameObject Spawn3;
	public GameObject Spawn4;

	GameObject player1Copy;
	GameObject player2Copy;
	GameObject player3Copy;
	GameObject player4Copy;

	public GameObject[] players = new GameObject[4];

	Player player1;
	Player player2;
	Player player3;
	Player player4;

	public GameObject Player;

	public Material player1Material;
	public Material player2Material;
	public Material player3Material;
	public Material player4Material;

	bool initiated = false;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if (!initiated)
		{
			Initiate();
		}

		if (!player1.isAlive())
		{
			player1.decreaseRespawnTimer(Time.deltaTime);

			if (player1.getRespawnTimer() <= 0.0f)
			{
				player1.respawn(player1Material, Spawn1);
			}
		}

		if (!player2.isAlive())
		{
			player2.decreaseRespawnTimer(Time.deltaTime);

			if (player2.getRespawnTimer() <= 0.0f)
			{
				player2.respawn(player2Material, Spawn2);
			}
		}

		if (!player3.isAlive())
		{
			player3.decreaseRespawnTimer(Time.deltaTime);
		
			if (player3.getRespawnTimer() <= 0.0f)
			{
				player3.respawn(player3Material, Spawn3);
			}
		}
		
		if (!player4.isAlive())
		{
			player4.decreaseRespawnTimer(Time.deltaTime);
		
			if (player4.getRespawnTimer() <= 0.0f)
			{
				player4.respawn(player4Material, Spawn4);
			}
		}

		for (int i = 0; i < 4; i++)
		{
			int temp = i + 1;
			players[i] = GameObject.FindGameObjectWithTag(temp.ToString());
			GameObject.Find("player" + temp.ToString() + "lives").GetComponent<Text>().text = players[i].GetComponent<Player>().getPoints().ToString();
		}
	}

	void Initiate()
	{
		player1Copy = Instantiate(Player, Spawn1.transform.position, Spawn1.transform.rotation) as GameObject;
		player1Copy.tag = "1";
		player1Copy.GetComponent<Renderer>().material = player1Material;
		player1 = player1Copy.GetComponent<Player>();

		player2Copy = Instantiate(Player, Spawn2.transform.position, Spawn2.transform.rotation) as GameObject;
		player2Copy.tag = "2";
		player2Copy.GetComponent<Renderer>().material = player2Material;
		player2 = player2Copy.GetComponent<Player>();

		player3Copy = Instantiate(Player, Spawn3.transform.position, Spawn3.transform.rotation) as GameObject;
		player3Copy.tag = "3";
		player3Copy.GetComponent<Renderer>().material = player3Material;
		player3 = player3Copy.GetComponent<Player>();

		player4Copy = Instantiate(Player, Spawn4.transform.position, Spawn4.transform.rotation) as GameObject;
		player4Copy.tag = "4";
		player4Copy.GetComponent<Renderer>().material = player4Material;
		player4 = player4Copy.GetComponent<Player>();

		initiated = true;
	}
}
