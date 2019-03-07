using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "SimpleAttack", menuName = "RPG/SimpleAttack")]

public class SimpleAttack : Ability
{
    public int HealthBefore;
    public int SummaryDamage;
    public int EvasionRoll;
    public int CritRoll;
    public new int EnergyCost = 5;
    public override void OnUse()
    {
        Target.WasAttacked(Caster);
        HealthBefore = Target.Health;
        Caster.Attacking(Target);
        EvasionRoll = Random.Range(0, 100);
        if (EvasionRoll<=Target.evasChance)
        {
            UIManager.instance.Print("Miss");
        }
        else
        {
            CritRoll = Random.Range(0, 100);
            if (CritRoll<=Caster.CH)
                GameLogic.instance.DealPhysDamage(Mathf.RoundToInt(Caster.damage*Caster.CD/100), Target);
            else
                GameLogic.instance.DealPhysDamage(Caster.damage, Target);
            GameLogic.instance.DealLightDamage(Caster.lightDmg, Target);
            GameLogic.instance.DealDarkDamage(Caster.darkDmg, Target);
            GameLogic.instance.DealEarthDamage(Caster.earthDmg, Target);
            GameLogic.instance.DealFireDamage(Caster.fireDmg, Target);
            GameLogic.instance.DealWaterDamage(Caster.waterDmg, Target);
            GameLogic.instance.DealAirDamage(Caster.airDmg, Target);
            GameLogic.instance.DealPureDamage(Caster.pureDmg, Target);
        }
        SummaryDamage = HealthBefore - Target.Health;
        if (SummaryDamage != 0)
        {
            Caster.DealDamage(Target, SummaryDamage);
        }
        SummaryDamage = HealthBefore - Target.Health;
        if (SummaryDamage != 0)
        {
            Target.GetDamage(Caster, SummaryDamage);
        }
        Caster.Energy -= EnergyCost;
    }
}