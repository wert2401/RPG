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
    public float plMana;
	//int bufftime;
	//float airbuff;
	//float earthbuff;
	//float firebuff;
	//float waterbuff;
	//float lightbuff;
	//float darkbuff;
    Spell buffSpell;
    [Header("Enemy")]
    public EnemyStatsHolder esh;
    public Enemy enemy;
    //public float enHealth;
    public float enMana;
    public bool canRun = true;
	int stun=0;
    [Header("Magic")]
    public InputField spellText;
    public Spell spell;
    public List<Buff> buffs;
    public List<int> BTs;
    public List<Buff> EnBuffs;
    public List<int> EnBTs;

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
        player.ResetStats();
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
        //plDmg = player.damage;

        esh.SetEnemy(enemy);
        //enHealth = enemy.maxHealth;
        //enMana = enemy.ManaMax;
        UIManager.instance.SetFightUI(enemy, player);
        UIManager.instance.UpdateHealth(Mathf.Round(esh.curHealth), Mathf.Round(plHealth));
    }

    public void BuffUse(Buff _buff,bool isEnemy)
    {
        if (isEnemy)
            enemy.EnManaLost(_buff);
        else
            _buff.ManaLost();
        if (_buff.TF == false)
            return;
        UIManager.instance.Print(_buff.words);
        player.airDmg += _buff.airBuff;
        player.earthDmg += _buff.earthBuff;
        player.damage += _buff.physBuff;
        player.fireDmg += _buff.fireBuff;
        player.waterDmg += _buff.waterBuff;
        player.lightDmg += _buff.lightBuff;
        player.darkDmg += _buff.darkBuff;
        buffs.Add(_buff);
        BTs.Add(_buff.buffTime);
    }

    public void EnBuffUse(Buff _buff,bool isEnemy)
    {
        if (isEnemy)
            enemy.EnManaLost(_buff);
        else
            _buff.ManaLost();
        if (_buff.TF == false)
            return;
        UIManager.instance.Print(_buff.words);
        esh.curMana -= _buff.ManaCost;
        esh.airDmg += _buff.airBuff;
        esh.damage += _buff.physBuff;
        esh.earthDmg += _buff.earthBuff;
        esh.fireDmg += _buff.fireBuff;
        esh.waterDmg += _buff.waterBuff;
        esh.lightDmg += _buff.lightBuff;
        esh.darkDmg += _buff.darkBuff;
        EnBuffs.Add(_buff);
        EnBTs.Add(_buff.buffTime);
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

        EnBuffs.Clear();
        EnBTs.Clear();
        buffs.Clear();
        BTs.Clear();
    }

    public void BuffUse()
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            if (BTs[i] > 0)
            {
                BTs[i] -= 1;
                if (BTs[i] == 0)
                {
                    player.airDmg -= buffs[i].airBuff;
                    player.earthDmg -= buffs[i].earthBuff;
                    player.fireDmg -= buffs[i].fireBuff;
                    player.waterDmg -= buffs[i].waterBuff;
                    player.lightDmg -= buffs[i].lightBuff;
                    player.darkDmg -= buffs[i].darkBuff;
                    player.damage -= buffs[i].physBuff;
                    buffs.Remove(buffs[i]);
                    BTs.Remove(BTs[i]);
                    i--;
                }
            }
        }
    }

    public void EnBuffUse()
    {
        for (int i = 0; i < EnBuffs.Count; i++)
        {
            if (EnBTs[i] > 0)
            {
                EnBTs[i] -= 1;
                if (EnBTs[i] == 0)
                {
                    esh.airDmg -= EnBuffs[i].airBuff;
                    esh.earthDmg -= EnBuffs[i].earthBuff;
                    esh.fireDmg -= EnBuffs[i].fireBuff;
                    esh.waterDmg -= EnBuffs[i].waterBuff;
                    esh.lightDmg -= EnBuffs[i].lightBuff;
                    esh.darkDmg -= EnBuffs[i].darkBuff;
                    esh.damage -= EnBuffs[i].physBuff;
                    EnBuffs.Remove(EnBuffs[i]);
                    EnBTs.Remove(EnBTs[i]);
                    i--;
                }
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
            UIManager.instance.UpdateHealth(Mathf.Round(esh.curHealth), Mathf.Round(plHealth));
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
        UIManager.instance.UpdateHealth(Mathf.Round(esh.curHealth), Mathf.Round(plHealth));
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
        EnBuffUse();
    }

    public void Stun()
    {
        stun = 3;
    }

    public void PlayerHit()
    {
        esh.curHealth -= (player.damage * Mathf.Pow(0.86f, enemy.armor)) + (enemy.airRes * player.airDmg) + (enemy.fireRes * player.fireDmg) + (enemy.darkRes * player.darkDmg) + (enemy.waterRes * player.waterDmg) + (enemy.lightRes * player.lightDmg) + (player.earthDmg * enemy.earthRes);
        UIManager.instance.Print("Вы атакуете монстра");
        canRun = true;
        float a = Random.value;
        if (a < player.CH / 500)
        {
            UIManager.instance.Print("Крит!");
            esh.curHealth -= player.CD * ((player.damage * Mathf.Pow(0.86f, enemy.armor)) + (enemy.airRes * player.airDmg) + (enemy.fireRes * player.fireDmg) + (enemy.darkRes * player.darkDmg) + (enemy.waterRes * player.waterDmg) + (enemy.lightRes * player.lightDmg) + (player.earthDmg * enemy.earthRes));
        }
        UIManager.instance.UpdateHealth(Mathf.Round(esh.curHealth), Mathf.Round(plHealth));
    }

    public void EnemyHit()
    {
        plHealth -= (esh.damage * Mathf.Pow(0.86f, player.armor)) + (player.airRes * enemy.airDmg) + (player.fireRes * enemy.fireDmg) + (player.darkRes * enemy.darkDmg) + (player.waterRes * enemy.waterDmg) + (player.lightRes * enemy.lightDmg) + (enemy.earthDmg * player.earthRes);
        canRun = true;
        float a = Random.value;
        if (a < esh.CH / 500)
        {
            UIManager.instance.Print("Крит!");
            plHealth -= esh.CD * ((enemy.damage * Mathf.Pow(0.86f, player.armor)) + (player.airRes * enemy.airDmg) + (player.fireRes * enemy.fireDmg) + (player.darkRes * enemy.darkDmg) + (player.waterRes * enemy.waterDmg) + (player.lightRes * enemy.lightDmg) + (enemy.earthDmg * player.earthRes));
        }
        UIManager.instance.UpdateHealth(Mathf.Round(esh.curHealth), Mathf.Round(plHealth));
    }
    #endregion

    public void CheckIfSomebodyDied()
	{
		if (plHealth <= 0 || esh.curHealth <= 0)
		{
			StopFight(false);
        }
	}

	public void React()
	{
        if (plHealth <= 0 || esh.curHealth <= 0)
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
				if (esh.curHealth < enemy.maxHealth * 0.05f) {
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
