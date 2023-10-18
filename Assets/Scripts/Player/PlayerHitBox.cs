using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [SerializeField] float damage = 50;
    [SerializeField] PlayerController controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            float currentDamage = damage;
            if (controller.PunchCharged())
            {
                currentDamage *= 2;
            }
            enemy.Damage(currentDamage);
        }
    }
}
