using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic : MonoBehaviour
{
	GameObject[] players;
	public SpawnManager spawnManager;
	int numAlive;
	bool[] alive;

	// Use this for initialization
	void Start()
	{
		alive = new bool[4];
	}

	// Update is called once per frame
	void Update()
	{
		alive = spawnManager.getPlayersAlive();
		
		numAlive = 0;

		Vector3 position = new Vector3(0,0,0);

		for (int i = 0; i < 4; i++)
		{
			if (alive[i] == true)
			{
				int temp = i + 1;
				Debug.Log(temp);
				players[i] = GameObject.FindGameObjectWithTag((temp).ToString());
				//Debug.Log(temp.ToString());
				position += players[i].transform.position;
				numAlive++;
			}
		}

		if (numAlive > 0)
		{
			position /= numAlive;
		}

		transform.SetPositionAndRotation(position, transform.rotation);

		float magnitude = 0.0f;

		if (numAlive > 1)
		{
			for (int i = 0; i < 4; i++)
			{
				for (int j = i + 1; j < 4; j++)
				{
					if (alive[i] && alive[j])
					{
						float temp = Mathf.Abs((players[i].transform.position - players[j].transform.position).magnitude);
						if (temp > magnitude);
						{
							magnitude = temp;
						}
					}
				}
			}
		}
		else
		{
			magnitude = 10.0f;
		}
		transform.Translate(-Vector3.forward * (magnitude + 5));
	}
}
