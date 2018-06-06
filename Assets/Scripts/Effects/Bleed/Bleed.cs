using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "Bleed", menuName = "RPG/Effects/Bleed")]
public class Bleed : Effect
{
    public override void Get()
    {
        if(onPlayer)
            UIManager.instance.Print("Вы истекаете кровью!");
        else
            UIManager.instance.Print("Ваш противник истекает кровью!");
    }
    public override void Tick()
    {
        if (onPlayer)
        {
            UIManager.instance.Print("Вы истекаете кровью!");
            GameLogic.instance.GetPureDamage(2 * GameLogic.instance.enemy.lvl);
        }
        else
        {
            UIManager.instance.Print("Ваш противник истекает кровью!");
            GameLogic.instance.DealPureDamage(2 * GameLogic.instance.player.lvl);
        }
        time--;
    }
}
