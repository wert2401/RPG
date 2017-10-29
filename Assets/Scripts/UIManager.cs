using System.Collections;
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
    [Header("Enemy")]
    public Text enName;
    public Text enHealth;
    [Header("Player")]
    public Text plName;
    public Text plHealth;

    [Header("Log")]
    public Text log;

    public delegate void met();

    private void Awake()
    {
        instance = this;
    }

#region Locations
    public void SetLocationName(string name)
    {
        locationName.text = name;
    }

    public void AddLocationButton(string name, int id)
    {
        RemoveAllButtons(locButtonsHolder.transform);

        GameObject go = Instantiate(button, locButtonsHolder.transform);
        ButtonHelper btnHelp = go.GetComponent<ButtonHelper>();
        btnHelp.SetText(name);
        btnHelp.btn.onClick.AddListener(() => GameLogic.instance.MoveToLocation(id));
    }

    public void AddDungeonButton(string name, int id)
    {
        GameObject go = Instantiate(button, locButtonsHolder.transform);
        ButtonHelper btnHelp = go.GetComponent<ButtonHelper>();
        btnHelp.SetText(name);
        btnHelp.btn.onClick.AddListener(() => GameLogic.instance.EnterTheDungeon(id));
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

    public void SetFightUI(Enemy en, Player pl)
    {
        RemoveAllButtons(dynButtonsHolder.transform);
        RemoveAllButtons(extrDynButtonsHolder.transform);
        ClearLog();

        locationScreen.SetActive(false);
        fightScreen.SetActive(true);

        en.SetEnemyUI();

        UpdateHealth(en.maxHealth, pl.maxHealth);
        UpdateNames(en.name, pl.name);
    }

    public void UpdateHealth(float enemyHp, float playerHp)
    {
        enHealth.text = enemyHp.ToString();
        plHealth.text = playerHp.ToString();
    }

    public void UpdateNames(string _enName, string _plName)
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

    public void AddDynButton(string name, met met)
    {
        GameObject go = Instantiate(button, dynButtonsHolder.transform);
        ButtonHelper btnHelp = go.GetComponent<ButtonHelper>();
        btnHelp.SetText(name);
        btnHelp.btn.onClick.AddListener(met.Invoke);
    }

    public void AddExtrDynButton(string name, met met)
    {
        GameObject go = Instantiate(button, extrDynButtonsHolder.transform);
        ButtonHelper btnHelp = go.GetComponent<ButtonHelper>();
        btnHelp.SetText(name);
        btnHelp.btn.onClick.AddListener(met.Invoke);
    }
    #endregion

#region Fight Log
    public void Print(string text)
    {
        log.text += text + "\r\n" + "**************" + "\r\n";
    }

    public void ClearLog()
    {
        log.text = "";
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


}
