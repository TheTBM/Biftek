using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.GetComponent<Info>().getPlayer())
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
            Debug.Log("Hey this shouldn't have collided");
        }
    }
}