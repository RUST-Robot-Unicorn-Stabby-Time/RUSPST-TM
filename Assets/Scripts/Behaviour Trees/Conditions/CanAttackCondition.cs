using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanAttackCondition : BehaviourBase
{
    public static int ConcurrentAttackingEnemies = 2;

    protected override EvaluationResult OnExecute()
    {
        return EnemyActions.EnemiesAttacking <= ConcurrentAttackingEnemies ? EvaluationResult.Success : EvaluationResult.Failure;
    }
}
