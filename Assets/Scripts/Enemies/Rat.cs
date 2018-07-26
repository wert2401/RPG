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
}