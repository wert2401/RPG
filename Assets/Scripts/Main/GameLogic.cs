using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameLogic : MonoBehaviour {
    [HideInInspector]
    public static GameLogic instance;
    public Location startLocation;
    public Location curLoc;
    public List<Location> Locations;
    public Effect OperatingEffect;
    public AudioClip MainTheme;
    public AudioClip BattleStart;

    public List<Effect> allEffects;
    [Header("Player")]
    public Player player;
    public List<Effect> PlayerEffects;
    public float plHealth;
    public float plMana;
    Spell buffSpell;
    [Header("Enemy")]
    public List<Effect> EnemyEffects;
    public Creature enemy;
    public Creature ifNPC;
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
        LocationCleaner.instance.LocClear();
        if (startLocation != null)
        {
            UIManager.instance.SetLocationUI(startLocation);
            curLoc = startLocation;
        }
        else
            Debug.Log("Set start location!");
        player.ResetStats();
        RessurectNPC();
    }

    public void RessurectNPC()
    {
        for (int i = 0; i < Locations.Count; i++)
        {
            for (int l = 0; l < Locations[i].NPC.Count; l++)
            {
                Locations[i].NPC[l].isAlive = true;
            }
        }
    }

    public void ClearAllBuffsOnPlayer()
    {
        for (int i = 0; i < BTs.Count; i++)
        {
            BTs[i] = 0;
        }
        BuffTick();
    }
    #region Effects
    public void AddEffect(bool onPlayer,int id,int time)
    {
        OperatingEffect = Instantiate(allEffects[id]);
        OperatingEffect.time = time;
        OperatingEffect.onPlayer = onPlayer;
        bool shouldEnd = false;
        if(onPlayer)
            for (int i = 0; i < PlayerEffects.Count; i++)
            {
                if (allEffects[id].EffectName == PlayerEffects[i].EffectName)
                {
                    if(!allEffects[id].stackable)
                        shouldEnd = true;
                }
            }
        else
            for (int i = 0; i < EnemyEffects.Count; i++)
            {
                if (allEffects[id].EffectName == EnemyEffects[i].EffectName)
                {
                    if (!allEffects[id].stackable)
                        shouldEnd = true;
                }
            }
        if (shouldEnd)
            return;
        if (onPlayer)
        {
            PlayerEffects.Add(Instantiate(OperatingEffect));
            OperatingEffect.Get();
            Debug.Log("Player get effect №" + id);
        }
        else
        {
            EnemyEffects.Add(OperatingEffect);
            OperatingEffect.Get();
            Debug.Log("Enemy get effect №" + id);
        }
    }
#region Functions
    public void UseAllFunctionOnStartAttack(bool PlayerIsTarget)
    {
        if (!PlayerIsTarget)
        {
            for (int i = 0; i < PlayerEffects.Count; i++)
            {
                PlayerEffects[i].FunctionOnStartAttack();
            }
            for (int i = 0; i < EnemyEffects.Count; i++)
            {
                EnemyEffects[i].FunctionOnStartBeingAttacked();
            }
        }
        else
        {
            for (int i = 0; i < PlayerEffects.Count; i++)
            {
                PlayerEffects[i].FunctionOnStartBeingAttacked();
            }
            for (int i = 0; i < EnemyEffects.Count; i++)
            {
                EnemyEffects[i].FunctionOnStartAttack();
            }
        }
    }
    public void UseAllFunctionOnStartUsingSpell()
    {

    }
    public void UseAllFunctionOnStartBeingSpelled()
    {

    }
    public void UseAllFunctionOnStartDealingDamage()
    {

    }
    public void UseAllFunctionOnStartBeingDamaged()
    {

    }

    public void UseAllFunctionOnEndAttack()
    {

    }
    public void UseAllFunctionOnEndBeingAttacked()
    {

    }
    public void UseAllFunctionOnEndUsingSpell()
    {

    }
    public void UseAllFunctionOnEndBeingSpelled()
    {

    }
    public void UseAllFunctionOnEndDealingDamage()
    {

    }
    public void UseAllFunctionOnEndBeingDamaged()
    {

    }

    public void UseAllFunctionOnBattleStarting()
    {

    }
    public void UseAllFunctionOnBattleEnding()
    {

    }
