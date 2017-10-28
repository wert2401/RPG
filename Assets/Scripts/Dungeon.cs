using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDungeon", menuName = "RPG/Enemies/Dungeon")]
public class Dungeon : ScriptableObject {
    public string dungeonName;
    public List<Enemy> enemies;

    public Enemy GetRandomEnemy()
    {
        int a = Random.Range(0, enemies.Count);
        return enemies[a];
    }
}
