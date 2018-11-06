using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRun : MonoBehaviour
{
    public GameObject Fire;
    public float cooldown;
    public int damage;
    public int speedBonus;
    public float lifeTime;
    private Info info;
    private Info otherInfo;
    public int owner;
    GameObject parentPlayer;
    GameObject copy;
    Player friendlyPlayer;

    Vector3 lastFirePosition;

    // Use this for initialization
    void Start()
    {
        info = GetComponent<Info>();
        parentPlayer = GameObject.FindGameObjectWithTag(owner.ToString());
        lastFirePosition = new Vector3(0, 0, -100);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime; // reduce duration remaining on FireRun
        transform.position = parentPlayer.transform.position; // update location to the player location

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        if ((transform.position - lastFirePosition).magnitude > 1)
        {
            // spawn a fire here
            summonFire();
        }

        parentPlayer.GetComponent<Rigidbody>().velocity *= (speedBonus * 1.01f);
    }

    public void damageEnemy(Player enemy)
    {
        if (enemy.getFireDamageTimer() > 1.0f)
        {
            enemy.GetComponent<Info>().takeDamage(damage);
            enemy.resetFireDamageTimer();

            if (otherInfo.getHealth() <= 0)
            {
                enemy.resetRespawnTimer();
                friendlyPlayer = GameObject.FindGameObjectWithTag(owner.ToString()).GetComponent<Player>();
                friendlyPlayer.addPoints(1);
            }
        }
    }

    private void summonFire()
    {
        copy = Instantiate(Fire, transform.position + transform.forward * 1.75f, transform.rotation) as GameObject;

        Fire fire = copy.GetComponent<Fire>();
        fire.owner = owner;
    }
}
