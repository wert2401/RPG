using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Effect : ScriptableObject
{
    public Sprite Sprite;
    public int BaseCooldown;
    public int Cooldown;
    public int time;
    public int id;
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
        Debug.Log("Эффект тикнул");
    }

    public virtual void End()
    {
    }

    public virtual void WhenTurnStarted() { }
    public virtual void WhenTurnEnded() { }

    public virtual void WhenAttacking(CreatureNew Target) { }
    public virtual void WhenAttacked(CreatureNew Attacker) { }
    public virtual void WhenDealDamage(CreatureNew Target, int Damage) { }
    public virtual void WhenGetDamage(CreatureNew Dealer, int Damage) { }
    public virtual void WhenCasting(CreatureNew Target) { }
    public virtual void WhenSpelled(CreatureNew Caster) { }

}
