using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Spells spellName = Spells.Empty;

    public Sprite Fireball, Shield, Boulder, Dash;

    public int spawnLocation;

	// Use this for initialization
	void Start ()
    {
        int num = Random.Range(1, 5);

        switch (num)
        {
            case 1:
                spellName = Spells.Fireball;
                GetComponentInChildren<SpriteRenderer>().sprite = Fireball;
                break;
            case 2:
                spellName = Spells.Bubbleshield;
                GetComponentInChildren<SpriteRenderer>().sprite = Shield;
                break;

            case 3:
                spellName = Spells.Boulder;
                GetComponentInChildren<SpriteRenderer>().sprite = Boulder;
                break;

            case 4:
                spellName = Spells.Dash;
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

    public Spells getSpell()
    {
        return spellName;
    }

    void OnDestroy()
    {
        GameObject.Find("Spell Spawn Manager").GetComponent<SpellSpawn>().setSpawnFreeTrue(spawnLocation);
    }
}
