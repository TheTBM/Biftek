using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundEnginePluginWrapper;

public class Boulder : MonoBehaviour
{
    public static float cooldown = 5;
	public int damage;
	float scalingFactor;
	float rollSoundDelay;
	public float velocity;
	public float lifetime;
	public Vector3 direction;
	public Collision collision;
	private Info info;
	private Info otherInfo;
	public int owner;
	Player friendlyPlayer;
	Player enemyPlayer;

	public ParticleSystem travelEmitter;
	public GameObject crumbleEmitter;
	private ParticleSystem teCopy;
	private GameObject ceCopy;

	// Use this for initialization
	void Start()
	{
		direction.Set(0, 0, 1);
		info = GetComponent<Info>();
		scalingFactor = 1.0f;
		rollSoundDelay = 0.5f;
		teCopy = Instantiate(travelEmitter, transform.position, transform.rotation * new Quaternion(-1, 0, 0, 0)) as ParticleSystem;
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
			rollSoundDelay -= Time.deltaTime;

			if (rollSoundDelay <= 0.0f)
			{
				SoundEngineWrapper.QueueSound("boulder_roll", 0, false, 12);
				rollSoundDelay = 10.0f;
			}

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

			SyncEmitters();
		}
	}

	public int getDamage()
	{
		return damage;
	}

	void SyncEmitters()
	{
		teCopy.transform.SetPositionAndRotation(transform.position - new Vector3(0, 0.5f * scalingFactor, 0), transform.rotation * new Quaternion(-1, 0, 0, 0));
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

	void OnDestroy()
	{
		SoundEngineWrapper.StopChannel(12);
		SoundEngineWrapper.QueueSound("boulder_crumble", 0, false, 12);

		teCopy.Stop(true, ParticleSystemStopBehavior.StopEmitting);
		teCopy.GetComponent<GeneralEmitter>().KillEmitter();

		ceCopy = Instantiate(crumbleEmitter, transform.position, transform.rotation) as GameObject;
		ceCopy.GetComponent<GeneralEmitter>().KillEmitter();
	}
}
