using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "FireBuffSpell", menuName = "RPG/Spell/FireBuffSpell")]
public class FireBuffSpell : Spell
{
    public override void SpellUse()
    {
        ManaLost();
        if(!TF)
        {
            UIManager.instance.SetSpellScreenOn();
            GameLogic.instance.React();
            return;
        }
        UIManager.instance.Print(SpellWords);
        GameLogic.instance.AddEffect(true, 3, 3);
        UIManager.instance.SetSpellScreenOn();
        GameLogic.instance.React();
    }
}