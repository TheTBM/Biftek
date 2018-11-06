using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float cooldown;
    public int damage;
    public float lifeTime;
    private Info info;
    private Info otherInfo;
    public int owner;
    Player friendlyPlayer;
    Player enemyPlayer;
    bool[] canHit = new bool[4];

    public Lightning()
    {
        damage = 1;
        for (int i = 0; i < 4; i++)
        {
            canHit[i] = true;
        }
        canHit[owner] = false;
    }

    // Use this for initialization
    void Start()
    {
        damage = 1;
        info = GetComponent<Info>();
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

            //if (flightSoundDelay <= 0.0f)
            //{
            //    SoundEngineWrapper.PlayASound("fireball_fly", 0, false, 10);
            //    flightSoundDelay = 10.0f;
            //}
        }
    }

    public int getDamage()
    {
        return damage;
    }

    void OnCollisionEnter(Collision other)
    {
        //Physics.IgnoreCollision(GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());

        info.setDamage(damage);

        if (other.gameObject.GetComponent<Info>() != null)
        {
            otherInfo = other.gameObject.GetComponent<Info>();

            if (otherInfo.getPlayer())
            {
                int id;
                int.TryParse(other.gameObject.tag, out id);
                if (canHit[id - 1])
                {
                    enemyPlayer = other.gameObject.GetComponent<Player>();
                    otherInfo.takeDamage(info.getDamage());

                    if (otherInfo.getHealth() <= 0)
                    {
                        enemyPlayer.resetRespawnTimer();
                        friendlyPlayer = GameObject.FindGameObjectWithTag(owner.ToString()).GetComponent<Player>();
                        friendlyPlayer.addPoints(1);
                    }

                    canHit[id - 1] = false;
                }
            }
            else
            {

            }
        }

        //SoundEngineWrapper.StopChannel(10);
        //SoundEngineWrapper.PlayASound("fireball_explode", 0, false, 10);
    }
}
