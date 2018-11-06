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

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Fire stuff");
        if (other.gameObject.GetComponent<Player>() != null)
        {
            int id;
            int.TryParse(other.gameObject.tag, out id);

            if (owner != id)
            {
                GetComponentInParent<FireRun>().damageEnemy(other.gameObject.GetComponent<Player>());
            }
        }
    }
}
