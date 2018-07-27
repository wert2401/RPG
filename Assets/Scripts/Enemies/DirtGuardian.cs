using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "NewGuardian", menuName = "RPG/Creatures/DirtGuardian")]
public class DirtGuardian : Creature
{
    public override void React()
    {
        float A = Random.value;
        if (A<0.5)
        {
            GameLogic.instance.EnemyAttack();
        }
        if (A>=0.5&&A<0.8)
        {
            float b = Random.value;
            UIManager.instance.Print("Страж метает в вас огромный булыжник");
            if (b > (GameLogic.instance.player.evasChance + 10 - GameLogic.instance.enemy.accuracy) / 100)
            {
                GameLogic.instance.GetEarthDamage(damage * 2);
            }
            else
                UIManager.instance.Print("Вы увернулись");
        }
        if (A>=0.8&&A<0.9)
        {
            float b = Random.value;
            UIManager.instance.Print("Страж пытается схватить вас ожившей грязью");
            if (b > (GameLogic.instance.player.evasChance + 50 - GameLogic.instance.enemy.accuracy) / 100)
            {
                GameLogic.instance.PlStun += 3;
            }
            else
                UIManager.instance.Print("Вы увернулись");
        }
        if (A>=0.9)
        {
            bool AlreadyHaveArmor = false;
            for (int i = 0; i < GameLogic.instance.EnemyEffects.Count; i++)
            {
                if (GameLogic.instance.EnemyEffects[i].id==6)
                {
                    AlreadyHaveArmor = true;
                }
            }
            if (AlreadyHaveArmor)
                GameLogic.instance.AddEffect(false, 5,3);
            else
                GameLogic.instance.EnemyAttack();
        }
    }
}