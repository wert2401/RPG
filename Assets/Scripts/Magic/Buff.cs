using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Types;
[CreateAssetMenu(fileName = "NewBuff", menuName = "RPG/Spell/Buff")]
public class Buff : Spell
{
    [Header("Buff")]
    public float fireBuff;
    public float waterBuff;
    public float airBuff;
    public float lightBuff;
    public float darkBuff;
    public float earthBuff;
    public int buffTime;
    public override void SpellUse()
    {
        if (GameLogic.instance.BT > 0)
        {
            UIManager.instance.Print("Ваше оружие уже зачаровано");
            UIManager.instance.Print("Это было бесполезно");
            return;
        }
        else
        {
            UIManager.instance.Print(SpellWords);
            GameLogic.instance.player.airDmg += airBuff;
            GameLogic.instance.player.earthDmg += earthBuff;
            GameLogic.instance.player.fireDmg += fireBuff;
            GameLogic.instance.player.waterDmg += waterBuff;
            GameLogic.instance.player.lightDmg += lightBuff;
            GameLogic.instance.player.darkDmg += darkBuff;
            GameLogic.instance.BT = buffTime;
            GameLogic.instance.buff = this;
        }
        UIManager.instance.SetSpellScreenOn();
        GameLogic.instance.React();
    }
    //public override void BuffUse()
    //{
    //    if (BuffTime>0)
    //    {
    //        BuffTime -= 1;
    //        if(BuffTime==0)
    //        {
    //            GameLogic.instance.player.airDmg -= airBuff;
    //            GameLogic.instance.player.earthDmg -= earthBuff;
    //            GameLogic.instance.player.fireDmg -= fireBuff;
    //            GameLogic.instance.player.waterDmg -= waterBuff;
    //            GameLogic.instance.player.lightDmg -= lightBuff;
    //            GameLogic.instance.player.darkDmg -= darkBuff;
    //        }
    //    }
    //}

}