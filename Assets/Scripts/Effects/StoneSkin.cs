using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "Stone Skin", menuName = "RPG/Effects/Stone Skin")]
public class StoneSkin : Effect
{
    public override void Get()
    {
        if (onPlayer)
        {
            GameLogic.instance.player.armor += 5;
            GameLogic.instance.player.evasChance -= 5;
            GameLogic.instance.player.accuracy -= 5;
            UIManager.instance.Print("Вы покрываетесь слоем из камня");
        }
        else
        {
            GameLogic.instance.enemy.armor += 5;
            GameLogic.instance.enemy.evasChance -= 5;
            GameLogic.instance.enemy.accuracy -= 5;
            UIManager.instance.Print("Противник покрывается слоем из камня");
        }
    }
    public override void End()
    {
        if (onPlayer)
        {
            GameLogic.instance.player.armor -= 5;
            GameLogic.instance.player.evasChance += 5;
            GameLogic.instance.player.accuracy += 5;
            UIManager.instance.Print("Слой камня на вас рассыпался в пыль");
        }
        else
        {
            GameLogic.instance.enemy.armor -= 5;
            GameLogic.instance.enemy.evasChance += 5;
            GameLogic.instance.enemy.accuracy += 5;
            UIManager.instance.Print("Слой камня на противнике рассыпался в пыль");
        }
    }
}