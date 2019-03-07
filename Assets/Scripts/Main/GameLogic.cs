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
    public List<CreatureNew> PlayerCharacters;
    public List<CreatureNew> EnemyCharacters;
    public List<CreatureNew> AllCharacters;
    public float enMana;
    public bool canRun = true;
	public int stun;
    public int PlStun;
    [Header("Magic")]
    public InputField spellText;
    public Spell spell;

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
                if (allEffects[id].id == PlayerEffects[i].id)
                {
                    Debug.Log("Player already have effect named " + PlayerEffects[i].EffectName);
                    if (!allEffects[id].stackable)
                    {
                        shouldEnd = true;
                        Debug.Log("This effect isn't stackable");
                    }
                    else
                        Debug.Log("This effect is stackable");
                }
            }
        else
            for (int i = 0; i < EnemyEffects.Count; i++)
            {
                if (allEffects[id].id == EnemyEffects[i].id)
                {
                    Debug.Log("Enemy already have effect named ");
                    if (!allEffects[id].stackable)
                    {
                        shouldEnd = true;
                        Debug.Log("This effect isn't stackable");
                    }
                    else
                        Debug.Log("This effect is stackable");
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
    public void DealPhysDamage(int damage, CreatureNew Target)
    {
        Target.Health -= Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.armor));
        if(Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.armor)) != 0)
            UIManager.instance.Print(Target.Name + " получил " + Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.armor)) + " физического урона");
    }

    public void DealAirDamage(int damage, CreatureNew Target)
    {
        Target.Health -= Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.airRes));
        if (Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.airRes)) != 0)
            UIManager.instance.Print(Target.Name + " получил " + Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.airRes)) + " воздушного урона");
    }

    public void DealWaterDamage(int damage, CreatureNew Target)
    {
        Target.Health -= Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.waterRes));
        if (Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.waterRes)) != 0)
            UIManager.instance.Print(Target.Name + " получил " + Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.waterRes)) + " водного урона");
    }

    public void DealDarkDamage(int damage, CreatureNew Target)
    {
        Target.Health -= Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.darkRes));
        if (Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.darkRes)) != 0)
            UIManager.instance.Print(Target.Name + " получил " + Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.darkRes)) + " тёмного урона");
    }

    public void DealLightDamage(int damage, CreatureNew Target)
    {
        Target.Health -= Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.lightRes));
        if (Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.lightRes)) != 0)
            UIManager.instance.Print(Target.Name + " получил " + Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.lightRes)) + " светлого урона");
    }

    public void DealEarthDamage(int damage, CreatureNew Target)
    {
        Target.Health -= Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.earthRes));
        if (Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.earthRes)) != 0)
            UIManager.instance.Print(Target.Name + " получил " + Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.earthRes)) + " земляного урона");
    }

    public void DealFireDamage(int damage, CreatureNew Target)
    {
        Target.Health -= Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.fireRes));
        if (Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.fireRes)) != 0)
            UIManager.instance.Print(Target.Name + " получил " + Mathf.RoundToInt(damage * Mathf.Pow(0.95f, Target.fireRes)) + " огненного урона");
    }

    public void DealPureDamage(int damage, CreatureNew Target)
    {
        Target.Health -= damage;
        if (damage != 0)
            UIManager.instance.Print(Target.Name + " получил " + damage + " чистого урона");
    }

    #endregion

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
        if (player.lvl - enemy.lvl >= 10)
            expG = 0;
        if (ran)
			expG = 0;
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
        UIManager.instance.SetLocationUI(curLoc);
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
        Debug.Log("Enemy attack");
		float a = Random.value;
		UIManager.instance.Print ("Противник атакует вас");
		if (a > (player.evasChance - enemy.accuracy) / 100) 
            EnemyHit();
		else 
			UIManager.instance.Print ("Вы увернулись");
    }

    public void EnemyHit()
    {
        UIManager.instance.UpdateHealth(Mathf.Round(enemy.health), Mathf.Round(plHealth));
    }
    #endregion


	public void React()
	{
        for (int i = 0; i < EnemyEffects.Count; i++)
        {
            EnemyEffects[i].Tick();
            if (EnemyEffects[i].time == 0)
            {
                EnemyEffects.Remove(EnemyEffects[i]);
                i--;
            }
        }
        if (plHealth <= 0 || enemy.health <= 0)
        {
            StopFight(false);
            return;
        }

		if (stun == 0) 
		{
            enemy.React();
		}
		else 
		{
			stun--;
			UIManager.instance.Print("Противник не понимает, что происходит");
			///Делает ничего
			/// Абсолютно
		}
        if (PlStun>0)
        {
            PlStun--;
            React();
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
