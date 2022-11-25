using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BehaviourBase
{
    [SerializeField] string targetKey;
    [SerializeField] int attackIndex;
    [SerializeField] float distance;

    protected override EvaluationResult OnExecute()
    {
        Vector3 point;
        if (Tree.blackboard.TryGetValue(targetKey, out Transform target))
        {
            point = target.position;
        }
        else if (!Tree.blackboard.TryGetValue(targetKey, out point)) return EvaluationResult.Failure;

        if ((point - Actions.transform.position).sqrMagnitude < distance * distance)
        {
            Actions.Attack(attackIndex);
            return EvaluationResult.Success;
        }

        return EvaluationResult.Failure;
    }
}
