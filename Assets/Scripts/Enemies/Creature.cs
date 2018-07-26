using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class Creature : ScriptableObject
{
    public string CrName;
   
    public int lvl;
    public float maxHealth;
    public float health;
    public float ManaMax;
    public float mana;
    public float damage;
    public float armor;
	public float accuracy;
	public float evasChance;
	public float CH;
	public float CD;
    public bool isAlive = true;
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
        UIManager.instance.RemoveAllButtons(UIManager.instance.dynButtonsHolder.transform);
        SetDynUI();
        SetEnemyUI();
        UIManager.instance.AddDynButton("Назад",Back, UIManager.instance.dynButtonsHolder.transform);
    }

    public void EnManaLost(Buff _buff)
    {
        if (GameLogic.instance.enemy.mana < _buff.ManaCost)
        {
            UIManager.instance.Print("Противнику не хватает маны");
            GameLogic.instance.enemy.health -= (GameLogic.instance.enemy.mana);
            GameLogic.instance.enemy.mana = 0;
            _buff.TF = false;
        }
        else
            _buff.TF = true;
    }

    virtual public void Talk()
    {
        //SetDynUI();
        ResetUI();
    }
    public void Back()
    {
        UIManager.instance.SetDynButtonsOn(false);
        UIManager.instance.SetFightButtonsOn(true);
        UIManager.instance.RemoveAllButtons(UIManager.instance.dynButtonsHolder.transform);
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
    virtual public void React()
    {
    }
    public virtual void GetDrop()
    {
        int a = Random.Range(0, drop.Count-1);
        Item item = drop[a];
        if (item != null)
        {
            InventoryManager.instance.AddItem(item);
        }
    }

    public virtual int GetMoney()
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
