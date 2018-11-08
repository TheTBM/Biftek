using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hailstorm : MonoBehaviour
{
    public float slowPercent;
    public static float cooldown = 10;
    public float velocity;
    public float lifeTime;
    public Vector3 direction;
    private Info info;
    private Info otherInfo;
    public int owner;
    Player friendlyPlayer;
    Player enemyPlayer;
    //private float flightSoundDelay;

    // Use this for initialization
    void Start()
    {
        direction.Set(0, 0, 1);
        info = GetComponent<Info>();
        //flightSoundDelay = 0.6f;
    }

    // Update is called once per frame
    void Update()
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
            transform.Translate(direction * velocity * Time.deltaTime);

            //if (flightSoundDelay <= 0.0f)
            //{
            //    SoundEngineWrapper.PlayASound("fireball_fly", 0, false, 10);
            //    flightSoundDelay = 10.0f;
            //}
        }
    }

    public float getSlow()
    {
        return slowPercent;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Info>() != null)
        {
            otherInfo = other.gameObject.GetComponent<Info>();

            if (otherInfo.getPlayer())
            {
                enemyPlayer = other.gameObject.GetComponent<Player>();

                enemyPlayer.addSlow(getSlow());
            }
        }
    }
}
