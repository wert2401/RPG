﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {
    [HideInInspector]
    public static GameLogic instance;
    public Location startLocation;
    public Location curLoc;
    [Header("Player")]
    public Player player;
    public float plHealth;
    float plDmg;

    [Header("Enemy")]
    public Enemy enemy;
    public float enHealth;
    float enDmg;
    public bool canRun = true;
	int stun=0;
    [Header("Magic")]
    public Text spellText;
    Spell spell;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (startLocation != null)
        {
            UIManager.instance.SetLocationUI(startLocation);
            curLoc = startLocation;
        }
        else
            Debug.Log("Set start location!");

        plHealth = player.maxHealth;
    }

#region Location
    //private void SetLocationUI(Location loc)
    //{
    //    curLoc = loc;
    //    UIManager.instance.SetLocationScreenOn(true);
    //    UIManager.instance.SetLocationName(curLoc.locationName);

    //    if (curLoc.locations.Count > 0)
    //    {
    //        for (int i = 0; i < curLoc.locations.Count; i++)
    //        {
    //            Location _loc = curLoc.locations[i];
    //            UIManager.instance.AddLocationButton(_loc.locationName, i);
    //            Debug.Log(_loc.locationName + " " + i);
    //        }
    //    }

    //    if (curLoc.dungeons.Count > 0)
    //    {
    //        for (int i = 0; i < curLoc.locations.Count; i++)
    //        {
    //            Dungeon _dun = curLoc.dungeons[i];
    //            UIManager.instance.AddDungeonButton(_dun.dungeonName, i);
    //        }
    //    }
    //}

    public void MoveToLocation(int id)
    {
        if (curLoc == null) return;

        //curLoc = curLoc.locations[id];
        UIManager.instance.SetLocationUI(curLoc.locations[id]);
        curLoc = curLoc.locations[id];
    }

    public void EnterTheDungeon(int id)
    {
        if (curLoc == null) return;

        enemy = curLoc.dungeons[id].GetRandomEnemy();
        UIManager.instance.SetLocationScreenOn(false);
        StartFight();
    }

    public void MoveToShop(int id)
    {
        if (curLoc == null) return;

        ShopManager.instance.SetShopUI(curLoc.shops[id]);
    }
 #endregion

#region Fight
    private void StartFight()
    {
        if (enemy == null) return;
		plDmg = InventoryManager.instance.GetItemsDamage ();
        enHealth = enemy.maxHealth;
        //plHealth = player.maxHealth;
        UIManager.instance.SetFightUI(enemy, player);
        UIManager.instance.UpdateHealth(enHealth, plHealth);
    }

    public void StopFight(bool ran)
    {
        UIManager.instance.SetLocationUI(startLocation);
        curLoc = startLocation;
        UIManager.instance.SetFightScreenOn(false);

        if (plHealth <= 0)
        {
            player.ResetStats();
			UIManager.instance.SetStartScreenOn();
			UIManager.instance.SetDiedScreenOn();
			UIManager.instance.ClearLog ();
            InventoryManager.instance.ResetInv();
            return;
        }

        int expG = enemy.expGain * (enemy.enLvl / player.lvl);
        if (ran)
        {  
			if (enemy.enLvl - player.lvl >= 20)
				expG = 0;
            expG = expG / 10;
        }
        player.AddExp(expG);

        Item itemGain = enemy.GetRandomDrop();
        if (itemGain != null)
            InventoryManager.instance.AddItem(itemGain);
    }

    public void Attack()
    {
		PlayerAttack ();
		React ();
    }

    public void Block()
    {
        float a = Random.value;
        float c = 0.1f + player.agility * 0.01f;
        if (a < c)
        {
            a = Random.value;
            c = 0.05f + (player.agility/2) * 0.01f;
            if (a < c)
            {
                UIManager.instance.Print("Вам удается заблокировать удар и контратаковать");
                PlayerAttack();
            }
            else
            {
                UIManager.instance.Print("Вам удается заблокировать удар");
            }
        }
        else
        {
            UIManager.instance.Print("Вам не удается заблокировать удар");
			EnemyAttack ();
        }
    }
	public int bufftime;
    public void Spell()
    {
        Debug.Log("Spell");
        spell = MagicManager.instance.CheckSpell(spellText.text);

		if (enemy == null || spell == null) 
		{
			UIManager.instance.Print("Но ничего не происходит");
			UIManager.instance.SetSpellScreenOn ();
			React ();
			return;
		}
		if (spell.spelltype=="attack")
		enHealth -= (enemy.airRes * spell.airDmg) + (enemy.fireRes * spell.fireDmg) + (enemy.darkRes * spell.darkDmg) + (enemy.waterRes * spell.waterDmg) + (enemy.lightRes * spell.lightDmg)+(spell.earthDmg*enemy.earthRes);
		if (spell.spelltype == "buff") 
		{
			if (bufftime > 0) 
			{
				UIManager.instance.Print ("Ваше оружие уже зачаровано");
				UIManager.instance.Print ("Это было бесполезно");
				React ();
				UIManager.instance.SetSpellScreenOn ();
				return;
			}
			else 
			{
				UIManager.instance.Print(spell.SpellWords);
				bufftime=spell.buffTime;
				player.airDmg += spell.airBuff;
				player.earthDmg += spell.earthBuff;
				player.fireDmg += spell.fireBuff;
				player.waterDmg += spell.waterBuff;
				player.lightDmg += spell.lightBuff;
				player.darkDmg += spell.darkBuff;
			}
		}
		UIManager.instance.Print("Вы успешно применяете заклинание");
		UIManager.instance.UpdateHealth(enHealth,plHealth);
		UIManager.instance.SetSpellScreenOn ();
		React ();
	}

    public void UseHeal()
    {
        float a = player.maxHealth - plHealth;
        float b = 36;
        if (player.healPotion <= 0)
        {
            UIManager.instance.Print("У вас нет зелий лечения");
            return;
        }
        if (plHealth >= player.maxHealth)
        {
            UIManager.instance.Print("Здоров как бык");
            return;
        }
        if (a < 36)
        {
            b = a;
        }
        plHealth += b;
        player.healPotion--;
        UIManager.instance.UpdateHealth(enHealth,plHealth);
        UIManager.instance.Print("Вы излечились");
    }

    public void Run()
    { 
        if (!canRun)
        {
            Debug.Log("You can`t run now!");
            return;
        }

        Debug.Log("Try to run");
        int a = Random.Range(0, 2);
        switch (a)
        {
            case 0:
                {
                    StopFight(true);
                    UIManager.instance.Print("Вы убежали.");
                    break;
                }
            case 1:
                {
                    UIManager.instance.Print("Убежать не удалось.");
					React ();
                    canRun = false;
                    break;
                }
        }
    }

    public void Interact()
    {
        Debug.Log("Interact");
        enemy.Interact();
    }
 #endregion

	public void Stun()
	{
		stun = 3;
	}

