using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [HideInInspector]
    public static UIManager instance;

    public GameObject button;
    public GameObject buttonHolder;

    [Header("Location Screen")]
    public GameObject locationScreen;
    public Text locationName;

    [Header("Fight Screen")]
    public GameObject fightScreen;
    [Header("Enemy")] 
    public Text enName;
    public Text enHealth;
    [Header("Player")]
    public Text plName;
    public Text plHealth;

    private void Awake()
    {
        instance = this;
    }

    public void SetLocationName(string name)
    {
        locationName.text = name;
    }

    public void AddLocationButton(string name, int id)
    {
        RemoveAllButtons();

        GameObject go = Instantiate(button, buttonHolder.transform);
        ButtonHelper btnHelp = go.GetComponent<ButtonHelper>();
        btnHelp.SetText(name);
        btnHelp.btn.onClick.AddListener(() => GameLogic.instance.MoveToLocation(id));
    }

    public void SetFightUI(Enemy en, Player pl)
    {
        RemoveAllButtons();
        fightScreen.SetActive(true);
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

    private void RemoveAllButtons()
    {
        if (buttonHolder.transform.childCount <= 0) return;

        for (int i = 0; i < buttonHolder.transform.childCount; i++)
        {
            Destroy(buttonHolder.transform.GetChild(i).gameObject);
        }
    }


}
