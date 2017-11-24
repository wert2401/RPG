using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public PlayerDefaultStats pds;
    public string plName;
    public float startHealth;
    public float ManaMax;
	public float maxHealth
    {
        get { return startHealth * strenght;  }
    }
	public float accuracy=5;
	public float evasChance=5;
	public float damage;
    public float armor;
	public int healPotion = 5;
    public int money = 100;

	public float CH=5;
	public float CD=1;
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

    public delegate void OnStatsChanged();
    public OnStatsChanged onStatsChanged;

    public void AddExp(int expGain)
    {
        exp += expGain;
        while (exp >= needExp)
        {
            exp -= needExp;
            AddLevel();
        }
        onStatsChanged.Invoke();
    }

    private void AddLevel()
    {
        lvl++;
        freePoints += 5;
        needExp += (needExp * 20 / 100);
        GameLogic.instance.plHealth = maxHealth;
        onStatsChanged.Invoke();
    }

    public void IncreaseStat(int type)
    {
        if (freePoints <= 0)
        {
            return;
        }
        switch (type)
        {
			case 0:
				strenght += 1;
				damage += 0.2f;
				CD += 0.01f;
                GameLogic.instance.plHealth += startHealth;
                break;
			case 1:
				agility += 1;
				evasChance += 1;
				CH += 1;
                break;
            case 2:
                intelligence += 1;
                break;
            case 3:
                concentration += 1;
				accuracy += 1;
                break;
        }
        freePoints--;
        onStatsChanged.Invoke();
    }

    public void ResetStats()
    {
        damage = pds.damage;
        armor = pds.armor;
        money = pds.money;
        lvl = pds.lvl;
        exp = pds.exp;
		GameLogic.instance.plHealth = pds.maxHealth;
        GameLogic.instance.plMana = pds.ManaMax;
        needExp = pds.needExp;
        freePoints = pds.freePoints;
        strenght = pds.strenght;
        agility = pds.agility;
        intelligence = pds.intelligence;
        concentration = pds.concentration;
        if (onStatsChanged !=null)
        onStatsChanged.Invoke();

        fireDmg = pds.fireDmg;
        waterDmg = pds.waterDmg;
        airDmg = pds.airDmg;
        lightDmg = pds.lightDmg;
        darkDmg = pds.darkDmg;
        earthDmg = pds.earthDmg;

        GameLogic.instance.buffs.Clear();
        GameLogic.instance.BTs.Clear();

        fireRes = pds.fireRes;
        waterRes = pds.waterRes;
        airRes = pds.airRes;
        lightRes = pds.lightRes;
        darkRes = pds.darkRes;
        earthRes = pds.earthRes;
    }
}
