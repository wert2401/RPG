using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewCreature", menuName = "RPG/Creature")]

public class CreatureNew : ScriptableObject
{
    public Sprite Sprite;
    public string Name;
    public string Description;
    public int Health;
    public int HealthMax;
    public int Mana;
    public int ManaMax;
    public int Energy;
    public int EnergyMax;
    public int accuracy = 5;
    public int evasChance = 5;
    public int damage;
    public int armor;
    public List<Ability> Abilities;
    public List<Effect> Effects;

    public int CH = 5;
    public int CD = 1;
    [Header("Elements Damage")]
    public int fireDmg;
    public int waterDmg;
    public int airDmg;
    public int lightDmg;
    public int darkDmg;
    public int earthDmg;
    public int pureDmg;

    [Header("Elements Resist/Vulnerability ")]
    public int fireRes;
    public int waterRes;
    public int airRes;
    public int lightRes;
    public int darkRes;
    public int earthRes;

    [Header("Characterirstics")]
    public int lvl;
    public int strenght;
    public int agility;
    public int intelligence;
    public int concentration;

    public void WasAttacked(CreatureNew Attacker)
    {
        for (int i = 0; i < Effects.Count; i++)
        {
            Effects[i].WhenAttacked(Attacker);
        }
    }
    public void Attacking(CreatureNew Target)
    {
        for (int i = 0; i < Effects.Count; i++)
        {
            Effects[i].WhenAttacking(Target);
        }
    }
    public void GetDamage(CreatureNew Dealer, int Damage)
    {
        for (int i = 0; i < Effects.Count; i++)
        {
            Effects[i].WhenGetDamage(Dealer, Damage);
        }
    }
    public void DealDamage(CreatureNew Target, int Damage)
    {
        for (int i = 0; i < Effects.Count; i++)
        {
            Effects[i].WhenGetDamage(Target, Damage);
        }
    }
}