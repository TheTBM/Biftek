﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellInventoryManager;
using UnityEngine.UI;

public class SpellInventory : MonoBehaviour
{
    private int id;
    Image[] spellImage = new Image[4];
   
    public Sprite Empty;
    public Sprite fireball;
    public Sprite bubbleShield;
    public Sprite boulder;
    public Sprite dash;
   

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            SpellInventoryPlugin.assignSpell(i, 0, Spells.Fireball);
            SpellInventoryPlugin.assignSpell(i, 1, Spells.Bubbleshield);
            SpellInventoryPlugin.assignSpell(i, 2, Spells.Empty);
            SpellInventoryPlugin.assignSpell(i, 3, Spells.Empty);

            int temp = i + 1;
            spellImage = GameObject.Find("Player" + temp + "UI").GetComponentsInChildren<Image>();
            spellImage[0].sprite = fireball;
            spellImage[1].sprite = bubbleShield;
            spellImage[2].sprite = Empty;
            spellImage[3].sprite = Empty;
        }
    }

    public void AssignID(int newid)
    {
        id = newid;
    }

    public Spells castSpell(int spellslot)
    {
        return SpellInventoryPlugin.castSpell(id, spellslot);
    }

    public void assignSpell(int spellslot, Spells newSpell)
    {
        int temp = id + 1;
        spellImage = GameObject.Find("Player" + temp + "UI").GetComponentsInChildren<Image>();



       switch (newSpell)
       {
           case Spells.Empty:
               spellImage[spellslot].sprite = Empty;
               break;
       
           case Spells.Fireball:
               spellImage[spellslot].sprite = fireball;
               break;
       
           case Spells.Bubbleshield:
               spellImage[spellslot].sprite = bubbleShield;
               break;
       
           case Spells.Boulder:
               spellImage[spellslot].sprite = boulder;
               break;
       
           case Spells.Dash:
               spellImage[spellslot].sprite = dash;
               break;
       
           default:
               break;
       }

        SpellInventoryPlugin.assignSpell(id, spellslot, newSpell);
    }
}