#region Different attacks
    public void PlayerAttack()
    {
		float a = Random.value;
		if (a > (player.accuracy - enemy.evasChance) / 200 && player.accuracy>enemy.evasChance) 
		{
			enHealth -= plDmg+ (enemy.airRes * player.airDmg) + (enemy.fireRes * player.fireDmg) + (enemy.darkRes * player.darkDmg) + (enemy.waterRes * player.waterDmg) + (enemy.lightRes * player.lightDmg) + (player.earthDmg * enemy.earthRes);
			UIManager.instance.Print ("Вы атакуете монстра");
			canRun = true;
			a = Random.value;
			if (a<player.CH/500)
			{
				UIManager.instance.Print ("Крит!");
				enHealth -= player.CD*(plDmg+ (enemy.airRes * player.airDmg) + (enemy.fireRes * player.fireDmg) + (enemy.darkRes * player.darkDmg) + (enemy.waterRes * player.waterDmg) + (enemy.lightRes * player.lightDmg) + (player.earthDmg * enemy.earthRes));
			}
			UIManager.instance.UpdateHealth (enHealth, plHealth);
		}
		else
			UIManager.instance.Print ("Вы промахнулись");
		if(bufftime>0)
		bufftime -= 1;
    }

    public void EnemyAttack()
    {
		float a = Random.value;
		UIManager.instance.Print ("Монстр атакует вас");
		if (a > (player.accuracy - enemy.evasChance) / 200) 
		{
			plHealth -= enemy.damage + (player.airRes * enemy.airDmg) + (player.fireRes * enemy.fireDmg) + (player.darkRes * enemy.darkDmg) + (player.waterRes * enemy.waterDmg) + (player.lightRes * enemy.lightDmg) + (enemy.earthDmg * player.earthRes);
			canRun = true;
			a = Random.value;
			if (a<enemy.CH/500)
			{
				UIManager.instance.Print ("Крит!");
				plHealth -= enemy.CD*(enemy.damage + (player.airRes * enemy.airDmg) + (player.fireRes * enemy.fireDmg) + (player.darkRes * enemy.darkDmg) + (player.waterRes * enemy.waterDmg) + (player.lightRes * enemy.lightDmg) + (enemy.earthDmg * player.earthRes));
			}
			UIManager.instance.UpdateHealth (enHealth, plHealth);
		}
		else 
		{
			UIManager.instance.Print ("Вы увернулись");
		}
    }
#endregion

	public void CheckIfSomebodyDied()
	{
		if (plHealth <= 0 || enHealth <= 0)
		{
			StopFight(false);
        }
	}

	public void React()
	{
        if (plHealth <= 0 || enHealth <= 0)
        {
            StopFight(false);
            return;
        }

		if (stun == 0) 
		{
			float a = Random.value;
			if (a < 0.4) 
			{
				EnemyAttack ();
		
			}
			if (0.4 <= a && a<0.9) 
			{
						a = Random.value;
						if (a<0.33)
						{
							enemy.React1();
						}
						if (0.33 <= a && a<0.67) 
						{
							enemy.React2();
						}
						if (0.67<=a)
						{
							enemy.React3();
						}
			}
			if (a >= 0.9)
			{
				if (enHealth < enemy.maxHealth * 0.05f) {
					StopFight (false);
					UIManager.instance.Print ("Противник сбежал");
					return;
				} 
				else 
				{
					EnemyAttack ();
				}
			}

		}
		else 
		{
			stun--;
			UIManager.instance.Print("Монстр не понимает, что происходит");
			///Делает ничего
			/// Абсолютно
		}

		CheckIfSomebodyDied ();
	}
}
