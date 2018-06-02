using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "Rage", menuName = "RPG/Effects/Rage")]
public class Rage : Effect
{
    public override void Get()
    {
        if (onPlayer)
        {
            GameLogic.instance.player.damage += (3 * GameLogic.instance.player.lvl);
            UIManager.instance.Print("Вы в ярости!");
        }
        else
        {
            GameLogic.instance.esh.damage += (3 * GameLogic.instance.enemy.lvl);
            UIManager.instance.Print("Ваш противник в ярости!");
        }
    }
    public override void End()
    {
        if (onPlayer)
        {
            GameLogic.instance.player.damage -= (3 * GameLogic.instance.player.lvl);
            UIManager.instance.Print("Вы больше не в ярости");
        }
        else
        {
            GameLogic.instance.esh.damage -= (3 * GameLogic.instance.enemy.lvl);
            UIManager.instance.Print("Ваш противник больше не в ярости");
        }
    }
    public override void Tick()
    {
        time--;
        if (time <= 0)
            End();
    }
}
