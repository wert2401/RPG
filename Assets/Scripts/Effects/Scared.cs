using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "Scared", menuName = "RPG/Effects/Scared")]
public class Scared : Effect
{
    public override void Get()
    {
        if (onPlayer)
        {
            GameLogic.instance.player.damage -= (5 * GameLogic.instance.enemy.lvl / GameLogic.instance.player.lvl);
            UIManager.instance.Print("Вы в ужасе");
        }
        else
        {
            GameLogic.instance.esh.damage -= (5 * GameLogic.instance.player.lvl / GameLogic.instance.enemy.lvl);
            UIManager.instance.Print("Ваш противник в ужасе");
        }
    }
    public override void End()
    {
        if (onPlayer)
        {
            GameLogic.instance.player.damage += (5 * GameLogic.instance.enemy.lvl / GameLogic.instance.player.lvl);
            UIManager.instance.Print("Вы больше не в ужасе");
        }
        else
        {
            GameLogic.instance.esh.damage += (5 * GameLogic.instance.player.lvl / GameLogic.instance.enemy.lvl);
            UIManager.instance.Print("Ваш противник больше не в ужасе");
        }
    }
    public override void Tick()
    {
        time--;
        if (time <= 0)
            End();
    }
}
