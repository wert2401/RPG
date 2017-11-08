using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {
    public Item item;
    public Button btn;
    public Image imageHolder;
    public bool equipmentSlot;
    public bool isShopSlot;

    public void SetItem(Item _item)
    {
        if(equipmentSlot && item != null)
        {
            InventoryManager.instance.AddItem(item);
            InventoryManager.instance.RemoveItemsStats(item);
        }

        item = _item;
        imageHolder.sprite = item.image;

        //if (btn == null) return;

        if(!isShopSlot && !equipmentSlot)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => InventoryManager.instance.EquipItem(item));
        }

        if(isShopSlot && !equipmentSlot)
        {
            //Debug.Log("!!!!");
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => ShopManager.instance.BuyItem(item));
        }
    }

    public void RemoveItem()
    {
        item = null;
        if (btn != null)
        {
            btn.onClick.RemoveAllListeners();
        }
        imageHolder.sprite = null;
    }

    public void ShowDescr()
    {
        //if (isShopSlot) return;
        if (item == null)
        {
            UnshowDescr();
            return;
        }

        //Debug.Log("Mouse");
        string text = "";

        if (item.type == Types.TypeOfItem.WEAPON)
        {
            text += "Урон:" + item.damage + "\r\n";

            if (item.airDmg != 1)
            {
                text += "Урон от воздуха:" + item.airDmg + "\r\n";
            }
            if (item.darkDmg != 1)
            {
                text += "Урон от тьмы:" + item.darkDmg + "\r\n";
            }
            if (item.earthDmg != 1)
            {
                text += "Урон от земли:" + item.earthDmg + "\r\n";
            }
            if (item.fireDmg != 1)
            {
                text += "Урон от огня:" + item.fireDmg + "\r\n";
            }
            if (item.lightDmg != 1)
            {
                text += "Урон от света:" + item.lightDmg + "\r\n";
            }
            if (item.waterDmg != 1)
            {
                text += "Урон от воды:" + item.waterDmg + "\r\n";
            }
        }
        else
        {
            text += "Защита:" + item.armor + "\r\n";

            if (item.airRes != 1)
            {
                text += "Сопротивление воздуху:" + item.airRes + "\r\n";
            }
            if (item.darkDmg != 1)
            {
                text += "Сопротивление тьме:" + item.darkRes + "\r\n";
            }
            if (item.earthRes != 1)
            {
                text += "Сопротивление земле:" + item.earthRes + "\r\n";
            }
            if (item.fireDmg != 1)
            {
                text += "Сопротивление огню:" + item.fireRes + "\r\n";
            }
            if (item.lightRes != 1)
            {
                text += "Сопротивление свету:" + item.lightRes + "\r\n";
            }
            if (item.waterRes != 1)
            {
                text += "Сопротивление земле:" + item.waterRes + "\r\n";
            }
        }

        if (!isShopSlot)
        {
            InventoryManager.instance.ShowDescr(text, isShopSlot);
        }
        else
        {
            text += "Цена:" + item.cost;
            InventoryManager.instance.ShowDescr(text, isShopSlot);
        }
    }

    public void UnshowDescr()
    {
        InventoryManager.instance.UnShowDescr();
    }
}
