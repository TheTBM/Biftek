using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
	public int damage;
	float scalingFactor;
	public float velocity;
	public float lifetime;
	public Vector3 direction;
	public Collision collision;
	private Info info;
	private Info otherInfo;
	public int owner;
	Player friendlyPlayer;
	Player enemyPlayer;

	// Use this for initialization
	void Start ()
	{
		direction.Set(0, 0, 1);
		info = GetComponent<Info>();
		scalingFactor = 1.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		lifetime -= Time.deltaTime;
		if (lifetime <= 0.0f)
		{
			Destroy(gameObject);
		}

		else
		{
			if (scalingFactor < 1.0f)
			{
				Destroy(gameObject);
			}

			scalingFactor += Time.deltaTime;

			if (scalingFactor >= 4.0f)
			{
				scalingFactor = 4.0f;
			}

			gameObject.transform.localScale = new Vector3(scalingFactor, scalingFactor, scalingFactor);

			transform.Translate(direction * (velocity * (scalingFactor * 0.5f)) * Time.deltaTime);
		}
	}

	public int getDamage()
	{
		return damage;
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag != "Boulder")
		{
			info.setDamage((int)scalingFactor);

			if (other.gameObject.GetComponent<Info>() != null)
			{
				otherInfo = other.gameObject.GetComponent<Info>();

				if (otherInfo.getPlayer())
				{
					enemyPlayer = other.gameObject.GetComponent<Player>();
					otherInfo.takeDamage(info.getDamage());

					if (otherInfo.getHealth() <= 0)
					{
						int num;

						int.TryParse(other.gameObject.tag, out num);

						if (num == owner)
						{
							otherInfo.setHealth(1);
						}

						else
						{
							enemyPlayer.resetRespawnTimer();
							friendlyPlayer = GameObject.FindGameObjectWithTag(owner.ToString()).GetComponent<Player>();
							friendlyPlayer.addPoints(1);
						}
					}
				}

				else
				{
					otherInfo.takeDamage(info.getDamage());
				}
			}

			if (other.gameObject.tag != "Fireball" && other.gameObject.tag != "Floor")
			{
				Destroy(gameObject);
			}
		}

		else
		{
			scalingFactor -= otherInfo.getDamage();
		}
	}
}
