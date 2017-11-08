using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class Enemy : ScriptableObject {
    public string enemyName;
   
    public int enLvl;
    public float maxHealth;
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
    [Header("Enemy`s death")]
    public int expGain;
    public int minMoneyGain;
    public int maxMoneyGain;
    public List<Item> drop;

 #region Actions
    virtual public void Interact()
    {
        SetDynUI();
    }

    virtual public void Attack()
    {
        GameLogic.instance.Attack();
        ResetUI();
    }

    virtual public void Talk()
    {
        SetDynUI();
        ResetUI();
    }
	virtual public void React1()
	{
	}
	virtual public void React2()
	{
	}
	virtual public void React3()
	{
	}

    public Item GetRandomDrop()
    {
        Item item;
        int a = Random.Range(0, drop.Count-1);
        //Debug.Log(a);
        item = drop[a];
        return item;
    }

    public int GetRandomMoney()
    {
        int mGain;
        mGain = Random.Range(minMoneyGain, maxMoneyGain);
        return mGain;
    }
    #endregion

 #region UI
    virtual public void SetEnemyUI()
    {

    }

    public void ResetUI()
    {
        UIManager.instance.SetExtrDynButtonsOn(false);
        UIManager.instance.SetDynButtonsOn(false);
        UIManager.instance.SetFightButtonsOn(true);
    }

    public void SetDynUI()
    {
        UIManager.instance.SetFightButtonsOn(false);
        UIManager.instance.SetDynButtonsOn(true);      
        UIManager.instance.SetExtrDynButtonsOn(false);
    }

    public void SetExtrDynUI()
    {
        UIManager.instance.SetFightButtonsOn(false);
        UIManager.instance.SetDynButtonsOn(false);
        UIManager.instance.SetExtrDynButtonsOn(true);
    }
    #endregion
}
