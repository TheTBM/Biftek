using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerInputs;
using SoundEnginePluginWrapper;

public class PlayerSpells : MonoBehaviour
{
	public GameObject player;
	public GameObject Fireball;
	public GameObject BubbleShield;
	public GameObject Boulder;

	Player realPlayer;
	GameObject copy;
	Vector3 Aim;

	public const float globalCooldown = 0.5f;
    public const float fireballBaseCooldown = 1.0f;
    public const float bubbleshieldBaseCooldown = 6.0f;
	public const float boulderBaseCooldown = 5.0f;
	public const float dashBaseCooldown = 1.0f;

	float cooldown;
    float fireballCooldown;
    float bubbleshieldCooldown;
	float boulderCooldown;
	float dashCooldown;
	int controller;

	float currentlyDashing;
	float maxDashTimer = 0.5f;

	// Use this for initialization
	void Start ()
	{
		realPlayer = player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		ControllerPluginWrapper.UpdateControllers();
		int.TryParse(player.tag, out controller);
		controller -= 1;

		if (realPlayer.dashing)
		{
			currentlyDashing -= Time.deltaTime;

			if (currentlyDashing <= 0.0f)
			{
				realPlayer.dashing = false;
			}
		}

		else if (cooldown <= 0)
		{
            //R1
			if (ControllerPluginWrapper.GetButtonPressed(controller, 9) && fireballCooldown <= 0.0f)
			{
				copy = Instantiate(Fireball, player.transform.position + player.transform.forward * 1.75f, player.transform.rotation) as GameObject;

				Fireball fireball = copy.GetComponent<Fireball>();
				fireball.owner = controller + 1;

				fireballCooldown = fireballBaseCooldown;
				cooldown = globalCooldown;

				SoundEngineWrapper.PlayASound("fireball_shoot", 0, false, 10);
			}

            //L1
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

			else if (ControllerPluginWrapper.RightTrigger(controller) >= 0.3f && dashCooldown <= 0.0f)
			{
				realPlayer.saveDirection();
				realPlayer.dashing = true;
				currentlyDashing = maxDashTimer;
				dashCooldown = dashBaseCooldown;
				cooldown = globalCooldown;
			}
		}

        cooldown -= Time.deltaTime;
        fireballCooldown -= Time.deltaTime;
        bubbleshieldCooldown -= Time.deltaTime;
		boulderCooldown -= Time.deltaTime;
		currentlyDashing -= Time.deltaTime;
		dashCooldown -= Time.deltaTime;

		ControllerPluginWrapper.RefreshStates();
    }

    public void triggerGlobalCooldown()
    {
        cooldown = globalCooldown;
    }

}