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
    }

    public override void Attack()
    {
        GameLogic.instance.PlayerAttack();
        ResetUI();
    }
	public override void React1()
	{
		UIManager.instance.Print(" Реакция первая звериная");
	}
	public override void React2()
	{
		UIManager.instance.Print(" Реакция вторая звериная");
	}
	public override void React3()
	{
		UIManager.instance.Print(" Реакция третья звериная");
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

///001///
///001///
///010///
///001///__________________________   
///100///                          |\ 
///011///                          ||
///111///__________________________|/ 
///100///
///001///
///010///
///010///


//Молоток выше
//А весной цветёт сакура

//На потом
//{
//Debug.Log("Spell");

//		UIManager.instance.Print("Вы успешно применяете заклинание");
//		UIManager.instance.UpdateHealth(Mathf.Round(enHealth), Mathf.Round(plHealth));
//		UIManager.instance.SetSpellScreenOn();

//        React();
//	}

   