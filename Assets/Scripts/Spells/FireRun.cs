using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRun : MonoBehaviour
{
    public GameObject Fire;
    public float cooldown;
    public int damage;
    public float speedBonus;
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
        //transform.position = parentPlayer.transform.position; // update location to the player location

        if (lifeTime <= 0)
        {
            if (GetComponentsInChildren<Fire>().Length == 0)
            {
                Destroy(gameObject);
            }
        }

        if (lifeTime > 0)
        {
            if ((parentPlayer.transform.position - lastFirePosition).magnitude > 1)
            {
                // spawn a fire here
                summonFire();
            }
            parentPlayer.GetComponent<Player>().addSpeed(speedBonus);
        }

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
        copy = Instantiate(Fire, parentPlayer.transform.position, transform.rotation) as GameObject;

        Fire fire = copy.GetComponent<Fire>();
        fire.owner = owner;
        fire.transform.parent = this.transform;

        lastFirePosition = fire.transform.position;
    }
}
