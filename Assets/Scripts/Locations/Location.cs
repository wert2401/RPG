using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewLocation", menuName ="RPG/Location")]
public class Location : ScriptableObject {
    public string locationName;
    public string description;

    public bool isTown;
    public float CoF;
    public int CoV;
    public int CoFW;
    public int NCoV;
    public int Multiplier;
    public List<Location> locations;
    public List<Shop> shops;
    public List<Creature> enemies;
    public List<NPC> NPC;

    public Creature GetRandomEnemy()
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

    public NPC GetTargetNPC(int id)
    {
        return NPC[id];
    }

}
