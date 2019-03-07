using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class LocationNew : ScriptableObject
{
    public string LocationName;
    public string Description;

    public int Multiplier;
    public List<LocationNew> BindedLocations;
    public List<Shop> LocalShops;
    public List<PackOfEnemies> Packs;

    public PackOfEnemies GetRandomPack()
    {
        int a = Random.Range(0, Packs.Count);
        return Packs[a];
    }
}