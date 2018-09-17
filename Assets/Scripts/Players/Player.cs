using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//Player variables
	public int health;
	public float velocity;
	public float maxVelocity;
	private Info info;
	Vector3 direction;
	bool initiated = false;

	private Fireball hit;

	bool player1;
	bool player2;

	public GameObject gameObject;

	// Use this for initialization
	void Start ()
	{
		player1 = player2 = true; // change later
		info = GetComponent<Info>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!initiated)
		{
			initiate();
		}
		checkDeath();

		//resets player direction before recalculating it
		direction.Set(0, 0, 0);

		//Player 1 controls
		if (gameObject.tag == "Player 1")
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				direction.z += 1;
			}

			if (Input.GetKey(KeyCode.DownArrow))
			{
				direction.z -= 1;
			}

			if (Input.GetKey(KeyCode.LeftArrow))
			{
				direction.x -= 1;
			}

			if (Input.GetKey(KeyCode.RightArrow))
			{
				direction.x += 1;
			}

			Debug.Log(info.getHealth());

			direction.Normalize();
			transform.Translate(direction * velocity * Time.deltaTime);
		}

		//Player 2 controls
		if (gameObject.tag == "Player 2")
		{
			if (Input.GetKey(KeyCode.W))
			{
				direction.z += 1;
			}

			if (Input.GetKey(KeyCode.S))
			{
				direction.z -= 1;
			}

			if (Input.GetKey(KeyCode.A))
			{
				direction.x -= 1;
			}

			if (Input.GetKey(KeyCode.D))
			{
				direction.x += 1;
			}

			direction.Normalize();
			transform.Translate(direction * velocity * Time.deltaTime);
		}
	}

	void checkDeath()
	{
		if (info.getHealth() <= 0)
		{
			Destroy(gameObject);
		}
	}

	public void initiate()
	{
		info.setHealth(health);
		info.setPlayer(true);
		initiated = true;
	}

	//void OnCollisionEnter(Collision other)
	//{
	//	if (other.gameObject.tag == "Fireball")
	//	{
	//		Destroy(other.gameObject);
	//		hit = new Fireball();
	//		takeDamage(hit.getDamage());
	//		Debug.Log(health);
	//		Debug.Log(hit.getDamage());
	//	}
	//}
}
