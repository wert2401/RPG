using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Types;
[CreateAssetMenu(fileName = "NewBuff", menuName = "RPG/Spell/Buff")]
public class Buff : Spell
{
    [Header("Buff")]
    public bool onEnemy;
    public float physBuff;
    public float fireBuff;
    public float waterBuff;
    public float airBuff;
    public float lightBuff;
    public float darkBuff;
    public float earthBuff;
    public int buffTime;
    public override void SpellUse()
    {
        if(onEnemy)
            GameLogic.instance.EnBuffUse(this, false);
        else
            GameLogic.instance.BuffUse(this, false);
        GameLogic.instance.React();
    }
}