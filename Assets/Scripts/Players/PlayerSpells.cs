﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerInputs;
using SoundEnginePluginWrapper;
using UnityEngine.UI;

public class PlayerSpells : MonoBehaviour
{
    public GameObject player;
    public GameObject FireballPrefab;
    public GameObject BubbleShieldPrefab;
    public GameObject BoulderPrefab;
    public GameObject LightningPrefab;
    public GameObject HailstormPrefab;
    public GameObject FireRunPrefab;
    public GameObject EarthWallPrefab;

    private Image spellCD;
    private float currentSpellCD;

    Player realPlayer;
    GameObject copy;
    Vector3 Aim;

    public const float globalCooldown = 0.5f;

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
    void Start()
    {
        realPlayer = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
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
                realPlayer.StopDashEmitter();
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

       for (int i = 0; i < 4; i++)
       {
           spellCooldownUpdate(GetComponent<SpellInventory>().castSpell(i), i);
       }


        ControllerPluginWrapper.RefreshStates();
    }

    void castSpell(Spells cast, ref float setCooldown)
    {
        switch (cast)
        {
            case Spells.Empty:// if you don't have a spell in this slot

                break;
            case Spells.Fireball: // cast fireball spell
                copy = Instantiate(FireballPrefab, player.transform.position + player.transform.forward * 1.75f, player.transform.rotation) as GameObject;

                Fireball fireball = copy.GetComponent<Fireball>();
                fireball.owner = controller + 1;

                setCooldown = Fireball.cooldown;
                cooldown = globalCooldown;

                SoundEngineWrapper.QueueSound("fireball_shoot", 0, false, 10);
                break;

            case Spells.Bubbleshield: // cast bubble shield spell
                copy = Instantiate(BubbleShieldPrefab, player.transform.position, player.transform.rotation) as GameObject;
                copy.tag = player.tag;
                setCooldown = BubbleShield.cooldown;
                cooldown = globalCooldown;

                SoundEngineWrapper.QueueSound("shield_activate", 0, false, 14);
                break;

            case Spells.Boulder: // cast boulder spell
                copy = Instantiate(BoulderPrefab, player.transform.position + player.transform.forward * 1.75f, player.transform.rotation) as GameObject;

                Boulder boulder = copy.GetComponent<Boulder>();
                boulder.owner = controller + 1;

                setCooldown = Boulder.cooldown;
                cooldown = globalCooldown;

                SoundEngineWrapper.QueueSound("boulder_shoot", 0, false, 12);
                break;
            case Spells.Dash: // cast dash spell
                realPlayer.saveDirection();
                realPlayer.dashing = true;
                currentlyDashing = maxDashTimer;

                setCooldown = dashBaseCooldown;
                cooldown = globalCooldown;

                SoundEngineWrapper.QueueSound("player_dash", 0, false, 11);
                realPlayer.StartDashEmitter();
                break;

            case Spells.Lightning: // cast lightning spell
                copy = Instantiate(LightningPrefab, player.transform.position + player.transform.forward * 1.75f, player.transform.rotation) as GameObject;

                LightningParent lightning = copy.GetComponent<LightningParent>();
                lightning.owner = controller + 1;

                setCooldown = LightningParent.cooldown;
                cooldown = globalCooldown;
                break;

            case Spells.Hailstorm: // cast hailstorm spell
                copy = Instantiate(HailstormPrefab, player.transform.position + player.transform.forward * 1.75f, player.transform.rotation) as GameObject;

                Hailstorm hailstorm = copy.GetComponent<Hailstorm>();
                hailstorm.owner = controller + 1;

                setCooldown = Hailstorm.cooldown;
                cooldown = globalCooldown;
                break;

            case Spells.EarthWall:
                copy = Instantiate(EarthWallPrefab, player.transform.position + player.transform.forward * 4.0f, player.transform.rotation) as GameObject;

                Wall earthwall = copy.GetComponent<Wall>();
                earthwall.owner = controller + 1;

                setCooldown = Wall.cooldown;
                cooldown = globalCooldown;
                break;

            case Spells.FireRun:
                copy = Instantiate(FireRunPrefab, player.transform.position + player.transform.forward * 1.75f, player.transform.rotation) as GameObject;

                FireRun firerun = copy.GetComponent<FireRun>();
                firerun.owner = controller + 1;

                setCooldown = FireRun.cooldown;
                cooldown = globalCooldown;
                break;

            default:
                break;
        }
    }

    private void spellCooldownUpdate(Spells cast, int spellslot)
    {
        string userTag = gameObject.tag;
        
        spellCD = GameObject.Find("Player" + userTag + "UI").GetComponentsInChildren<Image>()[spellslot + 4];

        switch (spellslot)
        {
            case 0:
                currentSpellCD = spell1Cooldown;
                break;

            case 1:
                currentSpellCD = spell2Cooldown;
                break;

            case 2:
                currentSpellCD = spell3Cooldown;
                break;

            case 3:
                currentSpellCD = spell4Cooldown;
                break;

            default:
                break;
        }

        if (currentSpellCD <= 0.0f && cooldown > 0.0f && cast != Spells.Empty)
        {
            spellCD.fillAmount = cooldown / globalCooldown;
        }
        else
        {
            switch (cast)
            {
                case Spells.Empty:// if you don't have a spell in this slot
                    spellCD.fillAmount = 0;

                    break;
                case Spells.Fireball: // cast fireball spell
                    spellCD.fillAmount = currentSpellCD / Fireball.cooldown;

                    break;

                case Spells.Bubbleshield: // cast bubble shield spell
                    spellCD.fillAmount = currentSpellCD / BubbleShield.cooldown;

                    break;

                case Spells.Boulder: // cast boulder spell
                    spellCD.fillAmount = currentSpellCD / Boulder.cooldown;

                    break;
                case Spells.Dash: // cast dash spell
                    spellCD.fillAmount = currentSpellCD / dashBaseCooldown;

                    break;

                case Spells.Lightning: // cast lightning spell
                    spellCD.fillAmount = currentSpellCD / LightningParent.cooldown;

                    break;

                case Spells.Hailstorm: // cast hailstorm spell
                    spellCD.fillAmount = currentSpellCD / Hailstorm.cooldown;

                    break;

                case Spells.EarthWall:
                    spellCD.fillAmount = currentSpellCD / Wall.cooldown;

                    break;

                case Spells.FireRun:
                    spellCD.fillAmount = currentSpellCD / FireRun.cooldown;

                    break;

                default:
                    break;
            }
        }
    }

    public void resetSpellCooldown(int spell)
    {
        switch (spell)
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