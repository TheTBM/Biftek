using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpawn : MonoBehaviour
{
    public float cooldown;
    private float currCooldown;
    public GameObject[] SpawnPoints = new GameObject[8];
    public GameObject Pickup;
    GameObject Spawn;

    public bool[] SpawnFree = new bool[8];

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            SpawnFree[i] = true;
        }

        for (int i = 0; i < 5; i++)
        {
            if (canSpawn())
            {
                GameObject pickup;
                int p = Random.Range(0, 8);

                while (SpawnFree[p] == false)
                {
                    p++;
                    p = p % 8;
                }

                pickup = Instantiate(Pickup, SpawnPoints[p].transform.position, SpawnPoints[p].transform.rotation) as GameObject;
                pickup.tag = "PickUp";
                pickup.GetComponent<PickUp>().spawnLocation = p;
                SpawnFree[p] = false;
            }
        }
        currCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        //tick down cooldown timer
        currCooldown -= Time.deltaTime;

        //if it is time to spawn and it is possible to spawn a spell do this
        if (currCooldown <= 0.0f && canSpawn())
        {
            //randomizing a value for position of new spell
            int p = Random.Range(0, 8);

            //check to make sure the chosen spawn position is free, if not increase it by one and if it increases to 8 reset to 0
            while (SpawnFree[p] == false)
            {
                p++;
                p = p % 8;
            }

            //spawn the new spell
            Spawn = Instantiate(Pickup, SpawnPoints[p].transform.position, SpawnPoints[p].transform.rotation) as GameObject;
            Spawn.tag = "PickUp";
            //tell the new spawn what location it was spawned at
            Spawn.GetComponent<PickUp>().spawnLocation = p;
            //assign the spawn position it was created at to be occupied
            SpawnFree[p] = false;

            //reset cooldown
            currCooldown = cooldown;
        }
    }

    public void setSpawnFreeTrue(int i)
    {
        SpawnFree[i] = true;
    }

    bool canSpawn()
    {
        for (int i = 0; i < 8; i++)
        {
            if (SpawnFree[i] == true)
            {
                return true;
            }
        }
        return false;
    }

    //void Initiate()
    //{
    //
    //}
}