#endregion


    #endregion

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

        UIManager.instance.SetLocationUI(curLoc.locations[id]);
        curLoc = curLoc.locations[id];
        curLoc.CoV++;
        if (curLoc.CoV>=curLoc.NCoV)
        {
            curLoc.CoFW++;
            if (curLoc.CoFW>curLoc.locations.Count)
            {
                curLoc.CoFW = curLoc.locations.Count;
            }
            curLoc.CoV = 0;
            curLoc.NCoV *= curLoc.Multiplier;
        }
        float a = Random.value;
        if (a<curLoc.CoF)
        {
            StartFight();
        }
    }

    public void MoveToShop(int id)
    {
        if (curLoc == null) return;

        ShopManager.instance.SetShopUI(curLoc.shops[id]);
    }

    public void StartActingWithNPC(int id)
    {
        UIManager.instance.SetLocationScreenOn(false);
        enemy = curLoc.GetTargetNPC(id);
        ifNPC = enemy;
        UIManager.instance.SetFightUI(enemy, player);
        UIManager.instance.UpdateHealth(Mathf.Round(enemy.health), Mathf.Round(plHealth));
    }
 #endregion
#region Fight
    private void StartFight()
    {
        UIManager.instance.SetLocationScreenOn(false);
        enemy = Instantiate(curLoc.GetRandomEnemy());
        enemy.health = enemy.maxHealth;
        enemy.mana = enemy.ManaMax;
        if (enemy == null) return;
        UIManager.instance.SetFightUI(enemy, player);
        UIManager.instance.UpdateHealth(Mathf.Round(enemy.health), Mathf.Round(plHealth));
        GetComponent<AudioSource>().clip = BattleStart;
        GetComponent<AudioSource>().Play();
    }
#region Dealing damage
    public void DealPhysDamage(float damage)
    {
        enemy.health -= (damage * Mathf.Pow(0.86f, enemy.armor));
        if(damage * Mathf.Pow(0.86f, enemy.armor) != 0)
            UIManager.instance.Print("Противник получил "+ damage * Mathf.Pow(0.86f, enemy.armor) + " физического урона");
    }

    public void DealAirDamage(float damage)
    {
        enemy.health -= (damage * enemy.airRes);
        if (damage * enemy.airRes != 0)
            UIManager.instance.Print("Противник получил " + damage * enemy.airRes + " воздушного урона");
    }

    public void DealWaterDamage(float damage)
    {
        enemy.health -= (damage * enemy.waterRes);
        if (damage * enemy.waterRes != 0)
            UIManager.instance.Print("Противник получил " + damage * enemy.waterRes + " водного урона");
    }

    public void DealDarkDamage(float damage)
    {
        enemy.health -= (damage * enemy.darkRes);
        if (damage * enemy.darkRes != 0)
            UIManager.instance.Print("Противник получил " + damage * enemy.darkRes + " тёмного урона");
    }

    public void DealLightDamage(float damage)
    {
        enemy.health -= (damage * enemy.lightRes);
        if (damage * enemy.lightRes != 0)
            UIManager.instance.Print("Противник получил " + damage * enemy.lightRes + " светлого урона");
    }

    public void DealEarthDamage(float damage)
    {
        enemy.health -= (damage * enemy.earthRes);
        if (damage * enemy.earthRes != 0)
            UIManager.instance.Print("Противник получил " + damage * enemy.earthRes + " земляного урона");
    }

    public void DealFireDamage(float damage)
    {
        enemy.health -= (damage * enemy.fireRes);
        if (damage * enemy.fireRes != 0)
            UIManager.instance.Print("Противник получил " + damage * enemy.fireRes + " огненного урона");
    }

    public void DealPureDamage(float damage)
    {
        enemy.health -= damage;
        if (damage != 0)
            UIManager.instance.Print("Противник получил " + damage + " чистого урона");
    }

    #endregion
