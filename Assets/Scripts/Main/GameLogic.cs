using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
    [HideInInspector]
    public static GameLogic instance;

    public Location startLocation;
    public Location curLoc;

    [Header("Player")]
    public Player player;
    public float plHealth;

    [Header("Enemy")]
    public Enemy enemy;
    public float enHealth;
    public bool canRun = true;

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

        UIManager.instance.SetFightUI(enemy, player);
        enHealth = enemy.maxHealth;
        plHealth = player.maxHealth;
    }

    public void StopFight()
    {
        UIManager.instance.SetLocationUI(startLocation);
        curLoc = startLocation;
        UIManager.instance.SetFightScreenOn(false);
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
            StopFight();
        }
    }

    public void Block()
    {
        Debug.Log("Block");
    }

    public void Spell()
    {
        Debug.Log("Spell");
    }

    public void UseHeal()
    {
        Debug.Log("Heal;");
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
                    StopFight();
                    UIManager.instance.Print("Вы убежали.");
                    break;
                }
            case 1:
                {
                    UIManager.instance.Print("Убежать не удалось, поэтому пришлось атаковать еще один раз.");
                    Attack();
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
        UIManager.instance.Print("Вы атакуете монстра.");
        UIManager.instance.UpdateHealth(enHealth, plHealth);
        canRun = true;
        if (plHealth <= 0 || enHealth <= 0)
        {
            StopFight();
        }
    }

    public void EnemyAttack()
    {
        plHealth -= enemy.damage;
        UIManager.instance.Print("Монстр атакует вас.");
        canRun = true;
        if (plHealth <= 0 || enHealth <= 0)
        {
            StopFight();
        }
    }
#endregion





}
