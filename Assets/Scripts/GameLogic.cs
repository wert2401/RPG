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

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (curLoc != null)
            SetLocationUI(curLoc);
        else
            Debug.Log("Set start location!");
    }

    private void SetLocationUI(Location loc)
    {
        curLoc = loc;
        UIManager.instance.SetLocationScreenOn(true);
        UIManager.instance.SetLocationName(curLoc.locationName);

        if (curLoc.locations.Count > 0)
        {
            for (int i = 0; i < curLoc.locations.Count; i++)
            {
                Location _loc = curLoc.locations[i];
                UIManager.instance.AddLocationButton(_loc.locationName, i);
            }
        }

        if (curLoc.dungeons.Count > 0)
        {
            for (int i = 0; i < curLoc.locations.Count; i++)
            {
                Dungeon _dun = curLoc.dungeons[i];
                UIManager.instance.AddDungeonButton(_dun.dungeonName, i);
            }
        }
    }

    private void StartFight()
    {
        if (enemy == null) return;

        UIManager.instance.SetFightUI(enemy, player);
        enHealth = enemy.maxHealth;
        plHealth = player.maxHealth;
    }

    private void StopFight()
    {
        SetLocationUI(startLocation);
        UIManager.instance.SetFightScreenOn(false);
    }

    public void MoveToLocation(int id)
    {
        if (curLoc == null) return;

        SetLocationUI(curLoc.locations[id]);
    }

    public void EnterTheDungeon(int id)
    {
        if (curLoc == null) return;

        enemy = curLoc.dungeons[id].GetRandomEnemy();
        UIManager.instance.SetLocationScreenOn(false);
        StartFight();
    }

    public void Attack()
    {
        Debug.Log("Attack");

        plHealth -= enemy.damage;
        enHealth -= player.damage;
        UIManager.instance.UpdateHealth(enHealth, plHealth);

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
        Debug.Log("Try to run");
    }

    public void Interact()
    {
        Debug.Log("Interact");
    }





}
