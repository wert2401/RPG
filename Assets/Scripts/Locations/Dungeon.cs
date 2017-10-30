using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDungeon", menuName = "RPG/Enemies/Dungeon")]
public class Dungeon : ScriptableObject {
    public string dungeonName;
    public List<Enemy> enemies;

    public Enemy GetRandomEnemy()
    {
        int a = Random.Range(0, 100);
        if ((a >= 0) && (a <= 39)) 
        {
            return enemies[0];
        }
        if ((a >= 40) && (a <= 79))
        {
            return enemies[1];
        }
        if ((a >= 80) && (a <= 94))
        {
            return enemies[2];
        }
        if ((a >= 95) && (a <= 99))
        {
            return enemies[3];
        }
        return enemies[0];
    }
}
