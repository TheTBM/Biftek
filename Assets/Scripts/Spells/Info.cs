using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
	private int health; // health
    private int lives;
	private bool player; //is it a player?
	private int damage; //damage
	private float timer;

	// Use this for initialization
	void Start ()
	{
		health = 0;
		player = false;
		damage = 0;
	}

	public void takeDamage(int dmg)
	{
		health -= dmg;
	}

	//change health
	public void setHealth(int x)
	{
		health = x;
	}

    //player loses one live
    public void loseLive()
    {
        lives -= 1;
    }

    //return lives
    public bool isDead()
    {
        if (lives <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //player lives get reset
    public void resetLives(int stock)
    {
        lives = stock;
    }

	//return health
	public int getHealth()
	{
		return health;
	}

	//is it a player?
	public void setPlayer(bool y)
	{
		player = y;
	}

	//return if its a player
	public bool getPlayer()
	{
		return player;
	}

	//set damage
	public void setDamage(int x)
	{
		damage = x;
	}

	//return damage
	public int getDamage()
	{
		return damage;
	}

	public void setTimer(float t)
	{
		timer = t;
	}

	public void decreaseTimer(float t)
	{
		timer -= t;
	}

	public float getTimer()
	{
		return timer;
	}
}
