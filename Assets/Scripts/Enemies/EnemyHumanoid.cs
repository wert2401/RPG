using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHumanoidEnemy", menuName = "RPG/Enemies/Humanoid")]
public class EnemyHumanoid : Enemy {
    [TextArea]
    public string answer;

    public override void SetEnemyUI()
    {
        UIManager.instance.AddDynButton("Поговорить", Talk, UIManager.instance.dynButtonsHolder.transform);
    }

    public override void Talk()
    {
        UIManager.instance.Print("Вы говорите с " + enemyName);
        UIManager.instance.Print(enemyName + " отвечает: " + answer);
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
