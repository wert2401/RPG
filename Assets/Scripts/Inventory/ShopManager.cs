using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
    [HideInInspector]
    public static ShopManager instance;

    public GameObject shopScreen;
    public Player player;
    public List<ItemSlot> slots;
    public Text playerMoney;
    public Text shopName;

    List<Item> items;

    private void Awake()
    {
        instance = this;
    }

#region UI
    public void SetShopUI(Shop shop)
    {
        //Debug.Log("ShopUi");
        shopName.text = shop.shopName;
        SetItems(shop);
        UpdateUI();
        SetShopScreenOn();
    }

    public void UpdateUI()
    {
        playerMoney.text = player.money.ToString();

        for (int i = 0; i < slots.Count; i++)
        {
            if (i < items.Count)
            {
                slots[i].SetItem(items[i]);
            }
            else
            {
                slots[i].RemoveItem();
            }
        }
    }
#endregion

    public void BuyItem(Item item)
    {
        if (item.cost > player.money)
        {
            UIManager.instance.Print("У вас не достаточно денег");
            return;
        }
        Debug.Log("You bought" + item.itemName);
        player.money -= item.cost;
        InventoryManager.instance.AddItem(item);
        items.Remove(item);
        UpdateUI();
    }

    public void SetItems(Shop shop)
    {
        items = shop.GetItems();
    }

    public void SetShopScreenOn()
    {
        shopScreen.SetActive(!shopScreen.activeSelf);
    }
}
