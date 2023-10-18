using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float speed = 3;
    [SerializeField] float attackDistanceThreshold = 1.1f;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerController target;

    private void Awake()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        if (!rb)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (target == null)
        {
            target = FindObjectOfType<PlayerController>();
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        Debug.Log("Health of enemy: " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        MoveAndAttack();
    }

    public void MoveAndAttack()
    {
        Vector3 difference = target.transform.position - transform.position;
        if (difference.magnitude > attackDistanceThreshold)
        {
            Vector3 velocity = difference.normalized * speed;
            rb.velocity = velocity;

            if (velocity.x < 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = -1;
                transform.localScale = scale;
            }
            else if (velocity.x > 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = 1;
                transform.localScale = scale;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            anim.SetTrigger("Attack");
        }
    }
}
