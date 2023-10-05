using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour
{
    [SerializeField] protected float health = 100;
    public float Health { get { return health; } }

    [SerializeField] protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (!spriteRenderer)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual float Damage(AttackData attackData)
    {
        health -= attackData.Damage;
        return attackData.Damage;
    }
}
