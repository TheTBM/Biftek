using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningParent : MonoBehaviour
{
    public float cooldown;
    public float lifeTime;
    public int owner;

	public GameObject castEmitter;
	private GameObject ceCopy;

	// Update is called once per frame
	void Update ()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
			//SoundEngineWrapper.StopChannel(10);
			//SoundEngineWrapper.PlayASound("fireball_explode", 0, false, 10);
		}
        else
        {

			//flightSoundDelay -= Time.deltaTime;

			//if (flightSoundDelay <= 0.0f)
			//{
			//    SoundEngineWrapper.PlayASound("fireball_fly", 0, false, 10);
			//    flightSoundDelay = 10.0f;
			//}
		}
    }

	public void PlayEmitter()
	{
		ceCopy = Instantiate(castEmitter, transform.position - (transform.forward * 2), transform.rotation) as GameObject;
		ceCopy.GetComponent<GeneralEmitter3>().KillEmitters();
	}
}
