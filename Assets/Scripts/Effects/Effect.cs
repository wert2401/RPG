using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Effect : ScriptableObject
{
    public int time;
    public bool onPlayer;
    public bool stackable;
    public virtual void Get()
    {
    }
    public virtual void Tick()
    {
    }
    public virtual void End()
    {
    }
}
