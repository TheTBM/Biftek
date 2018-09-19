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
    public const float fireballBaseCooldown = 1.0f;
    public const float bubbleshieldBaseCooldown = 6.0f;
	float cooldown;
    float fireballCooldown;
    float bubbleshieldCooldown;



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
				if (Input.GetKey(KeyCode.Joystick6Button5) && fireballCooldown <= 0)
				{
					copy = Instantiate(Fireball, player.transform.position + player.transform.forward * 1.75f, player.transform.rotation) as GameObject;
                    fireballCooldown = fireballBaseCooldown;
					cooldown = globalCooldown;
				}
				else if (Input.GetKey(KeyCode.Joystick6Button4) && bubbleshieldCooldown <= 0)
				{
					copy = Instantiate(BubbleShield, player.transform.position, player.transform.rotation) as GameObject;
					copy.tag = "2";
                    bubbleshieldCooldown = bubbleshieldBaseCooldown;
					cooldown = globalCooldown;
				}
			}
		}

		if (player.tag == "1")
		{
			if (cooldown <= 0)
			{
				if (Input.GetKey(KeyCode.Joystick5Button5) && fireballCooldown <= 0)
				{
					copy = Instantiate(Fireball, player.transform.position + player.transform.forward * 1.75f, player.transform.rotation) as GameObject;
                    fireballCooldown = fireballBaseCooldown;
                    cooldown = globalCooldown;
				}

				else if (Input.GetKey(KeyCode.Joystick5Button4) && bubbleshieldCooldown <= 0)
				{
					copy = Instantiate(BubbleShield, player.transform.position, player.transform.rotation) as GameObject;
					copy.tag = "1";
                    bubbleshieldCooldown = bubbleshieldBaseCooldown;
                    cooldown = globalCooldown;
				}
			}
		}

        cooldown -= Time.deltaTime;
        fireballCooldown -= Time.deltaTime;
        bubbleshieldCooldown -= Time.deltaTime;
    }
}
