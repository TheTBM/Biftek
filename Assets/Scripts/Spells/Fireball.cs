using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundEnginePluginWrapper;

public class Fireball : MonoBehaviour
{
    public float cooldown;
	public int damage;
	public float velocity;
	public float lifeTime;
	public Vector3 direction;
	private Info info;
	private Info otherInfo;
	public int owner;
	Player friendlyPlayer;
	Player enemyPlayer;
	private float flightSoundDelay;

	public ParticleSystem travelEmitter;
	public GameObject explodeEmitter;
	private ParticleSystem teCopy;
	private GameObject eeCopy;

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
		flightSoundDelay = 0.6f;

		teCopy = Instantiate(travelEmitter, transform) as ParticleSystem;
	}

	// Update is called once per frame
	void Update()
	{
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0)
		{
			Destroy(gameObject);
			SoundEngineWrapper.StopChannel(10);
			SoundEngineWrapper.QueueSound("fireball_explode", 0, false, 10);
		}

		else
		{
			flightSoundDelay -= Time.deltaTime;
			transform.Translate(direction * velocity * Time.deltaTime);

			if (flightSoundDelay <= 0.0f)
			{
				SoundEngineWrapper.QueueSound("fireball_fly", 0, false, 10);
				flightSoundDelay = 10.0f;
			}
		}

		SyncEmitters();
	}

	void SyncEmitters()
	{
		teCopy.transform.SetPositionAndRotation(transform.position, transform.rotation);
	}

	public int getDamage()
	{
		return damage;
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag != "Fireball" && other.gameObject.tag != "River")
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
			SoundEngineWrapper.StopChannel(10);
			SoundEngineWrapper.QueueSound("fireball_explode", 0, false, 10);
		}
		else
		{
			Physics.IgnoreCollision(GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
		}
	}

	void OnDestroy()
	{
		eeCopy = Instantiate(explodeEmitter, transform.position, transform.rotation) as GameObject;
		eeCopy.GetComponent<GeneralEmitter>().KillEmitter();
	}
}
