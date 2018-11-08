using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoundEnginePluginWrapper;

namespace Events
{
	//base class for each event
	public abstract class CustomEvent : MonoBehaviour
	{
		public abstract void initiate();

		public abstract void update();

		public abstract void displayEvent();

		public abstract void displayResults();

		public abstract void cleanup();

		public abstract bool finished();
	}

	public class KingOfTheHill : CustomEvent
	{
		GameObject zone;
		GameObject copy;
		Material dMaterial;
		int playerInZone;

		float[] scores = new float[4];
		GameObject[] players = new GameObject[4];

		List<GameObject> playersInZone = new List<GameObject>();

		public KingOfTheHill(GameObject gameObject)
		{
			zone = gameObject;
		}

		public override void initiate()
		{
			copy = Instantiate(zone, new Vector3(0, 0.1f, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
			dMaterial = copy.GetComponent<Renderer>().material;

			for (int i = 0; i < 4; i++)
			{
				players[i] = GameObject.FindGameObjectWithTag((i + 1).ToString());
			}

			if (!SoundEngineWrapper.IsPlaying(13))
			{
				SoundEngineWrapper.QueueSound("event_start", 0, false, 13);
			}
		}

		public override void update()
		{
			if (!finished())
			{
				for (int i = 0; i < 4; i++)
				{
					if (players[i].GetComponent<Player>().isAlive())
					{
						Vector2 playerPosition = new Vector2(0, 0);
						Vector2 zonePosition = new Vector2(0, 0);
						Vector2 difference = new Vector2(0, 0);

						playerPosition.Set(players[i].transform.position.x, players[i].transform.position.z);
						zonePosition.Set(zone.transform.position.x, zone.transform.position.z);
						difference = playerPosition - zonePosition;

						if (difference.magnitude < (zone.transform.localScale.x / 2))
						{
							playersInZone.Add(players[i]);
							playerInZone = i;
						}
					}
				}

				if (playersInZone.Count == 1)
				{
					Color color = playersInZone[0].GetComponent<Renderer>().material.color;
					color.a = 0.5f;

					copy.GetComponent<Renderer>().material.SetColor("_Color", color);

					scores[playerInZone] += Time.deltaTime;
				}

				else
				{
					Color color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
					copy.GetComponent<Renderer>().material.SetColor("_Color", color);
				}

				playersInZone.Clear();
			}
		}

		public override void displayEvent()
		{
			GameObject.Find("Event Start").GetComponent<Text>().text = "King of the hill!";
		}

		public override void displayResults()
		{
			for (int i = 0; i < 4; i++)
			{
				if (scores[i] >= 3.0f)
				{
					GameObject.Find("Event Start").GetComponent<Text>().text = "Player " + (i + 1).ToString() + " won King of the Hill!";
					break;
				}
			}
		}

		public override void cleanup()
		{
			Destroy(copy);
			SoundEngineWrapper.QueueSound("event_win", 0, false, 13);
		}

		public override bool finished()
		{
			for (int i = 0; i < 4; i++)
			{
				if (scores[i] >= 3.0f)
				{
					return true;
				}
			}

			return false;
		}
	}
}