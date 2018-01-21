using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHumanoidEnemy", menuName = "RPG/Creatures/Humanoid")]
public class EnemyHumanoid : Creature {
    [TextArea]
    public string answer;

    public override void SetEnemyUI()
    {
        UIManager.instance.AddDynButton("Поговорить", Talk, UIManager.instance.dynButtonsHolder.transform);
    }

    public override void Talk()
    {
        UIManager.instance.Print("Вы говорите с " + CrName);
        UIManager.instance.Print(CrName + " отвечает: " + answer);
        base.Talk();
    }
	public override void React1()
	{
		UIManager.instance.Print(" Реакция первая гуманоидная");
	}
	public override void React2()
	{
		UIManager.instance.Print(" Реакция вторая гуманоидная");
	}
	public override void React3()
	{
		UIManager.instance.Print(" Реакция третья гуманоидная");
	}
}
