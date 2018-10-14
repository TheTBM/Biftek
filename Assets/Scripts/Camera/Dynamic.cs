using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic : MonoBehaviour
{
	public GameObject[] players = new GameObject[4];

	//public GameObject player1;
	//public GameObject player2;

	int numAlive;
	Vector3 position;
	Vector3 prevPosition;
    Vector3 avgPos;
	float magnitude;
	float prevMagnitude;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		numAlive = 0;
		prevPosition = position;
		position.Set(0, 0, 0);
        avgPos.Set(0, 0, 0);
		
		for (int i = 0; i < 4; i++)
		{
			int temp = i + 1;
			players[i] = GameObject.FindGameObjectWithTag(temp.ToString());

			if (players[i].GetComponent<Player>().isAlive())
			{
				avgPos += players[i].transform.position;
                position = players[i].transform.position;
				numAlive++;
			}
		}
		
		if (numAlive > 0)
		{
			avgPos /= numAlive;
		}

		prevMagnitude = magnitude;
		magnitude = 0.0f;
		
		if (numAlive > 1)
		{
			for (int i = 0; i < 4; i++)
			{
				for (int j = i + 1; j < 4; j++)
				{
					if (players[i].GetComponent<Player>().isAlive() && players[j].GetComponent<Player>().isAlive())
					{
						float temp = Mathf.Abs((players[i].transform.position - players[j].transform.position).magnitude);
						if (temp > magnitude)
						{
							magnitude = temp;
							position = (players[i].transform.position + players[j].transform.position) / 2;
						}
					}
				}
			}
		}
		else
		{
			magnitude = 15.0f;
		}

        position = (avgPos * 0.1f + position * 0.9f);

		position = (prevPosition * 0.98f) + (0.02f * position);
		magnitude = (prevMagnitude * 0.98f) + (0.02f * magnitude);
		transform.SetPositionAndRotation(new Vector3(position.x, position.y, position.z - magnitude * 0.15f), transform.rotation);
		transform.Translate(-Vector3.forward * (magnitude * 0.7f + 5));
	}
}