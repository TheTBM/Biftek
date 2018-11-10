using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public int damage;
    private Info info;
    private Info otherInfo;
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
    }

    // Use this for initialization
    void Start()
    {
        damage = 1;
		info = GetComponent<Info>();
		canHit[GetComponentInParent<LightningParent>().owner - 1] = false;
    }

    public int getDamage()
    {
        return damage;
    }

    void OnTriggerEnter(Collider other)
    {
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
                        friendlyPlayer = GameObject.FindGameObjectWithTag(GetComponentInParent<LightningParent>().owner.ToString()).GetComponent<Player>();
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
