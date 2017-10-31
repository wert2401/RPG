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

    [Header("Enemy")]
    public Enemy enemy;
    public float enHealth;
    float enDmg;
    public bool canRun = true;

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
 #endregion

#region Fight
    private void StartFight()
    {
        if (enemy == null) return;

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
            return;
        }

        int expG = enemy.expGain * (enemy.enLvl / player.lvl);
        if (ran)
        {  
            expG = expG / 10;
        }
        player.AddExp(expG);
    }

    public void Attack()
    {
        UIManager.instance.Print("Вы атаковали монстра, а он атаковал в ответ.");

        plHealth -= enemy.damage;
        enHealth -= player.damage;
        UIManager.instance.UpdateHealth(enHealth, plHealth);
        canRun = true;

        if (plHealth <= 0 || enHealth <= 0)
        {
            StopFight(false);
        }
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
            EnemyAttack();
        }
    }

    public void Spell()
    {
        Debug.Log("Spell");
        spell = MagicManager.instance.CheckSpell(spellText.text);

        if (enemy == null)
            return;

        enHealth -= (enemy.airRes * spell.airDmg) + (enemy.fireRes * spell.fireDmg) + (enemy.darkRes * spell.darkDmg) + (enemy.waterRes * spell.waterDmg) + (enemy.lightRes * spell.lightDmg);
        EnemyAttack();
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
        if (plHealth > player.maxHealth)
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
                    EnemyAttack();
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
        enHealth -= player.damage;
        UIManager.instance.Print("Вы атакуете монстра");
        UIManager.instance.UpdateHealth(enHealth, plHealth);
        canRun = true;
        if (plHealth <= 0 || enHealth <= 0)
        {
            StopFight(false);
        }
    }

    public void EnemyAttack()
    {
        plHealth -= enemy.damage;
        UIManager.instance.Print("Монстр атакует вас");
        UIManager.instance.UpdateHealth(enHealth, plHealth);
        canRun = true;
        if (plHealth <= 0 || enHealth <= 0)
        {
            StopFight(false);
        }
    }
#endregion





}
