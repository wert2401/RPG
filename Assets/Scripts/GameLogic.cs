using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
    [HideInInspector]
    public static GameLogic instance;
    
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

    public void MoveToLocation(int id)
    {
        if (curLoc == null) return;

        SetLocationUI(curLoc.locations[id]);
    }


    private void SetLocationUI(Location loc)
    {
        curLoc = loc;
        UIManager.instance.SetLocationName(curLoc.locationName);

        for (int i = 0; i < curLoc.locations.Count; i++)
        {
            Location _loc = curLoc.locations[i];
            UIManager.instance.AddLocationButton(_loc.locationName, i);
        }
    }

    private void StartFight()
    {
        UIManager.instance.SetFightUI(enemy, player);
        enHealth = enemy.maxHealth;
    }



}
