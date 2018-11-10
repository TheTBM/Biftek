using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerInputs;
using UnityEngine.UI;
using SoundEnginePluginWrapper;

public class Player : MonoBehaviour
{
    //Player variables
    public int health;
	int previousHealth;
    public int lives;
    public float velocity;
    Info info;
    SpellInventory spells;
    Vector3 direction;
    Vector3 lookDirection;
    Vector3 dashDirection;
    bool initiated = false;
    int controller;
    int points;
    private float respawnTimer;
    public float fireDamageTimer = 0;

    private float speedModifier = 1.0f;

    bool alive;
    public bool dashing;

    public Material dead;
    Vector3 deadPosition;

	public ParticleSystem dashingEmitter;
	private ParticleSystem deCopy;

	public ParticleSystem pickupEmitter;
	private ParticleSystem peCopy;

	// Use this for initialization
	void Start()
    {
        info = GetComponent<Info>();
        spells = GetComponent<SpellInventory>();
        ControllerPluginWrapper.Initiate();
        deadPosition = new Vector3(0, -10, 0);
        alive = true;
		respawnTimer = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        ControllerPluginWrapper.UpdateControllers();

        if (!initiated)
        {
            initiate();
        }

		if ((previousHealth != info.getHealth()) && (info.getHealth() > 0))
		{
<<<<<<< HEAD
			SoundEngineWrapper.QueueSound("player_hurt", 0, false, 11);
=======
			//SoundEngineWrapper.QueueSound("player_hurt", 0, false, 11);
>>>>>>> 110a58946bc9913bad69f66d44c0ebaf6754e24c
		}

		if (info.getHealth() <= 0 && alive)
        {
            kill();
        }

        int.TryParse(gameObject.tag, out controller);
        controller -= 1;
        spells.AssignID(controller);

        fireDamageTimer += Time.deltaTime;

        if (!dashing)
        {
            //resets player direction before recalculating it
            direction.Set(0, 0, 0);

            if (!ControllerPluginWrapper.LStick_InDeadZone(controller))
            {
                direction.x = ControllerPluginWrapper.LeftStick_X(controller);
                direction.z = ControllerPluginWrapper.LeftStick_Y(controller);
            }

            transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 0.00001f));

            //transform.Translate(direction * velocity * Time.deltaTime);
            if (transform.GetComponent<Rigidbody>().velocity.magnitude < 10.0f * speedModifier)
            {
                transform.GetComponent<Rigidbody>().AddForce(direction * Mathf.Pow(velocity * speedModifier, 2));
            }

            if (transform.GetComponent<Rigidbody>().velocity.magnitude > 10.0f * speedModifier)
            {
                transform.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity.normalized * speedModifier * 10.0f;
            }

