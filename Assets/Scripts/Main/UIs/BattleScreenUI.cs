using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BattleScreenUI : MonoBehaviour
{
    public List<GameObject> Allies;
    public List<GameObject> Enemies;

    public void SetOnStart()
    {
        for (int i = 0; i < 4; i++)
        {

        }
        for (int i = 0; i < GameLogic.instance.PlayerCharacters.Count; i++)
        {
            Allies[i].GetComponent<Image>().sprite = GameLogic.instance.PlayerCharacters[i].Sprite;
            Allies[i].GetComponent("Health, Mana, Energy and Effects").GetComponent("Health").GetComponent<Text>().text = "HP:" + GameLogic.instance.PlayerCharacters[i].Health + "/" + GameLogic.instance.PlayerCharacters[i].HealthMax;
            Allies[i].GetComponent("Health, Mana, Energy and Effects").GetComponent("Mana").GetComponent<Text>().text = "MP:" + GameLogic.instance.PlayerCharacters[i].Mana + "/" + GameLogic.instance.PlayerCharacters[i].ManaMax;
            Allies[i].GetComponent("Health, Mana, Energy and Effects").GetComponent("Energy").GetComponent<Text>().text = "EN:" + GameLogic.instance.PlayerCharacters[i].Energy + "/" + GameLogic.instance.PlayerCharacters[i].EnergyMax;

        }
    }
}
