using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerInputs;

public class Player : MonoBehaviour
{
	//Player variables
	public int health;
	public float velocity;
	public float maxVelocity;
	public SpawnManager spawner;
	private Info info;
	Vector3 direction;
	Vector3 lookDirection;
	bool initiated = false;
	int controller;

	private Fireball hit;

	bool player1;
	bool player2;

	public GameObject gameObject;

	// Use this for initialization
	void Start ()
	{
		player1 = player2 = true; // change later
		info = GetComponent<Info>();
		ControllerPluginWrapper.Initiate();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!initiated)
		{
			initiate();
		}

		ControllerPluginWrapper.UpdateControllers();
		int.TryParse(gameObject.tag, out controller);
		controller -= 1;

		checkDeath();

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

		//lookDirection.x = Input.GetAxis("Horizontal Right " + gameObject.tag);
		//lookDirection.z = Input.GetAxis("Vertical Right " + gameObject.tag);

		transform.rotation = Quaternion.LookRotation(lookDirection);

		ControllerPluginWrapper.RefreshStates();

	}

	void checkDeath()
	{
		if (info.getHealth() <= 0)
		{
			spawner.playerDead(controller);
			Destroy(gameObject);
		}
	}

	public void initiate()
	{
		info.setHealth(health);
		info.setPlayer(true);
		initiated = true;
	}
}
