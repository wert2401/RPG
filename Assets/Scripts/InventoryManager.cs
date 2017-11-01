using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    public static InventoryManager instance;
    public List<Item> items;
    public List<ItemSlot> slots;

    [Header("UI")]
    public ItemSlot headHolder;
    public ItemSlot chestHolder;
    public ItemSlot feetHolder;
    public ItemSlot weaponHolder;

    private void Awake()
    {
        instance = this;
    }

    private void UpdateInv()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                slots[i].SetItem(items[i]);
            }
            else
            {
                slots[i].imageHolder.sprite = null;
            }
        }
        if (items.Count == 0)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].RemoveItem();
            }
        }
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        UpdateInv();

        Debug.Log("Not enough space in inventory");
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        UpdateInv();
    }

    public void EquipItem(Item item)
    {
        if (item == null) return;

        switch (item.type)
        {
            case Types.TypeOfItem.HEAD:
                headHolder.SetItem(item);
                items.Remove(item);
                UpdateInv();
                break;
            case Types.TypeOfItem.CHEST:
                chestHolder.SetItem(item);
                items.Remove(item);
                UpdateInv();
                break;
            case Types.TypeOfItem.FEET:
                feetHolder.SetItem(item);
                items.Remove(item);
                UpdateInv();
                break;
            case Types.TypeOfItem.WEAPON:
                weaponHolder.SetItem(item);
                items.Remove(item);
                UpdateInv();
                break;
        }
    }

    public void ResetInv()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i] = null;
        }
        headHolder.RemoveItem();
        chestHolder.RemoveItem();
        feetHolder.RemoveItem();
        weaponHolder.RemoveItem();
    }

    public float GetItemsArmor()
    {
        float fullArmor = 0;
        if (headHolder.item != null)
        {
            fullArmor += headHolder.item.armor;
        }
        if (chestHolder.item != null)
        {
            fullArmor += chestHolder.item.armor;
        }
        if (feetHolder.item != null)
        {
            fullArmor += feetHolder.item.armor;
        }
        return fullArmor;
    }

    public float GetItemsDamage()
    {
        float fullDmg = 0;
        fullDmg = weaponHolder.item.armor;
        if (weaponHolder.item != null)
        {
            fullDmg += weaponHolder.item.damage;
        }
        return fullDmg;
    }
}
