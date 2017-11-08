using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefStats", menuName = "RPG/Player/PlayerStats")]
public class PlayerDefaultStats : ScriptableObject {
    public string plName;
    public float startHealth;
    public float maxHealth
    {
        get { return startHealth * strenght; }
    }
    public float accuracy = 5;
    public float evasChance = 5;
    public float damage;
    public float armor;
    public int healPotion = 5;
    public int money = 100;

    public float CH = 5;
    public float CD = 1;
    [Header("Elements Damage")]
    public float fireDmg;
    public float waterDmg;
    public float airDmg;
    public float lightDmg;
    public float darkDmg;
    public float earthDmg;

    [Header("Elements Resist/Vulnerability ")]
    public float fireRes;
    public float waterRes;
    public float airRes;
    public float lightRes;
    public float darkRes;
    public float earthRes;

    [Header("Characterirstics")]
    public int lvl = 1;
    public int exp;
    public int needExp;
    public int freePoints;
    public int strenght;
    public int agility;
    public int intelligence;
    public int concentration;
}
