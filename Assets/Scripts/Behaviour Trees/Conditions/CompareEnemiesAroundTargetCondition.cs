using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CompareEnemiesAroundTargetCondition : BehaviourBase
{
    [SerializeField] string targetKey = "Target";
    [SerializeField] float range = 5.0f;
    [SerializeField] int threshold = 4;

    protected override EvaluationResult OnExecute()
    {
        Vector3 targetPoint;
        if (Tree.blackboard.TryGetValue(targetKey, out Transform target))
        {
            targetPoint = target.position;
        }
        else if (!Tree.blackboard.TryGetValue(targetKey, out targetPoint)) return EvaluationResult.Failure;

        int enemiesNearPlayer = 0;
        foreach (var enemy in EnemyActions.Enemies)
        {
            if ((enemy.transform.position - targetPoint).sqrMagnitude < range * range)
            {
                enemiesNearPlayer++;
            }
        }
        return enemiesNearPlayer > threshold ? EvaluationResult.Success : EvaluationResult.Failure;
    }
}
