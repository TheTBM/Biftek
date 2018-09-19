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
	Vector3 lookDirection;
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

		direction.x = Input.GetAxis("Horizontal Left " + gameObject.tag);
		direction.z = Input.GetAxis("Vertical Left " + gameObject.tag);

        transform.rotation = Quaternion.LookRotation(new Vector3(0,0,0));

        transform.Translate(direction * velocity * Time.deltaTime);

		lookDirection.x = Input.GetAxis("Horizontal Right " + gameObject.tag);
		lookDirection.z = Input.GetAxis("Vertical Right " + gameObject.tag);

		transform.rotation = Quaternion.LookRotation(lookDirection);

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
}
