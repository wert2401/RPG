using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBeastEnemy", menuName = "RPG/Enemies/Beast")]
public class EnemyBeast: Enemy
{
    public override void SetEnemyUI()
    {
        UIManager.instance.AddDynButton("Поговорить", Talk);
        UIManager.instance.AddDynButton("Покормить", TryFeed);

        UIManager.instance.AddExtrDynButton("Ударить", Attack);
        UIManager.instance.AddExtrDynButton("Сбежать", Run);
    }

    public override void Talk()
    {
        Debug.Log("You`r trying to talk with: " + enemyName);
        base.Talk();
    }

    void TryFeed()
    {
        UIManager.instance.Print("Вы кормите " + enemyName + ".");

        int a = Random.Range(0, 2);
        switch (a)
        {
            case 0:
                {
                    SetExtrDynUI();
                    UIManager.instance.Print(enemyName + " отвлекся.");
                    break;
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
        GameLogic.instance.StopFight();
        ResetUI();
    }
}

