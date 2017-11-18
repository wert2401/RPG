using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class Spell : ScriptableObject
{
    [Header("Words")]
    public string words;
    public string SpellWords;
    public int SpellType;
    public virtual void SpellUse()
    { }
    public virtual void BuffUse()
    { }
}
