using UnityEngine;
[CreateAssetMenu(fileName = "CursedByDark", menuName = "RPG/Effects/CursedByDark")]
public class CursedByDark : Effect
{
    public override void Get()
    {
        if (onPlayer)
        {
            UIManager.instance.Print("Кажется, вы были прокляты");
        }
        else
            UIManager.instance.Print("Кажется, противник был проклят");
    }
    public override void Tick()
    {
        if (onPlayer)
        {
            UIManager.instance.Print("Проклятие проявило себя");
        }
        else
        {
            UIManager.instance.Print("Проклятие проявило себя");
        }
        base.Tick();
    }
}