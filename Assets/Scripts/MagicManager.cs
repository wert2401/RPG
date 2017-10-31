using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicManager : MonoBehaviour {
    public static MagicManager instance;
    public List<Spell> spells;

    private void Awake()
    {
        instance = this;
    }

    public Spell CheckSpell(string words)
    {
        foreach (Spell spell in spells)
        {
            if (words == spell.words)
            {
                return spell;
            }
        }
        return null;
    }
}
