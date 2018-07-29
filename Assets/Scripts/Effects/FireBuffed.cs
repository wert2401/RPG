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
            GameLogic.instance.GetFireDamage(GameLogic.instance.player.lvl);
        }
        else
        {
            UIManager.instance.Print("Противник, чёрт возьми, горит!");
            GameLogic.instance.DealFireDamage(GameLogic.instance.enemy.lvl);
        }
        base.Tick();
    }
    public override void FunctionOnStartAttack()
    {
        if (onPlayer)
        {
            float a = Random.value;
            if(a<(0.5f+GameLogic.instance.player.lvl / GameLogic.instance.enemy.lvl))
            {

                UIManager.instance.Print("Противник, чёрт возьми, загорелся!");
                UIManager.instance.Print("Это очень больно, для справочки");
            }
        }
        else
        {
            float a = Random.value;
            if (a < (0.5f + GameLogic.instance.enemy.lvl / GameLogic.instance.player.lvl))
            {

                UIManager.instance.Print("Вы, чёрт возьми, загорелись!");
                UIManager.instance.Print("Это очень больно, для справочки");
            }
        }
    }
    public override void FunctionOnStartBeingAttacked()
    {
        if(onPlayer)
        {
            float a = Random.value;
            if (a < (0.5f + GameLogic.instance.player.lvl / GameLogic.instance.enemy.lvl))
            {
                GameLogic.instance.DealFireDamage(GameLogic.instance.player.lvl);
                UIManager.instance.Print("Противник, чёрт возьми, обжёгся об ваше пламя!");
            }
        }
        else
        {
            float a = Random.value;
            if (a < (0.5f + GameLogic.instance.enemy.lvl / GameLogic.instance.player.lvl))
            {
                GameLogic.instance.GetFireDamage(GameLogic.instance.enemy.lvl);
                UIManager.instance.Print("Вы, чёрт возьми, обожглись об пламя противника!");
            }
        }
    }
}