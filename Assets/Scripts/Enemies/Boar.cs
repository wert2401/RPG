using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "NewBoar", menuName = "RPG/Creatures/Boar")]
public class Boar : Creature
{
    public override void React()
    {
        float A = Random.value;
        if (A<0.8)
        {
            GameLogic.instance.EnemyAttack();
        }
        if (A>=0.8)
        {
            float b = Random.value;
            if (b>=(GameLogic.instance.player.evasChance+60-GameLogic.instance.enemy.accuracy)/100&& GameLogic.instance.player.armor<5)
            {
                UIManager.instance.Print("Кабан делает рывок, оглушая вас мощным ударом");
                GameLogic.instance.PlStun += 3;
            }
            else
            {
                if (b < (GameLogic.instance.player.evasChance + 60 - GameLogic.instance.enemy.accuracy) / 100)
                    UIManager.instance.Print("Кабан делает рывок, но промахивается по вам");
                else
                    UIManager.instance.Print("Кабан делает рывок, но ваша броня позволила устоять");
            }
        }
    }
}
