﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerInputs;

public class Player : MonoBehaviour
{
	//Player variables
	public int health;
	public float velocity;
	Info info;
	Vector3 direction;
	Vector3 lookDirection;
	bool initiated = false;
	int controller;
	int points;
	public float respawnTimer;

	bool alive;

	public Material dead;
	Vector3 deadPosition;

	public GameObject gameObject;

	// Use this for initialization
	void Start ()
	{
		info = GetComponent<Info>();
		ControllerPluginWrapper.Initiate();
		deadPosition = new Vector3(0, -10, 0);
		alive = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!initiated)
		{
			initiate();
		}

		if (info.getHealth() <= 0)
		{
			kill();
		}

		ControllerPluginWrapper.UpdateControllers();
		int.TryParse(gameObject.tag, out controller);
		controller -= 1;

		//resets player direction before recalculating it
		direction.Set(0, 0, 0);

		if (!ControllerPluginWrapper.LStick_InDeadZone(controller))
		{
			direction.x = ControllerPluginWrapper.LeftStick_X(controller);
			direction.z = ControllerPluginWrapper.LeftStick_Y(controller);
		}

        transform.rotation = Quaternion.LookRotation(new Vector3(0,0,0));

        transform.Translate(direction * velocity * Time.deltaTime);

		if (!ControllerPluginWrapper.RStick_InDeadZone(controller))
		{
			lookDirection.x = ControllerPluginWrapper.RightStick_X(controller);
			lookDirection.z = ControllerPluginWrapper.RightStick_Y(controller);
		}

		transform.rotation = Quaternion.LookRotation(lookDirection);

		ControllerPluginWrapper.RefreshStates();
	}

	public void kill()
	{
		alive = false;
		gameObject.GetComponent<Renderer>().material = dead;
		gameObject.transform.SetPositionAndRotation(deadPosition, gameObject.transform.rotation);
	}

	public void respawn(Material m, GameObject location)
	{
		alive = true;
		info.setHealth(health);
		gameObject.GetComponent<Renderer>().material = m;
		gameObject.transform.SetPositionAndRotation(location.transform.position, location.transform.rotation);
	}

	public bool isAlive()
	{
		return alive;
	}

	public void addPoints(int p)
	{
		points += p;
	}

	public int getPoints()
	{
		return points;
	}

	public int getHealth()
	{
		return info.getHealth();
	}

	public void resetRespawnTimer()
	{
		info.setTimer(respawnTimer);
	}

	public void decreaseRespawnTimer(float t)
	{
		info.decreaseTimer(t);
	}

	public float getRespawnTimer()
	{
		return info.getTimer();
	}

	public void initiate()
	{
		info.setHealth(health);
		info.setPlayer(true);
		initiated = true;
	}
}
