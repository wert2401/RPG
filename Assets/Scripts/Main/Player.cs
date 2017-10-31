using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public string plName;
    public float startHealth;
    public float maxHealth
    {
        get { return startHealth * strenght;  }
    }
    public float damage;
    public float fireDmg;
    public float waterDmg;
    public float airDmg;
    public float earthDmg;

    public int healPotion = 5;

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
            int a = exp - needExp;
            exp = a;
            AddLevel();
            return;
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
                GameLogic.instance.plHealth += startHealth;
                break;
            case 1:
                agility += 1;
                break;
            case 2:
                intelligence += 1;
                break;
            case 3:
                concentration += 1;
                break;
        }
        freePoints--;
        onStatsChanged.Invoke();
    }

    public void ResetStats()
    {
        lvl = 1;
        exp = 0;
		GameLogic.instance.plHealth = 50;
        needExp = 100;
        freePoints = 5;
        strenght = 5;
        agility = 5;
        intelligence = 5;
        concentration = 5;
        onStatsChanged.Invoke();
    }
}
