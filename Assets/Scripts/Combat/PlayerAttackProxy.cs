using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class PlayerAttackProxy : MonoBehaviour
{
    [SerializeField] protected AttackData attackData;
    public AttackData AttackData { get { return attackData; } }
    protected List<Enemy> attackedEnemies = new List<Enemy>();
    [SerializeField] Collider2D collider;

    private void Awake()
    {
        if (!collider)
            collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryAttackEnemy(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TryAttackEnemy(collision);
    }

    protected bool TryAttackEnemy(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (!attackedEnemies.Contains(enemy))
            {
                attackData.TryAttack(enemy);
                Debug.Log("Attacked enemy.");
                attackedEnemies.Add(enemy);
                return true;
            }
        }
        return false;
    }

    void OnDisable()
    {
        attackedEnemies.Clear();
    }

    private void OnEnable()
    {
        List<Collider2D> collisions = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.NoFilter();
        collider.OverlapCollider(contactFilter, collisions);
        foreach (Collider2D collision in collisions)
        {
            TryAttackEnemy(collision);
        }
    }
}
