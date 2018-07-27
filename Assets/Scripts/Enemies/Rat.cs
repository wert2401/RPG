using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "NewRat", menuName = "RPG/Creatures/Rat")]
public class Rat : Creature
{
    public override void React()
    {
        float A = Random.value;
        if (A < 0.5)
        {
            GameLogic.instance.EnemyAttack();
        }
        if (A>=0.5&&A<0.8)
        {
            UIManager.instance.Print("Крыса приходит в ярость!");
            GameLogic.instance.AddEffect(false, 1, 3);
        }
        if (A>=0.8)
        {
            UIManager.instance.Print("Крыса делает незначительный укус");
            GameLogic.instance.GetPhysDamage(damage * 0.5f);
            GameLogic.instance.AddEffect(true,4,3);
        }
    }
    public override void SetEnemyUI()
    {
        UIManager.instance.AddDynButton("Поговорить", Talk, UIManager.instance.dynButtonsHolder.transform);
        UIManager.instance.AddDynButton("Покормить", TryFeed, UIManager.instance.dynButtonsHolder.transform);
    }
    void TryFeed()
    {
        UIManager.instance.Print("Вы кормите крысу");
        UIManager.instance.Print("Крыса отвлеклась");
        UIManager.instance.RemoveAllButtons(UIManager.instance.dynButtonsHolder.transform);
        UIManager.instance.AddDynButton("Ударить", HitAfterFeeding, UIManager.instance.dynButtonsHolder.transform);
        UIManager.instance.AddDynButton("Сбежать", Run, UIManager.instance.dynButtonsHolder.transform);
        
    }
    public void HitAfterFeeding()
    {
        GameLogic.instance.PlayerHit();
        UIManager.instance.RemoveAllButtons(UIManager.instance.dynButtonsHolder.transform);
        ResetUI();
    }
    void Run()
    {
        UIManager.instance.Print("Вы убежали");
        GameLogic.instance.StopFight(true);
        ResetUI();
    }
}