#region Getting damage
    public void GetPhysDamage(float damage)
    {
        plHealth -= (damage * Mathf.Pow(0.86f, player.armor));
        if (damage * Mathf.Pow(0.86f, player.armor) != 0)
            UIManager.instance.Print("Вы получили " + damage * Mathf.Pow(0.86f, player.armor) + " физического урона");
    }

    public void GetAirDamage(float damage)
    {
        plHealth -= (damage * player.airRes);
        if (damage * player.airRes != 0)
            UIManager.instance.Print("Вы получили " + damage * player.airRes + " воздушного урона");
    }

    public void GetWaterDamage(float damage)
    {
        plHealth -= (damage * player.waterRes);
        if (damage * player.waterRes != 0)
            UIManager.instance.Print("Вы получили " + damage * player.waterRes + " водного урона");
    }

    public void GetDarkDamage(float damage)
    {
        plHealth -= (damage * player.darkRes);
        if (damage * player.darkRes != 0)
            UIManager.instance.Print("Вы получили " + damage * player.darkRes + " тёмного урона");
    }

    public void GetLightDamage(float damage)
    {
        plHealth -= (damage * player.lightRes);
        if (damage * player.lightRes != 0)
            UIManager.instance.Print("Вы получили " + damage * player.lightRes + " светлого урона");
    }

    public void GetEarthDamage(float damage)
    {
        plHealth -= (damage * player.earthRes);
        if (damage * player.earthRes != 0)
            UIManager.instance.Print("Вы получили " + damage * player.earthRes + " земляного урона");
    }

    public void GetFireDamage(float damage)
    {
        plHealth -= (damage * player.fireRes);
        if (damage * player.fireRes != 0)
            UIManager.instance.Print("Вы получили " + damage * player.fireRes + " огненного урона");
    }

    public void GetPureDamage(float damage)
    {
        plHealth -= damage;
        if (damage != 0)
            UIManager.instance.Print("Вы получили " + damage + " чистого урона");
    }
    #endregion
    public void BuffUse(Buff _buff,bool isEnemy)
    {
        if (isEnemy)
            enemy.EnManaLost(_buff);
        else
            _buff.ManaLost();
        if (_buff.TF == false)
            return;
        UIManager.instance.Print(_buff.SpellWords);
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
        UIManager.instance.Print(_buff.SpellWords);
        enemy.mana -= _buff.ManaCost;
        enemy.airDmg += _buff.airBuff;
        enemy.damage += _buff.physBuff;
        enemy.earthDmg += _buff.earthBuff;
        enemy.fireDmg += _buff.fireBuff;
        enemy.waterDmg += _buff.waterBuff;
        enemy.lightDmg += _buff.lightBuff;
        enemy.darkDmg += _buff.darkBuff;
        EnBuffs.Add(_buff);
        EnBTs.Add(_buff.buffTime);
    }

    public void StopFight(bool ran)
    {
        GetComponent<AudioSource>().clip = MainTheme;
        GetComponent<AudioSource>().Play();
        foreach (Effect effect in PlayerEffects)
        {
            effect.End();
        }
        PlayerEffects.Clear();
        foreach (Effect effect in EnemyEffects)
        {
            effect.End();
        }
        EnemyEffects.Clear();
        UIManager.instance.SetFightScreenOn(false);
        UIManager.instance.SetLocationUI(curLoc);
        if (plHealth <= 0)
        {
            player.ResetStats();
			UIManager.instance.SetStartScreenOn();
			UIManager.instance.SetDiedScreenOn();
			UIManager.instance.ClearLog ();
            InventoryManager.instance.ResetInv();
            LocationCleaner.instance.LocClear();
            UIManager.instance.SetLocationUI(startLocation);
            return;
        }

        int expG = enemy.expGain * (enemy.lvl / player.lvl);
        if (ran)
        {  
			if (enemy.lvl - player.lvl >= 20)
				expG = 0;
            expG = expG / 10;
        }
        player.AddExp(expG);

        if (!ran)
        {
            int moneyGain = enemy.GetMoney();
            player.money += moneyGain;
            if (ifNPC != null)
            {
                enemy.isAlive = false;
                ifNPC = null;
            }
            enemy.GetDrop();
            Debug.Log("Айтем выдан");
        }
        EnBuffs.Clear();
        EnBTs.Clear();
        ClearAllBuffsOnPlayer();
        UIManager.instance.SetLocationUI(curLoc);
    }

    public void BuffTick()
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            if (BTs[i] >= 0)
            {
                BTs[i] -= 1;
                if (BTs[i] <= 0)
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

    public void EnemyBuffTick()
    {
        for (int i = 0; i < EnBuffs.Count; i++)
        {
            if (EnBTs[i] > 0)
            {
                EnBTs[i] -= 1;
                if (EnBTs[i] == 0)
                {
                    enemy.airDmg -= EnBuffs[i].airBuff;
                    enemy.earthDmg -= EnBuffs[i].earthBuff;
                    enemy.fireDmg -= EnBuffs[i].fireBuff;
                    enemy.waterDmg -= EnBuffs[i].waterBuff;
                    enemy.lightDmg -= EnBuffs[i].lightBuff;
                    enemy.darkDmg -= EnBuffs[i].darkBuff;
                    enemy.damage -= EnBuffs[i].physBuff;
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
            UIManager.instance.UpdateHealth(Mathf.Round(enemy.health), Mathf.Round(plHealth));
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
        UIManager.instance.UpdateHealth(Mathf.Round(enemy.health), Mathf.Round(plHealth));
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
        UIManager.instance.UpdateHealth(Mathf.Round(enemy.health), Mathf.Round(plHealth));
    }
 #endregion

#region Different attacks
    public void PlayerAttack()
    {
        UseAllFunctionOnStartAttack(false);
	    float a = Random.value;
	    if (a > (enemy.evasChance - player.accuracy) / 100) 
	    {
            PlayerHit();
	    }
	    else
		    UIManager.instance.Print ("Вы промахнулись");
        BuffTick();
        for (int i = 0; i < PlayerEffects.Count; i++)
        {
            PlayerEffects[i].Tick();
            if (PlayerEffects[i].time == 0)
            {
                PlayerEffects.Remove(PlayerEffects[i]);
                i--;
            }
        }
        UIManager.instance.UpdateHealth(Mathf.Round(enemy.health), Mathf.Round(plHealth));
    }

    public void EnemyAttack()
    {
        UseAllFunctionOnStartAttack(true);
        Debug.Log("Enemy attack");
		float a = Random.value;
		UIManager.instance.Print ("Противник атакует вас");
		if (a > (player.evasChance - enemy.accuracy) / 100) 
		{
            EnemyHit();
		}
		else 
			UIManager.instance.Print ("Вы увернулись");
        EnemyBuffTick();
    }

    public void Stun()
    {
        stun = 3;
    }

    public void PlayerHit()
    {
        UIManager.instance.Print("Вы атакуете монстра");
        DealAirDamage(player.airDmg);
        DealDarkDamage(player.darkDmg);
        DealEarthDamage(player.earthDmg);
        DealFireDamage(player.fireDmg);
        DealLightDamage(player.lightDmg);
        DealPhysDamage(player.damage);
        DealWaterDamage(player.waterDmg);
        canRun = true;
        float a = Random.value;
        if (a < player.CH / 500)
        {
            UIManager.instance.Print("Крит!");
            DealAirDamage(player.airDmg * player.CD);
            DealDarkDamage(player.darkDmg * player.CD);
            DealEarthDamage(player.earthDmg * player.CD);
            DealFireDamage(player.fireDmg * player.CD);
            DealLightDamage(player.lightDmg * player.CD);
            DealPhysDamage(player.damage * player.CD);
            DealWaterDamage(player.waterDmg * player.CD);
        }
    }

    public void EnemyHit()
    {
        GetAirDamage(enemy.airDmg);
        GetDarkDamage(enemy.darkDmg);
        GetEarthDamage(enemy.earthDmg);
        GetFireDamage(enemy.fireDmg);
        GetLightDamage(enemy.lightDmg);
        GetPhysDamage(enemy.damage);
        GetWaterDamage(enemy.waterDmg);
        canRun = true;
        float a = Random.value;
        if (a < enemy.CH / 500)
        {
            UIManager.instance.Print("Крит!");
            GetAirDamage(enemy.airDmg * enemy.CD);
            GetDarkDamage(enemy.darkDmg * enemy.CD);
            GetEarthDamage(enemy.earthDmg * enemy.CD);
            GetFireDamage(enemy.fireDmg * enemy.CD);
            GetLightDamage(enemy.lightDmg * enemy.CD);
            GetPhysDamage(enemy.damage * enemy.CD);
            GetWaterDamage(enemy.waterDmg * enemy.CD);
        }
        UIManager.instance.UpdateHealth(Mathf.Round(enemy.health), Mathf.Round(plHealth));
    }
    #endregion


	public void React()
	{
        if (plHealth <= 0 || enemy.health <= 0)
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
						if (0.67<=a && a<0.9)
						{
							enemy.React3();
						}
			}
			if (a >= 0.9)
			{
				if (enemy.health < enemy.maxHealth * 0.05f) {
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
			UIManager.instance.Print("Противник не понимает, что происходит");
			///Делает ничего
			/// Абсолютно
		}
        UIManager.instance.UpdateHealth(Mathf.Round(enemy.health), Mathf.Round(plHealth));
        if (plHealth <= 0 || enemy.health <= 0)
        {
            StopFight(false);
            return;
        }
        UIManager.instance.UpdateHealth(Mathf.Round(enemy.health), Mathf.Round(plHealth));
    }
}
