using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpells : MonoBehaviour
{
	public GameObject player;
	public GameObject Fireball;
	public GameObject BubbleShield;
	GameObject copy;
	Vector3 Aim;

	public const float globalCooldown = 0.5f;
	float cooldown;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (player.tag == "2")
		{
			if (cooldown <= 0)
			{
				if (Input.GetKey(KeyCode.C))
				{
					copy = Instantiate(Fireball, player.transform.position + (Vector3.forward * 1.75f), player.transform.rotation) as GameObject;
					cooldown = globalCooldown;
				}

				else if (Input.GetKey(KeyCode.V))
				{
					copy = Instantiate(BubbleShield, player.transform.position, player.transform.rotation) as GameObject;
					copy.tag = "2";
					cooldown = globalCooldown;
				}
			}
			else
			{
				cooldown -= Time.deltaTime;
			}
		}

		if (player.tag == "1")
		{
			if (cooldown <= 0)
			{
				if (Input.GetKey(KeyCode.P))
				{
					copy = Instantiate(Fireball, player.transform.position + (Vector3.forward * 1.75f), player.transform.rotation) as GameObject;
					cooldown = globalCooldown;
				}

				else if (Input.GetKey(KeyCode.O))
				{
					copy = Instantiate(BubbleShield, player.transform.position, player.transform.rotation) as GameObject;
					copy.tag = "1";
					cooldown = globalCooldown;
				}
			}
			else
			{
				cooldown -= Time.deltaTime;
			}
		}
	}
}
