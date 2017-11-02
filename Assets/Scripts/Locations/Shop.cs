using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShop", menuName = "RPG/Shop")]
public class Shop : ScriptableObject {
    public string shopName;
    public List<Item> items;

    public List<Item> GetItems()
    {
        List<Item> _items = new List<Item>();
        //List<Item> curItems = new List<Item>();
        //curItems = items;
        for (int i = 0; i < items.Count; i++)
        {
            int a = Random.Range(0, items.Count);
            //if (curItems.Count == 0) continue; 
            if (items[a] == null) continue;
            _items.Add(items[a]);
            //curItems.Remove(curItems[a]);
        }     
        return _items;
    }
}