            if (!ControllerPluginWrapper.RStick_InDeadZone(controller))
            {
                lookDirection.x = ControllerPluginWrapper.RightStick_X(controller);
                lookDirection.z = ControllerPluginWrapper.RightStick_Y(controller);
            }
            if (lookDirection != new Vector3(0, 0, 0))
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
            else
            {
                //  transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 0.00001f));
            }
        }
        else
        {
            if (!ControllerPluginWrapper.RStick_InDeadZone(controller))
            {
                lookDirection.x = ControllerPluginWrapper.RightStick_X(controller);
                lookDirection.z = ControllerPluginWrapper.RightStick_Y(controller);
            }

            GetComponent<Rigidbody>().velocity = dashDirection * velocity * 3;

            SyncEmitters();
		}

        //make sure the player speed gets reset to a normal value
        resetSpeed();

        updatehpbar();

        ControllerPluginWrapper.RefreshStates();

		previousHealth = info.getHealth();
    }

    public void kill()
    {
        alive = false;
        info.loseLive();
        gameObject.GetComponent<Renderer>().material = dead;
        Color temp = GetComponentInChildren<SpriteRenderer>().color;
        temp.a = 0.0f;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = temp;
        gameObject.transform.SetPositionAndRotation(deadPosition, gameObject.transform.rotation);
<<<<<<< HEAD
		SoundEngineWrapper.QueueSound("player_die", 0, false, 11);
=======
		//SoundEngineWrapper.QueueSound("player_die", 0, false, 11);

		print(respawnTimer);
>>>>>>> 110a58946bc9913bad69f66d44c0ebaf6754e24c
	}

	public void respawn(Material m, GameObject location)
    {
        alive = true;
        info.setHealth(health);
        gameObject.GetComponent<Renderer>().material = m;
        Color temp = GetComponentInChildren<SpriteRenderer>().color;
        temp.a = 0.2f;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = temp;
        gameObject.transform.SetPositionAndRotation(location.transform.position, location.transform.rotation);
<<<<<<< HEAD
		SoundEngineWrapper.QueueSound("player_respawn", 0, false, 11);
=======
		//SoundEngineWrapper.QueueSound("player_respawn", 0, false, 11);
		previousHealth = info.getHealth();
>>>>>>> 110a58946bc9913bad69f66d44c0ebaf6754e24c
	}

    void resetSpeed()
    {
        speedModifier = 1.0f;
    }

    public void addSlow(float slow)
    {
        speedModifier -= speedModifier * slow;
    }

    public void addSpeed(float speed)
    {
        speedModifier += speedModifier * speed;
        Debug.Log(speedModifier);
    }

    public void saveDirection()
    {
        dashDirection = transform.forward;
        Debug.Log(transform.forward);
    }

    private void updatehpbar()
    {
        int temp = controller + 1;
        Slider healthSlider = GameObject.Find("Player" + temp + "UI").GetComponentInChildren<Slider>();
        healthSlider.value = ((float)info.getHealth() / (float)health) * 100.0f;
    }

    public bool isAlive()
    {
        return alive;
    }

    public void addPoints(int p)
    {
        points += p;
    }

    public int getPoints()
    {
        return points;
    }

    public int getHealth()
    {
        return info.getHealth();
    }

    public void resetRespawnTimer()
    {
        info.setTimer(respawnTimer);
    }

    public void decreaseRespawnTimer(float t)
    {
        info.decreaseTimer(t);
    }

    public float getRespawnTimer()
    {
        return info.getTimer();
    }

    public void initiate()
    {
        info.setHealth(health);
        info.resetLives(lives);
        info.setPlayer(true);
        initiated = true;
		previousHealth = info.getHealth();
    }

    public float getFireDamageTimer()
    {
        return fireDamageTimer;
    }

    public void resetFireDamageTimer()
    {
        fireDamageTimer = 0.0f;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Info>() != null && dashing)
        {
            Info otherInfo = other.gameObject.GetComponent<Info>();

            if (otherInfo.getPlayer())
            {
                Player enemyPlayer = other.gameObject.GetComponent<Player>();
                otherInfo.takeDamage(1);

                if (otherInfo.getHealth() <= 0)
                {
                    enemyPlayer.resetRespawnTimer();
                    addPoints(1);
                }
            }
            else
            {
                otherInfo.takeDamage(1);
            }
        }

        dashing = false;
		StopDashEmitter();
		SoundEngineWrapper.QueueSound("player_dash_hit", 0, false, 11);
	}

	public void StartDashEmitter()
	{
		deCopy = Instantiate(dashingEmitter, transform.position, transform.rotation * new Quaternion(0, -1, 0, 0)) as ParticleSystem;
	}

	public void StopDashEmitter()
	{
		deCopy.Stop(true, ParticleSystemStopBehavior.StopEmitting);
		deCopy.GetComponent<GeneralEmitter>().KillEmitter();
	}

	private void SyncEmitters()
	{
		deCopy.transform.SetPositionAndRotation(transform.position, transform.rotation * new Quaternion(0, 1, 0, 0));
	}

	void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PickUp" && ControllerPluginWrapper.LStick_InDeadZone(controller))
        {
            GetComponent<PlayerSpells>().triggerGlobalCooldown();
            //R1
            if (ControllerPluginWrapper.GetButtonPressed(controller, 9))
            {
                GetComponent<SpellInventory>().assignSpell(0, other.GetComponent<PickUp>().getSpell());
                GetComponent<PlayerSpells>().resetSpellCooldown(0);
                Destroy(other.gameObject);
				peCopy = Instantiate(pickupEmitter, other.transform.position - new Vector3(1, 0, 0), other.transform.rotation) as ParticleSystem;
				peCopy.GetComponent<GeneralEmitter>().KillEmitter();
				SoundEngineWrapper.QueueSound("player_pickup", 0, false, 11);
			}

			//L1
			if (ControllerPluginWrapper.GetButtonPressed(controller, 8))
            {
                GetComponent<SpellInventory>().assignSpell(1, other.GetComponent<PickUp>().getSpell());
                GetComponent<PlayerSpells>().resetSpellCooldown(1);
                Destroy(other.gameObject);
				peCopy = Instantiate(pickupEmitter, other.transform.position - new Vector3(1, 0, 0), other.transform.rotation) as ParticleSystem;
				peCopy.GetComponent<GeneralEmitter>().KillEmitter();
				SoundEngineWrapper.QueueSound("player_pickup", 0, false, 11);
			}

			if (ControllerPluginWrapper.LeftTrigger(controller) >= 0.3f)
            {
                GetComponent<SpellInventory>().assignSpell(2, other.GetComponent<PickUp>().getSpell());
                GetComponent<PlayerSpells>().resetSpellCooldown(2);
                Destroy(other.gameObject);
				peCopy = Instantiate(pickupEmitter, other.transform.position - new Vector3(1, 0, 0), other.transform.rotation) as ParticleSystem;
				peCopy.GetComponent<GeneralEmitter>().KillEmitter();
				SoundEngineWrapper.QueueSound("player_pickup", 0, false, 11);
			}

			if (ControllerPluginWrapper.RightTrigger(controller) >= 0.3f)
            {
                GetComponent<SpellInventory>().assignSpell(3, other.GetComponent<PickUp>().getSpell());
                GetComponent<PlayerSpells>().resetSpellCooldown(3);
                Destroy(other.gameObject);
				peCopy = Instantiate(pickupEmitter, other.transform.position - new Vector3(1, 0, 0), other.transform.rotation) as ParticleSystem;
				peCopy.GetComponent<GeneralEmitter>().KillEmitter();
				SoundEngineWrapper.QueueSound("player_pickup", 0, false, 11);
			}
		}
    }
}