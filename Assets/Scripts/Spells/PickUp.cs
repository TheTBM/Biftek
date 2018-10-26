using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum SpellName
    {
        Empty, Fireball, Shield, Boulder, Dash
    }

    private SpellName spellName = SpellName.Empty;

    public Sprite Fireball, Shield, Boulder, Dash;

    public int spawnLocation;

	// Use this for initialization
	void Start ()
    {
        int num = Random.Range(1, 4);

        switch (num)
        {
            case 1:
                spellName = SpellName.Fireball;
                GetComponentInChildren<SpriteRenderer>().sprite = Fireball;
                break;
            case 2:
                spellName = SpellName.Shield;
                GetComponentInChildren<SpriteRenderer>().sprite = Shield;
                break;

            case 3:
                spellName = SpellName.Boulder;
                GetComponentInChildren<SpriteRenderer>().sprite = Boulder;
                break;

            case 4:
                spellName = SpellName.Dash;
                GetComponentInChildren<SpriteRenderer>().sprite = Dash;
                break;

            default:
                break;
        }
	}

	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnCollisionEnter(Collision other)
    {
        
    }

    void OnDestroy()
    {
        GameObject.Find("Spell Spawn Manager").GetComponent<SpellSpawn>().setSpawnFreeTrue(spawnLocation);
    }
}
