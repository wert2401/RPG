using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "NIPP", menuName = "RPG/Spell/NIPP")]
public class IPP : Spell
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