using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public enum Spells
{
    Empty, Fireball, Boulder, Dash, Bubbleshield, Lightning, Hailstorm, EarthWall, FireRun
}

namespace SpellInventoryManager
{
    public class SpellInventoryPlugin : MonoBehaviour
    {
        const string DLL_NAME = "SpellInventory";

        [DllImport(DLL_NAME)]
        public static extern Spells castSpell(int id, int spellslot);

        [DllImport(DLL_NAME)]
        public static extern void assignSpell(int id, int spellslot, Spells newSpell);
    }
}
