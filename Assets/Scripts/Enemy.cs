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
    public float fireDmg;
    public float waterDmg;
    public float airDmg;
    public float earthDmg;

    virtual public void Interact()
    {

    }

    virtual public void Attack()
    {

    }
}
