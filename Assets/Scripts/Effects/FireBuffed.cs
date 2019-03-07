using UnityEngine;
[CreateAssetMenu(fileName = "FireBuffed", menuName = "RPG/Effects/FireBuffed")]
public class FireBuffed : Effect
{
    public override void Get()
    {
        if(onPlayer)
        {
            GameLogic.instance.player.fireDmg += GameLogic.instance.player.lvl * 3;
            GameLogic.instance.player.CD += GameLogic.instance.player.lvl * 0.01F;
            UIManager.instance.Print("Вы, чёрт возьми, загорелись!");
        }
        else
        {
            GameLogic.instance.enemy.fireDmg += GameLogic.instance.enemy.lvl * 3;
            GameLogic.instance.enemy.CD += GameLogic.instance.enemy.lvl * 0.01F;
            UIManager.instance.Print("Противник, чёрт возьми, загорелся!");
        }
    }
    public override void End()
    {
        if (onPlayer)
        {
            GameLogic.instance.player.fireDmg -= GameLogic.instance.player.lvl * 3;
            GameLogic.instance.player.CD -= GameLogic.instance.player.lvl * 0.01F;
            UIManager.instance.Print("Вы, чёрт возьми, потухли!");
        }
        else
        {
            GameLogic.instance.enemy.fireDmg -= GameLogic.instance.enemy.lvl * 3;
            GameLogic.instance.enemy.CD -= GameLogic.instance.enemy.lvl * 0.01F;
            UIManager.instance.Print("Противник, чёрт возьми, потух!");
        }
    }
    public override void Tick()
    {
        if(onPlayer)
        {
            UIManager.instance.Print("Вы, чёрт возьми, горите!");
        }
        else
        {
            UIManager.instance.Print("Противник, чёрт возьми, горит!");
        }
        base.Tick();
    }
}