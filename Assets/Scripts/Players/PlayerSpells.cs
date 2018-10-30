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
	public const float dashBaseCooldown = 3.0f;

	float cooldown;
    float spell1Cooldown;
    float spell2Cooldown;
	float spell3Cooldown;
	float spell4Cooldown;
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
			if (ControllerPluginWrapper.GetButtonPressed(controller, 9) && spell1Cooldown <= 0.0f)
			{
                castSpell(GetComponent<SpellInventory>().castSpell(0), ref spell1Cooldown);
			}
            //L1
			else if (ControllerPluginWrapper.GetButtonPressed(controller, 8) && spell2Cooldown <= 0.0f)
			{
                castSpell(GetComponent<SpellInventory>().castSpell(1), ref spell2Cooldown);
            }
			else if ((ControllerPluginWrapper.LeftTrigger(controller) >= 0.3f) && (spell3Cooldown <= 0.0f))
			{
                castSpell(GetComponent<SpellInventory>().castSpell(2), ref spell3Cooldown);
            }
			else if (ControllerPluginWrapper.RightTrigger(controller) >= 0.3f && spell4Cooldown <= 0.0f)
			{
                castSpell(GetComponent<SpellInventory>().castSpell(3), ref spell4Cooldown);
            }
		}

        cooldown -= Time.deltaTime;
        spell1Cooldown -= Time.deltaTime;
        spell2Cooldown -= Time.deltaTime;
		spell3Cooldown -= Time.deltaTime;
		currentlyDashing -= Time.deltaTime;
		spell4Cooldown -= Time.deltaTime;

		ControllerPluginWrapper.RefreshStates();
    }

    void castSpell(Spells cast, ref float setCooldown)
    {
        switch(cast)
        {
            case Spells.Empty:// if you don't have a spell in this slot

                break;
            case Spells.Fireball: // cast fireball spell
                copy = Instantiate(Fireball, player.transform.position + player.transform.forward * 1.75f, player.transform.rotation) as GameObject;

                Fireball fireball = copy.GetComponent<Fireball>();
                fireball.owner = controller + 1;

                setCooldown = fireballBaseCooldown;
                cooldown = globalCooldown;

                SoundEngineWrapper.PlayASound("fireball_shoot", 0, false, 10);
                break;
               
            case Spells.Bubbleshield: // cast bubble shield spell
                copy = Instantiate(BubbleShield, player.transform.position, player.transform.rotation) as GameObject;
                copy.tag = player.tag;
                setCooldown = bubbleshieldBaseCooldown;
                cooldown = globalCooldown;
                break;

            case Spells.Boulder: // cast boulder spell
                copy = Instantiate(Boulder, player.transform.position + player.transform.forward * 1.75f, player.transform.rotation) as GameObject;

                Boulder boulder = copy.GetComponent<Boulder>();
                boulder.owner = controller + 1;

                setCooldown = boulderBaseCooldown;
                cooldown = globalCooldown;
                break;
            case Spells.Dash: // cast dash spell
                realPlayer.saveDirection();
                realPlayer.dashing = true;
                currentlyDashing = maxDashTimer;

                setCooldown = dashBaseCooldown;
                cooldown = globalCooldown;
                break;

            default:
                break;
        }
    }

    public void resetSpellCooldown(int spell)
    {
        switch(spell)
        {
            case 0:
                spell1Cooldown = 0;
                break;
            case 1:
                spell2Cooldown = 0;
                break;
            case 2:
                spell3Cooldown = 0;
                break;
            case 3:
                spell4Cooldown = 0;
                break;
            
            default:
                break;
        }
    }

    public void triggerGlobalCooldown()
    {
        cooldown = globalCooldown;
    }

}