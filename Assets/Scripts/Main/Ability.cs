using UnityEngine;
using UnityEditor;

public class Ability : ScriptableObject
{
    public string Name;
    public CreatureNew Target;
    public CreatureNew Caster;
    public int EnergyCost;
    public virtual void OnUse()
    {
    }
}