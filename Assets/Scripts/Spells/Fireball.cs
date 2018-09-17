using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
	public int damage;
	public float velocity;
	public float lifeTime;
	public Vector3 direction;
	public Collision collision;
	private Info info;
	private Info otherInfo;
	public int owner;
	Player player;

	public Fireball()
	{
		damage = 2;
	}
	
	// Use this for initialization
	void Start ()
	{
		direction.Set(0, 0, 1);
		damage = 2;
		info = GetComponent<Info>();
	}

	// Update is called once per frame
	void Update ()
	{
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0)
		{
			Destroy(gameObject);
		}

		else
		{
			transform.Translate(direction * velocity * Time.deltaTime);
		}		
	}

	public int getDamage()
	{
		return damage;
	}

	void OnCollisionEnter(Collision other)
	{
		info.setDamage(damage);
		otherInfo = other.gameObject.GetComponent<Info>();

		//if (otherInfo.getPlayer())
		{
			otherInfo.takeDamage(info.getDamage());
			Debug.Log("Success");
		}

		Destroy(gameObject);
	}
}
