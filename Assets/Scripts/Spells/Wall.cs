using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int health;
    public float cooldown;
    public float lifeTime;
    private Info info;
    public int owner;
    public bool initiated = false;

    void Start()
    {
        info = GetComponent<Info>();
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
        }
    }

    private void Initiate()
    {
        info.setHealth(health);
        initiated = true;
    }
}
