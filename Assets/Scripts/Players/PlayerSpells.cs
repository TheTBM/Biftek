using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerInputs;

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
	int controller;


	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		ControllerPluginWrapper.UpdateControllers();
		int.TryParse(player.tag, out controller);
		controller -= 1;

		if (cooldown <= 0)
		{
			if (ControllerPluginWrapper.GetButtonPressed(controller, 9) && fireballCooldown <= 0)
			{
				copy = Instantiate(Fireball, player.transform.position + player.transform.forward * 1.75f, player.transform.rotation) as GameObject;

				Fireball fireball = copy.GetComponent<Fireball>();
				fireball.owner = controller + 1;

				fireballCooldown = fireballBaseCooldown;
				cooldown = globalCooldown;
			}

			else if (ControllerPluginWrapper.GetButtonPressed(controller, 8) && bubbleshieldCooldown <= 0)
			{
				copy = Instantiate(BubbleShield, player.transform.position, player.transform.rotation) as GameObject;
				copy.tag = player.tag;
				bubbleshieldCooldown = bubbleshieldBaseCooldown;
				cooldown = globalCooldown;
			}
		}

        cooldown -= Time.deltaTime;
        fireballCooldown -= Time.deltaTime;
        bubbleshieldCooldown -= Time.deltaTime;

		ControllerPluginWrapper.RefreshStates();
    }
}