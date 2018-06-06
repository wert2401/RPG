using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "NewATK", menuName = "RPG/Spell/ATK")]
public class AttackSpell : Spell
{
    [Header("DMG")]
    public float fireDmg;
    public float waterDmg;
    public float airDmg;
    public float lightDmg;
    public float darkDmg;
    public float earthDmg;
    public override void SpellUse()
    {
        ManaLost();
        if (!TF)
        {
            UIManager.instance.SetSpellScreenOn();
            GameLogic.instance.React();
            return;
        }
        UIManager.instance.Print(SpellWords);
        GameLogic.instance.enemy.health -= (GameLogic.instance.enemy.airRes * airDmg) + (GameLogic.instance.enemy.fireRes * fireDmg) + (GameLogic.instance.enemy.darkRes * darkDmg) + (GameLogic.instance.enemy.waterRes * waterDmg) + (GameLogic.instance.enemy.lightRes * lightDmg) + (earthDmg * GameLogic.instance.enemy.earthRes);
        UIManager.instance.SetSpellScreenOn();
        GameLogic.instance.React();
    }

}