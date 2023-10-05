using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : ScriptableObject
{
    public abstract bool ShouldAct(Enemy enemy, PlayerController target);
    public abstract void Act(Enemy enemy, PlayerController target);
}
