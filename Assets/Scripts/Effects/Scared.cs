using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "Scared", menuName = "RPG/Effects/Scared")]
public class Scared : Effect
{
    public override void Get()
    {
        if (onPlayer)
            GameLogic.instance.player.damage -= (5 * GameLogic.instance.enemy.lvl / GameLogic.instance.player.lvl);
        else
            GameLogic.instance.esh.damage -= (5 * GameLogic.instance.player.lvl / GameLogic.instance.enemy.lvl);
    }
    public override void End()
    {
        if (onPlayer)
            GameLogic.instance.player.damage += (5 * GameLogic.instance.enemy.lvl / GameLogic.instance.player.lvl);
        else
            GameLogic.instance.esh.damage += (5 * GameLogic.instance.player.lvl / GameLogic.instance.enemy.lvl);
    }
    public override void Tick()
    {
        time--;
        if (time <= 0)
            End();
    }
}
