using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	const int maxPlayers = 2;
	int[] scores;
	int currentPlayers;
	public Player player1;
	public Player player2;

	public Player[] players;

	// Use this for initialization
	void Start ()
	{
		players = new Player[2];
		players[0] = player1;
		players[1] = player2;

		scores = new int[2];
		scores[0] = 0;
		scores[1] = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < players.GetLength(0) - 1; i++)
		{
			if (players[i].health <= 0)
			{
				Destroy(players[i]);
			}
		}
	}
}