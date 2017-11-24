using System.Collections;
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
	//int bufftime;
	//float airbuff;
	//float earthbuff;
	//float firebuff;
	//float waterbuff;
	//float lightbuff;
	//float darkbuff;
    Spell buffSpell;
    [Header("Enemy")]
    public Enemy enemy;
    public float enHealth;
    float enDmg;
    public bool canRun = true;
	int stun=0;
    [Header("Magic")]
    public InputField spellText;
    public Spell spell;
    public Buff buff;
    public int BT;
    
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
        //plDmg = InventoryManager.instance.GetItemsDamage ();
        plDmg = player.damage;
        enHealth = enemy.maxHealth;
        //plHealth = player.maxHealth;
        UIManager.instance.SetFightUI(enemy, player);
        UIManager.instance.UpdateHealth(Mathf.Round(enHealth), Mathf.Round(plHealth));
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

        int moneyGain = enemy.GetRandomMoney();
        player.money += moneyGain;
    }
    public void BuffUse()
    {
        if (BT > 0)
        {
            BT -= 1;
            if (BT == 0)
            {
                player.airDmg -= buff.airBuff; 
                player.earthDmg -= buff.earthBuff;
                player.fireDmg -= buff.fireBuff;
                player.waterDmg -= buff.waterBuff;
                player.lightDmg -= buff.lightBuff;
                player.darkDmg -= buff.darkBuff;

            }
        }
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
    public void Spell()
    {
        spell = MagicManager.instance.CheckSpell(spellText.text);
        spellText.text = "";
        if (spell == null)
        {
            UIManager.instance.Print("Но ничего не произошло");
            UIManager.instance.SetSpellScreenOn();
            React();
            return;
        }
        else
        {
            spell.SpellUse();
            UIManager.instance.UpdateHealth(Mathf.Round(enHealth), Mathf.Round(plHealth));
        }
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
        UIManager.instance.UpdateHealth(Mathf.Round(enHealth), Mathf.Round(plHealth));
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
    public void PlayerHit()
    {
        enHealth -= (plDmg * Mathf.Pow(0.86f, enemy.armor)) + (enemy.airRes * player.airDmg) + (enemy.fireRes * player.fireDmg) + (enemy.darkRes * player.darkDmg) + (enemy.waterRes * player.waterDmg) + (enemy.lightRes * player.lightDmg) + (player.earthDmg * enemy.earthRes);
        UIManager.instance.Print("Вы атакуете монстра");
        canRun = true;
        float a = Random.value;
        if (a < player.CH / 500)
        {
            UIManager.instance.Print("Крит!");
            enHealth -= player.CD * ((plDmg * Mathf.Pow(0.86f, enemy.armor)) + (enemy.airRes * player.airDmg) + (enemy.fireRes * player.fireDmg) + (enemy.darkRes * player.darkDmg) + (enemy.waterRes * player.waterDmg) + (enemy.lightRes * player.lightDmg) + (player.earthDmg * enemy.earthRes));
        }
        UIManager.instance.UpdateHealth(Mathf.Round(enHealth), Mathf.Round(plHealth));
    }
    public void EnemyHit()
    {
        plHealth -= (enemy.damage * Mathf.Pow(0.86f, player.armor)) + (player.airRes * enemy.airDmg) + (player.fireRes * enemy.fireDmg) + (player.darkRes * enemy.darkDmg) + (player.waterRes * enemy.waterDmg) + (player.lightRes * enemy.lightDmg) + (enemy.earthDmg * player.earthRes);
        canRun = true;
        float a = Random.value;
        if (a < enemy.CH / 500)
        {
            UIManager.instance.Print("Крит!");
            plHealth -= enemy.CD * ((enemy.damage * Mathf.Pow(0.86f, player.armor)) + (player.airRes * enemy.airDmg) + (player.fireRes * enemy.fireDmg) + (player.darkRes * enemy.darkDmg) + (player.waterRes * enemy.waterDmg) + (player.lightRes * enemy.lightDmg) + (enemy.earthDmg * player.earthRes));
        }
        UIManager.instance.UpdateHealth(Mathf.Round(enHealth), Mathf.Round(plHealth));
    }

#region Different attacks
    public void PlayerAttack()
    {
	    float a = Random.value;
	    if (a > (enemy.evasChance - player.accuracy) / 100) 
	    {
            PlayerHit();
	    }
	    else
		    UIManager.instance.Print ("Вы промахнулись");
        BuffUse();
    }

    public void EnemyAttack()
    {
		float a = Random.value;
		UIManager.instance.Print ("Монстр атакует вас");
		if (a > (player.evasChance - enemy.accuracy) / 100) 
		{
            EnemyHit();
		}
		else 
			UIManager.instance.Print ("Вы увернулись");
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
