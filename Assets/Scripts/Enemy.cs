using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "RPG/Enemy")]
public class Enemy : ScriptableObject {
    public string enemyName;
	public TypeOfEnemy type;
    public float maxHealth;
    public float damage;

    [Header("Elements Damage")]
    public float fireDmg;
    public float waterDmg;
    public float airDmg;
    public float lightDmg;
    public float darkDmg;

    [Header("Elements Resist/Vulnerability ")]
    public float fireRes;
    public float waterRes;
    public float airRes;
    public float lightRes;
    public float darkRes;

    virtual public void Interact()
    {

    }

    virtual public void Attack()
    {

    }
}
