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
	Player friendlyPlayer;
	Player enemyPlayer;

	public Fireball()
	{
		damage = 2;
	}

	// Use this for initialization
	void Start()
	{
		direction.Set(0, 0, 1);
		damage = 2;
		info = GetComponent<Info>();
	}

	// Update is called once per frame
	void Update()
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
		if (other.gameObject.tag != "Fireball")
		{
			info.setDamage(damage);

			if (other.gameObject.GetComponent<Info>() != null)
			{
				otherInfo = other.gameObject.GetComponent<Info>();

				if (otherInfo.getPlayer())
				{
					enemyPlayer = other.gameObject.GetComponent<Player>();
					otherInfo.takeDamage(info.getDamage());

					if (otherInfo.getHealth() <= 0)
					{
						enemyPlayer.resetRespawnTimer();
						friendlyPlayer = GameObject.FindGameObjectWithTag(owner.ToString()).GetComponent<Player>();
						friendlyPlayer.addPoints(1);
					}
				}

				else
				{
					otherInfo.takeDamage(info.getDamage());
				}
			}
			Destroy(gameObject);
		}
		else
		{
			Physics.IgnoreCollision(GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
		}
	}
}
