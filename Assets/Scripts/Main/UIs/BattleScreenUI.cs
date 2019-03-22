using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BattleScreenUI : MonoBehaviour
{
    public List<GameObject> Allies;
    public List<GameObject> Enemies;
    public GameObject OperatingObject;
    public delegate void Del(CreatureNew CRN);

    public void SetOnStart()
    {
        for (int i = 0; i < 4; i++)
        {
            Allies[i].SetActive(false);
            Enemies[i].SetActive(false);
        }
        for (int i = 0; i < GameLogic.instance.PlayerCharacters.Count; i++)
        {
            Allies[i].SetActive(true);
            Allies[i].transform.Find("Character Sprite").transform.Find("CS").GetComponent<Image>().sprite = GameLogic.instance.PlayerCharacters[i].Sprite;
            Allies[i].transform.Find("Health, Mana, Energy and Effects").transform.Find("Health").transform.Find("Text").GetComponent<Text>().text = "HP:" + GameLogic.instance.PlayerCharacters[i].Health + "/" + GameLogic.instance.PlayerCharacters[i].HealthMax;
            Allies[i].transform.Find("Health, Mana, Energy and Effects").transform.Find("Mana").transform.Find("Text").GetComponent<Text>().text = "MP:" + GameLogic.instance.PlayerCharacters[i].Mana + "/" + GameLogic.instance.PlayerCharacters[i].ManaMax;
            Allies[i].transform.Find("Health, Mana, Energy and Effects").transform.Find("Energy").transform.Find("Text").GetComponent<Text>().text = "EN:" + GameLogic.instance.PlayerCharacters[i].Energy + "/" + GameLogic.instance.PlayerCharacters[i].EnergyMax;
            for (int k = 0; k < 15; k++)
            {
                Allies[i].transform.Find("Health, Mana, Energy and Effects").transform.Find("Grey").transform.Find("Additional").transform.Find("IH (" + k + ")").transform.Find("Image").gameObject.SetActive(false);
            }
            if (GameLogic.instance.PlayerCharacters[i].Effects.Count<=15)
                for (int k = 0; k < GameLogic.instance.PlayerCharacters[i].Effects.Count; k++)
                {
                    Allies[i].transform.Find("Health, Mana, Energy and Effects").transform.Find("Grey").transform.Find("Additional").transform.Find("IH (" + k + ")").transform.Find("Image").gameObject.SetActive(true);
                    Allies[i].transform.Find("Health, Mana, Energy and Effects").transform.Find("Grey").transform.Find("Additional").transform.Find("IH (" + k + ")").transform.Find("Image").GetComponent<Image>().sprite = GameLogic.instance.PlayerCharacters[i].Effects[k].Sprite;
                }
            else
                for (int k = 0; k < 15; k++)
                {
                    Allies[i].transform.Find("Health, Mana, Energy and Effects").transform.Find("Grey").transform.Find("Additional").transform.Find("IH (" + k + ")").transform.Find("Image").gameObject.SetActive(true);
                    Allies[i].transform.Find("Health, Mana, Energy and Effects").transform.Find("Grey").transform.Find("Additional").transform.Find("IH (" + k + ")").transform.Find("Image").GetComponent<Image>().sprite = GameLogic.instance.PlayerCharacters[i].Effects[k].Sprite;
                }
            Allies[i].transform.Find("Info Button").GetComponent<Button>().onClick.RemoveAllListeners();
            FixThisShittyButton(InfoScreenUI.instance.SetEverything, i);
        }

        for (int i = 0; i < GameLogic.instance.EnemyCharacters.Count; i++)
        {
            Enemies[i].SetActive(true);
            Enemies[i].GetComponent<Image>().sprite = GameLogic.instance.EnemyCharacters[i].Sprite;
            Enemies[i].GetComponent("Health, Mana, Energy and Effects").GetComponent("Health").GetComponent<Text>().text = "HP:" + GameLogic.instance.EnemyCharacters[i].Health + "/" + GameLogic.instance.EnemyCharacters[i].HealthMax;
            Enemies[i].GetComponent("Health, Mana, Energy and Effects").GetComponent("Mana").GetComponent<Text>().text = "MP:" + GameLogic.instance.EnemyCharacters[i].Mana + "/" + GameLogic.instance.EnemyCharacters[i].ManaMax;
            Enemies[i].GetComponent("Health, Mana, Energy and Effects").GetComponent("Energy").GetComponent<Text>().text = "EN:" + GameLogic.instance.EnemyCharacters[i].Energy + "/" + GameLogic.instance.EnemyCharacters[i].EnergyMax;
            for (int k = 0; k < 15; k++)
            {
                Enemies[i].GetComponent("Health, Mana, Energy and Effects").GetComponent("Grey").GetComponent("Additional").GetComponent("IH (" + k + ")").GetComponent<Image>().sprite = null;
            }
            if (GameLogic.instance.EnemyCharacters[i].Effects.Count <= 15)
                for (int k = 0; k < GameLogic.instance.EnemyCharacters[i].Effects.Count; k++)
                {
                    Enemies[i].GetComponent("Health, Mana, Energy and Effects").GetComponent("Grey").GetComponent("Additional").GetComponent("IH (" + k + ")").GetComponent<Image>().sprite = GameLogic.instance.EnemyCharacters[i].Effects[k].Sprite;
                }
            else
                for (int k = 0; k < 15; k++)
                {
                    Enemies[i].GetComponent("Health, Mana, Energy and Effects").GetComponent("Grey").GetComponent("Additional").GetComponent("IH (" + k + ")").GetComponent<Image>().sprite = GameLogic.instance.EnemyCharacters[i].Effects[k].Sprite;
                }
            Enemies[i].GetComponent<Button>().onClick.RemoveAllListeners();
            Enemies[i].GetComponent<Button>().onClick.AddListener(() => InfoScreenUI.instance.SetEverything(GameLogic.instance.EnemyCharacters[i]));
        }
    }

    public void FixThisShittyButton(Del del, int ID)
    {
        Allies[ID].transform.Find("Info Button").GetComponent<Button>().onClick.AddListener(() => del(GameLogic.instance.PlayerCharacters[ID]));
    }
}
