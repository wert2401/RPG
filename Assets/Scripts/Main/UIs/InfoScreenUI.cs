using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InfoScreenUI : MonoBehaviour
{
    public static InfoScreenUI instance;
    public GameObject ScreenItSelf;
    public Image Image;
    public Text Name;
    public Text Description;
    public Text Health;
    public Text Mana;
    public Text Energy;
    public Text DMG;
    public Text RESISTS;
    public GameObject Abilities;
    public GameObject Effects;
    public GameObject DM;
    public List<string> AL;
    public List<Text> ATL;
    public List<string> EL;
    public List<Text> ETL;
    public Text BasicText;
    public int AI;
    public int EI;
    public GameObject UABH;
    public GameObject DABH;
    public GameObject UEBH;
    public GameObject DEBH;

    public Transform TestShit;

    private void Awake()
    {
        instance = this;
    }

    public void WriteEffects(CreatureNew CS)
    {
        SaveEffects(CS);
        for (int i = 0; i < 5; i++)
        {
            ETL[i].text = "";
        }
        if (EL.Count >= 5)
            for (int i = 0; i < 5; i++)
            {
                ETL[i].text = EL[i];
            }
        else
            for (int i = 0; i < EL.Count; i++)
            {
                ETL[i].text = EL[i];
            }
    }

    public void WriteAbilities(CreatureNew CS)
    {
        SaveAbilities(CS);
        for (int i = 0; i < 5; i++)
        {
            ATL[i].text = "";
        }
        if (AL.Count >= 5)
            for (int i = 0; i < 5; i++)
            {
                ATL[i].text = AL[i];
            }
        else
            for (int i = 0; i < AL.Count; i++)
            {
                ATL[i].text = AL[i];
            }
    }

    public void SaveEffects(CreatureNew CS)
    {
        EL.Clear();
        for (int i = 0; i < CS.Effects.Count; i++)
        {
            EL.Add(CS.Effects[i].EffectName);
        }
    }

    public void SaveAbilities(CreatureNew CS)
    {
        AL.Clear();
        for (int i = 0; i < CS.Abilities.Count; i++)
        {
            AL.Add(CS.Abilities[i].Name);
        }
    }

    public void UpEffects()
    {
        EI -= 1;
        DUE();
    }

    public void DownEffects()
    {
        EI += 1;
        DUE();
    }

    public void UpAbilities()
    {
        AI -= 1;
        DUA();
    }

    public void DownAbilities()
    {
        AI += 1;
        DUA();
    }

    public void DUA()
    {
        for (int i = 0; i < 5; i++)
        {
            ATL[i].text = AL[i + AI];
        }
        if (AI == 0)
            UABH.SetActive(false);
        else
            UABH.SetActive(true);
        if (AI < AL.Count - 5)
            DABH.SetActive(true);
        else
            DABH.SetActive(false);
    }

    public void DUE()
    {
        for (int i = 0; i < 5; i++)
        {
            ETL[i].text = EL[i + EI];
        }
        if (EI == 0)
            UEBH.SetActive(false);
        else
            UEBH.SetActive(true);
        if (EI < EL.Count - 5)
            DEBH.SetActive(true);
        else
            DEBH.SetActive(false);
    }

    public void SetEverything(CreatureNew CS)
    {
        AI = 0;
        EI = 0;
        WriteAbilities(CS);
        WriteEffects(CS);
        UEBH.SetActive(false);
        UABH.SetActive(false);
        DEBH.SetActive(true);
        DABH.SetActive(true);
        if (EL.Count<=5)
            DEBH.SetActive(false);
        if (AL.Count <= 5)
            DABH.SetActive(false);
        Image.sprite = CS.Sprite;
        Name.text = CS.Name + ". Level:" + CS.lvl.ToString();
        Description.text = CS.Description;
        Health.text = "Health:" + CS.Health.ToString() + "/" + CS.HealthMax.ToString();
        Mana.text = "Mana:" + CS.Mana.ToString() + "/" + CS.ManaMax.ToString();
        Energy.text = "Energy:" + CS.Energy.ToString() + "/" + CS.EnergyMax.ToString();
        DMG.text = "Average damage:" + CS.damage.ToString() + " + " + ((CS.pureDmg + CS.waterDmg + CS.darkDmg + CS.earthDmg + CS.fireDmg + CS.lightDmg + CS.airDmg) / 7).ToString();
        RESISTS.text = "Average resist:" + CS.armor.ToString() + " + " + ((CS.waterRes + CS.darkRes + CS.earthRes + CS.fireRes + CS.lightRes + CS.airRes) / 6).ToString();
        ScreenItSelf.SetActive(true);
    }
}
