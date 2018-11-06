using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEmitter : MonoBehaviour
{
	public ParticleSystem emitter;
	public float remainingLife;
	bool countdown = false;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (countdown)
		{
			remainingLife -= Time.deltaTime;

			if (remainingLife <= 0.0f)
			{
				Destroy(gameObject);
			}
		}
	}

	public void KillEmitter()
	{
		countdown = true;
	}
}
