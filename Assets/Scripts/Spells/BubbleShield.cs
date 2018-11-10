using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundEnginePluginWrapper;

public class BubbleShield : MonoBehaviour
{
	public static float cooldown = 6;
	public int health;
	public float lifeTime;
	public GameObject theBubble;
	GameObject bubbleBlower;
	private Info info;
	bool initiated = false;

	public ParticleSystem activeEmitter;
	private ParticleSystem aeCopy;

	// Use this for initialization
	void Start ()
	{
		//bubbleBlower.tag = theBubble.tag;
		bubbleBlower = GameObject.FindGameObjectWithTag(theBubble.tag);
		Physics.IgnoreCollision(bubbleBlower.GetComponent<Collider>(), theBubble.GetComponent<Collider>());
		info = GetComponent<Info>();

		activeEmitter.GetComponent<SeekBehaviour>().target = transform;
		aeCopy = Instantiate(activeEmitter, transform.position + new Vector3(0, 1.46f, 0), transform.rotation) as ParticleSystem;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!initiated)
		{
			initiate();
		}
		checkDeath();
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0)
		{
			Destroy(theBubble);
		}

		if (!SoundEngineWrapper.IsPlaying(14))
		{
			SoundEngineWrapper.QueueSound("shield_active", 0, false, 14);
		}

		theBubble.transform.SetPositionAndRotation(bubbleBlower.transform.position + new Vector3(0, 1.46f, 0), bubbleBlower.transform.rotation);
		SyncEmitters();
	}

	public void initiate()
	{
		info.setHealth(health);
		initiated = true;
	}

	void checkDeath()
	{
		if (info.getHealth() <= 0)
		{
			Destroy(gameObject);
		}
	}

	void SyncEmitters()
	{
		aeCopy.transform.SetPositionAndRotation(transform.position, transform.rotation);
	}

	void OnDestroy()
	{
		SoundEngineWrapper.StopChannel(14);
		SoundEngineWrapper.QueueSound("shield_deactivate", 0, false, 14);
		Destroy(aeCopy);
	}
}
