using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBeastEnemy", menuName = "RPG/Enemies/Beast")]
public class EnemyBeast: Enemy
{
    public bool eatYou;
    public string reaction;

    public override void SetEnemyUI()
    {
        UIManager.instance.AddDynButton("Поговорить", Talk, UIManager.instance.dynButtonsHolder.transform);
        UIManager.instance.AddDynButton("Покормить", TryFeed, UIManager.instance.dynButtonsHolder.transform);

        UIManager.instance.AddDynButton("Ударить", Attack, UIManager.instance.extrDynButtonsHolder.transform);
        UIManager.instance.AddDynButton("Сбежать", Run, UIManager.instance.extrDynButtonsHolder.transform);
    }

    public override void Talk()
    {
        UIManager.instance.Print("Вы говорите с '" + enemyName+ "'");
        UIManager.instance.Print(reaction);
        base.Talk();
    }

    public override void Attack()
    {
        GameLogic.instance.PlayerAttack();
        ResetUI();
    }

    void TryFeed()
    {
        UIManager.instance.Print("Вы кормите " + enemyName + ".");

        int a = Random.Range(0, 2);
        switch (a)
        {
            case 0:
                {
                    if (!eatYou)
                    {
                        UIManager.instance.Print(enemyName + " отвлекся.");
                        SetExtrDynUI();
                        break;
                    }
                    else
                    {
                        UIManager.instance.Print(enemyName + " хочет съесть только вас.");
                        GameLogic.instance.EnemyAttack();
                        ResetUI();
                        break;
                    }
                }
            case 1:
                {
                    UIManager.instance.Print(enemyName + " не реагирует на вашу еду.");
                    ResetUI();
                    break;
                }
        }
    }

    void Run()
    {
        UIManager.instance.Print("Вы убежали.");
        GameLogic.instance.StopFight(true);
        ResetUI();
    }
}

