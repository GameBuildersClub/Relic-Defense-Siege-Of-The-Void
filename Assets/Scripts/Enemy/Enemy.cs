using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected List<EnemyBehavior> behaviors = new List<EnemyBehavior>();
    [SerializeField] protected PlayerController target;

    private void Awake()
    {
        target = GameManager.Instance.Player;
    }

    public void RunBehaviors()
    {
        foreach (EnemyBehavior behavior in behaviors)
        {
            if (behavior.ShouldAct(this, target))
            {
                behavior.Act(this, target);
                break;
            }
        }
    }

    void FixedUpdate()
    {
        RunBehaviors();
    }
}
