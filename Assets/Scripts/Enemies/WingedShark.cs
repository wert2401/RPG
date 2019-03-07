using UnityEngine;
[CreateAssetMenu(fileName = "NewWingedShark", menuName = "RPG/Creatures/WingedShark")]
public class WingedShark : Creature
{
    public override void React()
    {
        float A = Random.value;
        if (A<0.5)
        {
            GameLogic.instance.EnemyAttack();
        }
        if (A>=0.5&&A<0.6)
        {
            UIManager.instance.Print("Акула взмывает в небо, готовясь к особой атаке");
            GameLogic.instance.enemy.CH += 100;
            GameLogic.instance.EnemyAttack();
            GameLogic.instance.enemy.CH -= 100;
        }
        if (A >= 0.6 && A < 0.8)
        {
            UIManager.instance.Print("Акула рычит, ваша рука дрогнула");
            GameLogic.instance.AddEffect(true, 0, 3);
            Debug.Log("Акула рычит");
        }
        if (A >= 0.8 && A < 0.9)
        {
            UIManager.instance.Print("Акула воет, кажется, ОНА ПЕРЕПОЛНЕН РЕШИМОСТЬЮ");
            UIManager.instance.Print("С каких пор акулы воют?");
            GameLogic.instance.AddEffect(false, 1, 3);
            Debug.Log("Акула воет");
        }
        if (A >= 0.9)
        {
            UIManager.instance.Print("Мощным укусом акула пытается разорвать вашу плоть");
            if (((1 + (lvl / 10)) * damage * Mathf.Pow(0.95f, GameLogic.instance.player.armor)) >= (GameLogic.instance.plHealth / 5))
            {
                GameLogic.instance.AddEffect(true, 2, 5);
            }
            Debug.Log("Акула сделала сильный укус");
        }
    }
}