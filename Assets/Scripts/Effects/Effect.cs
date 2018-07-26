using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Effect : ScriptableObject
{
    public int time;
    public bool onPlayer;
    public bool stackable;
    public string EffectName;
    public virtual void Get()
    {
    }
    public virtual void Tick()
    {
        time--;
        if (time <= 0)
            End();
    }
    public virtual void End()
    {
    }
    public virtual void FunctionOnStartAttack(){}
    public virtual void FunctionOnStartBeingAttacked() {}
    public virtual void FunctionOnStartUsingSpell() {}
    public virtual void FunctionOnStartBeingSpelled() {}
    public virtual void FunctionOnStartDealingDamage() {}
    public virtual void FunctionOnStartBeingDamaged() {}

    public virtual void FunctionOnEndAttack() { }
    public virtual void FunctionOnEndBeingAttacked() { }
    public virtual void FunctionOnEndUsingSpell() { }
    public virtual void FunctionOnEndBeingSpelled() { }
    public virtual void FunctionOnEndDealingDamage() { }
    public virtual void FunctionOnEndBeingDamaged() { }

    public virtual void FunctionOnBattleStarting() { }
    public virtual void FunctionOnBattleEnding() { }
}
