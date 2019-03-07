using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBeastEnemy", menuName = "RPG/Creatures/Beast")]
public class EnemyBeast: Creature
{
    public bool eatYou;
    public string reaction;

    public override void SetEnemyUI()
    {
        UIManager.instance.AddDynButton("Поговорить", Talk, UIManager.instance.dynButtonsHolder.transform);
        UIManager.instance.AddDynButton("Покормить", TryFeed, UIManager.instance.dynButtonsHolder.transform);
    }

    public override void Talk()
    {
        UIManager.instance.Print("Вы говорите с '" + CrName+ "'");
        UIManager.instance.Print(reaction);
        base.Talk();
    }

	public override void React1()
	{
        UIManager.instance.Print("Зверь рычит, ваша рука дрогнула");
        GameLogic.instance.AddEffect(true, 0, 3);
        Debug.Log("Зверь рычит");
    }
	public override void React2()
	{
        UIManager.instance.Print("Зверь воет, кажется, ОН ПЕРЕПОЛНЕН РЕШИМОСТЬЮ");
        GameLogic.instance.AddEffect(false, 1, 3);
        Debug.Log("Зверь воет");
    }
	public override void React3()
	{
        UIManager.instance.Print("Мощным ударом зверь пытается разорвать вашу плоть");
        if(((1 + (lvl / 10)) * damage * Mathf.Pow(0.86f, GameLogic.instance.player.armor)) >=(GameLogic.instance.plHealth/5))
        {
            GameLogic.instance.AddEffect(true, 2, 5);
        }
        Debug.Log("Зверь сделал удар лапой");
    }

    public void HitAfterFeeding()
    {
        UIManager.instance.RemoveAllButtons(UIManager.instance.dynButtonsHolder.transform);
        ResetUI();
    }

    void TryFeed()
    {
        UIManager.instance.Print("Вы кормите " + CrName);

        int a = Random.Range(0, 2);
        switch (a)
        {
            case 0:
                {
                    if (!eatYou)
                    {
                        UIManager.instance.Print(CrName + " отвлекся");
                        UIManager.instance.RemoveAllButtons(UIManager.instance.dynButtonsHolder.transform);
                        UIManager.instance.AddDynButton("Ударить", HitAfterFeeding, UIManager.instance.dynButtonsHolder.transform);
                        UIManager.instance.AddDynButton("Сбежать", Run, UIManager.instance.dynButtonsHolder.transform);
                        break;
                    }
                    else
                    {
                        UIManager.instance.Print(CrName + " хочет съесть только вас");
                        GameLogic.instance.EnemyAttack();
                        ResetUI();
                        break;
                    }
                }
            case 1:
                {
                    UIManager.instance.Print(CrName + " не реагирует на вашу еду");
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

   