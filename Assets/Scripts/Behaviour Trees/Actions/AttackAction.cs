using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BehaviourBase
{
    [SerializeField] string targetKey;
    [SerializeField] int attackIndex;

    protected override EvaluationResult OnExecute()
    {
        Vector3 point;
        if (Tree.blackboard.TryGetValue(targetKey, out Transform target))
        {
            point = target.position;
        }
        else if (!Tree.blackboard.TryGetValue(targetKey, out point)) return EvaluationResult.Failure;

        Actions.Attack(point, attackIndex);
        return EvaluationResult.Success;
    }
}
