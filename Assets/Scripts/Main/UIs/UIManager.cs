﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [HideInInspector]
    public static UIManager instance;

    public GameObject button;

    [Header("Location Screen")]
    public GameObject locationScreen;
    public GameObject locButtonsHolder;
    public Text locationName;

    [Header("Fight Screen")]
    public GameObject fightScreen;
    public GameObject fightButtonsHolder;
    public GameObject dynButtonsHolder;
    public GameObject extrDynButtonsHolder;
	public GameObject SpellScreen;
	public GameObject StartScreen;
	public GameObject Died;
    [Header("Enemy")]
    public Text enName;
    public Text enHealth;
    [Header("Player")]
    public Text plName;
    public Text plHealth;

    [Header("Log")]
    public Text log;

    [Header("Player stats and staff")]
    public GameObject charScreen;
    public GameObject inventoryScreen;

    public delegate void Met();
    public delegate void Dia(int a);

    private void Awake()
    {
        instance = this;
    }

#region Locations
    public void SetLocationUI(Location loc)
    {
        SetLocationScreenOn(true);
        SetLocationName(loc.locationName);
        RemoveAllButtons(locButtonsHolder.transform);

        if (loc.CoFW > 0 && loc.locations.Count > 0)
        {
            for (int i = 0; i < loc.CoFW; i++)
            {
                Location _loc = loc.locations[i];
                AddLocationButton(_loc.locationName, i);
            }
        }

        if (loc.shops.Count > 0)
        {
            for (int i = 0; i < loc.shops.Count; i++)
            {
                Shop _shop = loc.shops[i];
                AddShopButton(_shop.shopName, i);
            }
        }

<<<<<<< HEAD
        if (loc.CoFW==loc.locations.Count)
        {
            for (int i = 0; i < loc.NPC.Count; i++)
            {
                AddNPCButton(loc.NPC[i].CrName, i);
=======
        if (loc.NPC.Count > 0 && loc.isTown)
        {
            for (int i = 0; i < loc.NPC.Count; i++)
            {
                AddDiaButton(loc.NPC[i].CrName, GameLogic.instance.StartFight, locButtonsHolder.transform, i);
>>>>>>> 592918dcf2b0bc3d6173d625570f485416134db1
            }
        }
    }

    public void SetLocationName(string name)
    {
        locationName.text = name;
    }

    public void AddLocationButton(string name, int id)
    {
        GameObject go = Instantiate(button, locButtonsHolder.transform);
        ButtonHelper btnHelp = go.GetComponent<ButtonHelper>();
        btnHelp.SetText(name);
        btnHelp.btn.onClick.AddListener(() => GameLogic.instance.MoveToLocation(id));
    }

    public void AddNPCButton(string name, int id)
    {
        GameObject go = Instantiate(button, locButtonsHolder.transform);
        ButtonHelper btnHelp = go.GetComponent<ButtonHelper>();
        btnHelp.SetText(name);
        btnHelp.btn.onClick.AddListener(() => GameLogic.instance.StartActing(id));
    }

    public void AddShopButton(string name, int id)
    {
        GameObject go = Instantiate(button, locButtonsHolder.transform);
        ButtonHelper btnHelp = go.GetComponent<ButtonHelper>();
        btnHelp.SetText(name);
        btnHelp.btn.onClick.AddListener(() => GameLogic.instance.MoveToShop(id));
    }


    public void SetLocationScreenOn(bool stage)
    {
        locationScreen.SetActive(stage);
    }
#endregion

#region Fight
    public void SetFightScreenOn(bool stage)
    {
        fightScreen.SetActive(stage);
    }

    public void SetFightUI(Creature en, Player pl)
    {
        RemoveAllButtons(dynButtonsHolder.transform);
        RemoveAllButtons(extrDynButtonsHolder.transform);
        ClearLog();

        locationScreen.SetActive(false);
        fightScreen.SetActive(true);

        en.SetEnemyUI();

        UpdateHealth(en.maxHealth, pl.maxHealth);
        UpdateNames(en.CrName, pl.plName);
    }

    public void UpdateHealth(float enemyHp, float playerHp)
    {
        enHealth.text = enemyHp.ToString();
        plHealth.text = playerHp.ToString();
    }

    private void UpdateNames(string _enName, string _plName)
    {
        plName.text = _plName;
        enName.text = _enName;
    }

    public void SetDynButtonsOn(bool stage)
    {
        dynButtonsHolder.SetActive(stage);
    }

    public void SetExtrDynButtonsOn(bool stage)
    {
        extrDynButtonsHolder.SetActive(stage);
    }

    public void SetFightButtonsOn(bool stage)
    {
        fightButtonsHolder.SetActive(stage);
    }

    public void AddDynButton(string name, Met met, Transform holder)
    {
        GameObject go = Instantiate(button, holder.transform);
        ButtonHelper btnHelp = go.GetComponent<ButtonHelper>();
        btnHelp.SetText(name);
        btnHelp.btn.onClick.AddListener(met.Invoke);
    }

    public void AddDiaButton(string name, Dia dia, Transform holder,int id)
    {
        GameObject go = Instantiate(button, holder.transform);
        ButtonHelper btnHelp = go.GetComponent<ButtonHelper>();
        btnHelp.SetText(name);
        btnHelp.btn.onClick.AddListener(() => dia(id));
    }

    //public void AddExtrDynButton(string name, met met)
    //{
    //    GameObject go = Instantiate(button, extrDynButtonsHolder.transform);
    //    ButtonHelper btnHelp = go.GetComponent<ButtonHelper>();
    //    btnHelp.SetText(name);
    //    btnHelp.btn.onClick.AddListener(met.Invoke);
    //}
    #endregion

#region Fight Log
    public void Print(string text)
    {
        log.text += text + ".\r\n" + "**********" + "\r\n";
    }

    public void ClearLog()
    {
        log.text = "";
    }
    #endregion

#region LevelSystem
    public void SetCharScreenOn()
    {
        charScreen.SetActive(!charScreen.activeSelf);
    }
#endregion

#region Inventory
    public void SetInventoryScreenOn()
    {
        inventoryScreen.SetActive(!inventoryScreen.activeSelf);
    }
    #endregion

    private void RemoveAllButtons(Transform tr)
    {
        if (tr.childCount <= 0) return;
        for (int i = 0; i < tr.childCount; i++)
        {
            Destroy(tr.GetChild(i).gameObject);
        }
    }

    public void ClearDialogue()
    {
        RemoveAllButtons(dynButtonsHolder.transform);
    }

	public void SetSpellScreenOn()
	{
		SpellScreen.SetActive(!SpellScreen.activeSelf);
	}

	public void SetStartScreenOn()
	{
		StartScreen.SetActive(!StartScreen.activeSelf);
	}

	public void SetDiedScreenOn()
	{
		Died.SetActive(true);
	}

	public void SetDiedScreenOff()
	{
		Died.SetActive(false);
	}
}
