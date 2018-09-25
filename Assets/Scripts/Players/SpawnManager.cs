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

	Player player1;
	Player player2;

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
		player1Copy = Instantiate(Player, Spawn1.transform.position, Spawn1.transform.rotation) as GameObject;
		player1Copy.tag = "1";
		player1Copy.GetComponent<Renderer>().material = player1Material;
		player1 = player1Copy.GetComponent<Player>();

		player2Copy = Instantiate(Player, Spawn2.transform.position, Spawn2.transform.rotation) as GameObject;
		player2Copy.tag = "2";
		player2Copy.GetComponent<Renderer>().material = player2Material;
		player2 = player2Copy.GetComponent<Player>();
	}

	// Update is called once per frame
	void Update ()
	{
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
				player2.respawn(player1Material, Spawn1);
			}
		}
	}
}
