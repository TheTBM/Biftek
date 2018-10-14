using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Events;

public class EventManager : MonoBehaviour
{
	float timer;
	float endTimer;
	bool initiated;
	bool eventHappening;
	public GameObject boundaryKOTH;

	CustomEvent currentEvent;

	// Use this for initialization
	void Start ()
	{
		timer = 5.0f;
		initiated = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!initiated)
		{
			initiate();
		}

		if (timer > 0.0f)
		{
			timer -= Time.deltaTime;
		}

		if (!eventHappening)
		{
			if (timer <= 0.0f)
			{
				//Do some kind of random number generator to determine the event to do
				{
					currentEvent = new KingOfTheHill(boundaryKOTH);
				}

				eventHappening = true;
				timer = 5.0f;
				endTimer = 5.0f;
				currentEvent.initiate();
				currentEvent.displayEvent();
			}
		}

		else
		{
			currentEvent.update();

			if (currentEvent.finished() && endTimer == 5.0f)
			{
				currentEvent.displayResults();
				currentEvent.cleanup();
				endTimer -= Time.deltaTime;
			}

			else if (currentEvent.finished() && endTimer > 0.0f)
			{
				endTimer -= Time.deltaTime;
			}

			else if (currentEvent.finished() && endTimer <= 0.0f)
			{
				eventHappening = false;
				timer = 5.0f;
				GameObject.Find("Event Start").GetComponent<Text>().text = "";
			}

			else if (timer <= 0.0f)
			{
				GameObject.Find("Event Start").GetComponent<Text>().text = "";
			}
		}
	}

	void initiate()
	{
		initiated = true;
	}
}
