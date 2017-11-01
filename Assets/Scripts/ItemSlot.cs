﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {
    public Item item;
    public Button btn;
    public Image imageHolder;
    public bool equipmentSlot;
    bool isOvered;

    public void SetItem(Item _item)
    {
        if(equipmentSlot && item != null)
        {
            InventoryManager.instance.AddItem(item);
        }

        item = _item;
        imageHolder.sprite = item.image;

        if (btn != null)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => InventoryManager.instance.EquipItem(item));
        }
    }

    public void RemoveItem()
    {
        item = null;
        btn.onClick.RemoveAllListeners();
        imageHolder.sprite = null;
    }

    public void ShowDescr()
    {
        if (item == null) return;
        isOvered = true;
        //if (isOvered) return;
        Debug.Log("Mouse");
        string text;

        if (item.type == Types.TypeOfItem.WEAPON)
        {
            text = "Урон: " + item.damage;
        }
        else
        {
            text = "Защита: " + item.armor;
        }

        InventoryManager.instance.ShowDescr(text, transform);
    }
    public void UnshowDescr()
    {
        isOvered = false;
        InventoryManager.instance.UnShowDescr();
    }
}
