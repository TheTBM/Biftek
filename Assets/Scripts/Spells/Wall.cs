using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundEnginePluginWrapper;

public class Wall : MonoBehaviour
{
    public int health;
<<<<<<< HEAD
    public static float cooldown = 10;
    public float lifeTime;
=======
	public static float cooldown = 10;
	public float lifeTime;
>>>>>>> 110a58946bc9913bad69f66d44c0ebaf6754e24c
    private Info info;
    public int owner;
    public bool initiated = false;

	public GameObject spawnEmitter;
	private GameObject seCopy;
	public GameObject dieEmitter;
	private GameObject deCopy;

    void Start()
    {
        info = GetComponent<Info>();

		seCopy = Instantiate(spawnEmitter, transform.position - new Vector3(0, 1, 0), transform.rotation) as GameObject;
		seCopy.GetComponent<GeneralEmitter3>().KillEmitters();
    }

    // Update is called once per frame
    void Update()
    {
        if (!initiated)
        {
            Initiate();
        }

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
			deCopy = Instantiate(dieEmitter, transform.position, transform.rotation) as GameObject;
			deCopy.GetComponent<GeneralEmitter3>().KillEmitters();

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

        if (info.getHealth() <= 0)
        {
            Destroy(gameObject);
			deCopy = Instantiate(dieEmitter, transform.position, transform.rotation) as GameObject;
			deCopy.GetComponent<GeneralEmitter3>().KillEmitters();
		}
    }

    private void Initiate()
    {
        info.setHealth(health);
        initiated = true;
    }

	void OnDestroy()
	{
		SoundEngineWrapper.QueueSound("wall_destroy", 0, false, 21);
	}
}
