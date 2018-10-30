using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundEnginePluginWrapper;

public class BubbleShield : MonoBehaviour
{
	public int health;
	public float lifeTime;
	public GameObject theBubble;
	GameObject bubbleBlower;
	private Info info;
	bool initiated = false;

	// Use this for initialization
	void Start ()
	{
		//bubbleBlower.tag = theBubble.tag;
		bubbleBlower = GameObject.FindGameObjectWithTag(theBubble.tag);
		Physics.IgnoreCollision(bubbleBlower.GetComponent<Collider>(), theBubble.GetComponent<Collider>());
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
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0)
		{
			Destroy(theBubble);
		}

		if (!SoundEngineWrapper.IsPlaying(14))
		{
			SoundEngineWrapper.PlayASound("shield_active", 0, false, 14);
		}

		theBubble.transform.SetPositionAndRotation(bubbleBlower.transform.position, bubbleBlower.transform.rotation);
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

	void OnDestroy()
	{
		SoundEngineWrapper.StopChannel(14);
		SoundEngineWrapper.PlayASound("shield_deactivate", 0, false, 14);
	}
}
