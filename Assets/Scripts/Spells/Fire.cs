using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float lifeTime;
    public int owner;

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            if (other.gameObject != GameObject.FindGameObjectWithTag(owner.ToString()))
            {
                GetComponentInParent<FireRun>().damageEnemy(other.gameObject.GetComponent<Player>());
            }
        }
    }
}
