using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Spells spellName = Spells.Empty;

    public Sprite Fireball, Shield, Boulder, Dash, Lightning, Hailstorm, FireRun, EarthWall;

    public int spawnLocation;

	// Use this for initialization
	void Start ()
    {
        int num = Random.Range(1, 9);

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

            case 5:
                spellName = Spells.Lightning;
                GetComponentInChildren<SpriteRenderer>().sprite = Lightning;
                break;

            case 6:
                spellName = Spells.Hailstorm;
                GetComponentInChildren<SpriteRenderer>().sprite = Hailstorm;
                break;

            case 7:
                spellName = Spells.FireRun;
                GetComponentInChildren<SpriteRenderer>().sprite = FireRun;
                break;

            case 8:
                spellName = Spells.EarthWall;
                GetComponentInChildren<SpriteRenderer>().sprite = EarthWall;
                break;

            default:
                break;
        }

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
