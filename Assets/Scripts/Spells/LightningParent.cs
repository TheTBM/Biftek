using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningParent : MonoBehaviour
{
	public static float cooldown = 3;
	public float lifeTime;
    public int owner;

	public GameObject castEmitter;
	private GameObject ceCopy;

	// Update is called once per frame
	void Update ()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
		}
    }

	public void PlayEmitter()
	{
		ceCopy = Instantiate(castEmitter, transform.position - (transform.forward * 2), transform.rotation) as GameObject;
		ceCopy.GetComponent<GeneralEmitter3>().KillEmitters();
	}
}
