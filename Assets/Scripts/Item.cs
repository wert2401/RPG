using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Types;

[CreateAssetMenu(fileName = "NewItem", menuName = "RPG/Items/Item")]
public class Item : ScriptableObject {
    public string itemName;
    public int cost;
    public TypeOfItem type;
    public Sprite image;
    public float armor;
    public float damage;

    public float fireDmg;
    public float waterDmg;
    public float airDmg;
    public float lightDmg;
    public float darkDmg;
    public float earthDmg;

    public float fireRes;
    public float waterRes;
    public float airRes;
    public float lightRes;
    public float darkRes;
    public float earthRes;
}
