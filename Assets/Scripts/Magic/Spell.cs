using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class Spell : ScriptableObject
{
    [Header("Words")]
    [TextArea]
    public string words;
    public string SpellWords;

    public float ManaCost;
    public bool TF = false;
    public virtual void SpellUse() { }
    public void ManaLost()
    {
        if (GameLogic.instance.plMana < ManaCost)
        {
            UIManager.instance.Print("Вам не хватает маны");
            UIManager.instance.Print("Магическая энергия вырвалась из вашего тела, унеся частичку вашей души");
            GameLogic.instance.GetPureDamage(GameLogic.instance.plMana);
            GameLogic.instance.plMana = 0;
            UIManager.instance.Print("Ай");
            TF = false;
            UIManager.instance.SetSpellScreenOn();
        }
        else
        {
            GameLogic.instance.plMana -= ManaCost;
            TF = true;
        }
    }

}
