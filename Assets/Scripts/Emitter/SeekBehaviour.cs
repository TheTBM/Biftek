using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class was written with the help of the following youtube video
//https://www.youtube.com/watch?v=KsT_ZyTv1ms

public class SeekBehaviour : MonoBehaviour
{
	public Transform target;
	public float force = 10.0f;

	private ParticleSystem emitter;

	void Start ()
	{
		emitter = GetComponent<ParticleSystem>();
	}
	

	void LateUpdate ()
	{
		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[emitter.particleCount];
		emitter.GetParticles(particles);

		if (emitter.main.simulationSpace == ParticleSystemSimulationSpace.Local)
		{
			for (int i = 0; i < particles.Length; i++)
			{
				ParticleSystem.Particle p = particles[i];

				Vector3 targetDirection = (-p.position).normalized;

				Vector3 seekForce = targetDirection * force * Time.deltaTime;

				p.velocity += seekForce;

				particles[i] = p;
			}
		}

		else
		{
			for (int i = 0; i < particles.Length; i++)
			{
				ParticleSystem.Particle p = particles[i];

				Vector3 targetDirection = (target.position - p.position).normalized;

				Vector3 seekForce = targetDirection * force * Time.deltaTime;

				p.velocity += seekForce;

				particles[i] = p;
			}
		}

		emitter.SetParticles(particles, particles.Length);
	}
}
