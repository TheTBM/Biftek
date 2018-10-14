using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerInputs;

public class PlayerSpells : MonoBehaviour
{
	public GameObject player;
	public GameObject Fireball;
	public GameObject BubbleShield;
	public GameObject Boulder;

	GameObject copy;
	Vector3 Aim;

	public const float globalCooldown = 0.5f;
    public const float fireballBaseCooldown = 1.0f;
    public const float bubbleshieldBaseCooldown = 6.0f;
	public const float boulderBaseCooldown = 5.0f;

	float cooldown;
    float fireballCooldown;
    float bubbleshieldCooldown;
	float boulderCooldown;
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
			if (ControllerPluginWrapper.GetButtonPressed(controller, 9) && fireballCooldown <= 0.0f)
			{
				copy = Instantiate(Fireball, player.transform.position + player.transform.forward * 1.75f, player.transform.rotation) as GameObject;

				Fireball fireball = copy.GetComponent<Fireball>();
				fireball.owner = controller + 1;

				fireballCooldown = fireballBaseCooldown;
				cooldown = globalCooldown;
			}

			else if (ControllerPluginWrapper.GetButtonPressed(controller, 8) && bubbleshieldCooldown <= 0.0f)
			{
				copy = Instantiate(BubbleShield, player.transform.position, player.transform.rotation) as GameObject;
				copy.tag = player.tag;
				bubbleshieldCooldown = bubbleshieldBaseCooldown;
				cooldown = globalCooldown;
			}

			else if ((ControllerPluginWrapper.LeftTrigger(controller) >= 0.3f) && (boulderCooldown <= 0.0f))
			{
				copy = Instantiate(Boulder, player.transform.position + player.transform.forward * 1.75f, player.transform.rotation) as GameObject;

				Boulder boulder = copy.GetComponent<Boulder>();
				boulder.owner = controller + 1;

				boulderCooldown = boulderBaseCooldown;
				cooldown = globalCooldown;
			}
		}

        cooldown -= Time.deltaTime;
        fireballCooldown -= Time.deltaTime;
        bubbleshieldCooldown -= Time.deltaTime;
		boulderCooldown -= Time.deltaTime;

		ControllerPluginWrapper.RefreshStates();
    }
}