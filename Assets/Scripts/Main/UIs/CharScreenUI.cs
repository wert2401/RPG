using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharScreenUI : MonoBehaviour {
    [Header("Player")]
    public Player player;

    [Header("Characteristics TextObjects")]
    public Text lvl;
    public Text exp;
    public Text freePoints;
    public Text strenght;
    public Text agility;
    public Text intelligence;
    public Text concentration;

    private void Start()
    {
        UpdateStats();
        player.onStatsChanged += UpdateStats;
    }

    private void UpdateStats()
    {
        lvl.text = player.lvl.ToString();
        exp.text = player.exp.ToString() + "/" + player.needExp.ToString();
        freePoints.text = player.freePoints.ToString();
        strenght.text = player.strenght.ToString();
        agility.text = player.agility.ToString();
        intelligence.text = player.intelligence.ToString();
        concentration.text = player.concentration.ToString();
    }
}
