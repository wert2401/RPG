using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Types;

[CreateAssetMenu(fileName = "NewItem", menuName = "RPG/Items/Item")]
public class Item : ScriptableObject {
    public TypeOfItem type;
    public Sprite image;
    public float armor;
    public float damage;
}
