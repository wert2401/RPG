using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsHolder : MonoBehaviour {
    public int enLvl;
    public float curHealth;
    public float curMana;
    public float damage;
    public float armor;
    public float accuracy;
    public float evasChance;
    public float CH;
    public float CD;
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

    public void SetEnemy(Creature enemy)
    {
        curHealth = enemy.maxHealth;
        curMana = enemy.ManaMax;
        damage = enemy.damage;
        armor = enemy.armor;
        accuracy = enemy.accuracy;
        evasChance = enemy.evasChance;
        CH = enemy.CH;
        CD = enemy.CD;

        fireDmg = enemy.fireDmg;
        waterDmg = enemy.waterDmg;
        airDmg = enemy.airDmg;
        lightDmg = enemy.lightDmg;
        darkDmg = enemy.darkDmg;
        earthDmg = enemy.earthDmg;

        fireRes = enemy.fireRes;
        waterRes = enemy.waterRes;
        airRes = enemy.airRes;
        lightRes = enemy.lightRes;
        darkRes = enemy.darkRes;
        earthRes = enemy.earthRes;
    }
}
