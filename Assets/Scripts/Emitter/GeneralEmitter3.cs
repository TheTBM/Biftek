using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEmitter3 : MonoBehaviour {

	public ParticleSystem emitter1;
	public ParticleSystem emitter2;
	public ParticleSystem emitter3;
	public float remainingLife;
	bool countdown = false;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
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

	public void KillEmitters()
	{
		countdown = true;
	}
}
