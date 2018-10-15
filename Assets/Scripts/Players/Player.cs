using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerInputs;

public class Player : MonoBehaviour
{
	//Player variables
	public int health;
	public int lives;
	public float velocity;
	Info info;
	Vector3 direction;
	Vector3 lookDirection;
	Vector3 dashDirection;
	bool initiated = false;
	int controller;
	int points;
	public float respawnTimer;

	bool alive;
	public bool dashing;

	public Material dead;
	Vector3 deadPosition;

	// Use this for initialization
	void Start()
	{
		info = GetComponent<Info>();
		ControllerPluginWrapper.Initiate();
		deadPosition = new Vector3(0, -10, 0);
		alive = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (!initiated)
		{
			initiate();
		}

		if (info.getHealth() <= 0 && alive)
		{
			kill();
		}

		ControllerPluginWrapper.UpdateControllers();
		int.TryParse(gameObject.tag, out controller);
		controller -= 1;

		if (!dashing)
		{
			//resets player direction before recalculating it
			direction.Set(0, 0, 0);

			if (!ControllerPluginWrapper.LStick_InDeadZone(controller))
			{
				direction.x = ControllerPluginWrapper.LeftStick_X(controller);
				direction.z = ControllerPluginWrapper.LeftStick_Y(controller);
			}

			transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 0));

			//transform.Translate(direction * velocity * Time.deltaTime);
			if (transform.GetComponent<Rigidbody>().velocity.magnitude < 10.0f)
			{
				transform.GetComponent<Rigidbody>().AddForce(direction * Mathf.Pow(velocity, 2));
			}

			if (transform.GetComponent<Rigidbody>().velocity.magnitude > 10.0f)
			{
				transform.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity.normalized * 10.0f;
			}

			if (!ControllerPluginWrapper.RStick_InDeadZone(controller))
			{
				lookDirection.x = ControllerPluginWrapper.RightStick_X(controller);
				lookDirection.z = ControllerPluginWrapper.RightStick_Y(controller);
			}

			transform.rotation = Quaternion.LookRotation(lookDirection);
		}
		else
		{
			if (!ControllerPluginWrapper.RStick_InDeadZone(controller))
			{
				lookDirection.x = ControllerPluginWrapper.RightStick_X(controller);
				lookDirection.z = ControllerPluginWrapper.RightStick_Y(controller);
			}
			GetComponent<Rigidbody>().velocity = dashDirection * velocity * 3;
		}

		if (tag == "1")
		{

			Debug.Log(transform.forward);
			Debug.Log(dashDirection);
		}
		ControllerPluginWrapper.RefreshStates();
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.GetComponent<Info>().getPlayer() && dashing)
		{
			other.gameObject.GetComponent<Info>().takeDamage(1);
		}
		dashing = false;
	}

	public void kill()
	{
		alive = false;
		info.loseLive();
		gameObject.GetComponent<Renderer>().material = dead;
		Color temp = GetComponentInChildren<SpriteRenderer>().color;
		temp.a = 0.0f;
		gameObject.GetComponentInChildren<SpriteRenderer>().color = temp;
		gameObject.transform.SetPositionAndRotation(deadPosition, gameObject.transform.rotation);
	}

	public void respawn(Material m, GameObject location)
	{
		alive = true;
		info.setHealth(health);
		gameObject.GetComponent<Renderer>().material = m;
		Color temp = GetComponentInChildren<SpriteRenderer>().color;
		temp.a = 0.2f;
		gameObject.GetComponentInChildren<SpriteRenderer>().color = temp;
		gameObject.transform.SetPositionAndRotation(location.transform.position, location.transform.rotation);
	}

	public void saveDirection()
	{
		dashDirection = transform.forward;
		Debug.Log(transform.forward);
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
		info.resetLives(lives);
		info.setPlayer(true);
		initiated = true;
	}
}
