using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    protected float timeLastHit = 0f;
    protected float hitColorDuration = 0.5f;
    [SerializeField] protected Color hitColor = Color.red;
    protected Color initialColor;

    private void Awake()
    {
        if (!spriteRenderer)
            spriteRenderer = GetComponent<SpriteRenderer>();

        initialColor = spriteRenderer.color;
    }

    public override float Damage(AttackData attackData)
    {
        float dmg = base.Damage(attackData);
        if (dmg > 0)
        {
            timeLastHit = Time.fixedTime;
        }
        return dmg;
    }

    private void FixedUpdate()
    {
        float hitTimeDiff = Time.fixedTime - timeLastHit;
        if (hitTimeDiff < hitColorDuration)
        {
            spriteRenderer.color = Color.Lerp(initialColor, hitColor, 1 - (hitTimeDiff / hitColorDuration));
        } else
        {
            spriteRenderer.color = initialColor;
        }

    }
}
