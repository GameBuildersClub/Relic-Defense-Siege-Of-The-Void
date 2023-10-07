using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackData
{
    [SerializeField] protected float damage;
    public float Damage { get { return damage; } }

    [SerializeField] public Action<Attackable> beforeDamage;
    [SerializeField] public Action<Attackable, float, float> onDamage; // float, float represents damage taken and damage intended

    public AttackData(float damage = 0)
    {
        this.damage = damage;
    }

    public bool TryAttack(Attackable attackable)
    {
        beforeDamage?.Invoke(attackable);
        float damageTaken = attackable.Damage(this);
        // NOTE: if damageTaken < 0 or == -1 then it is considered a miss
        onDamage?.Invoke(attackable, damageTaken, damage);
        return (damage == damageTaken) || (damage >= 0 && damageTaken >= 0);
    }
}
