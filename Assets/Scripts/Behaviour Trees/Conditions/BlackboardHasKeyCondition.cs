using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardHasKeyCondition : BehaviourBase
{
    [SerializeField] string key;

    protected override EvaluationResult OnExecute()
    {
        return Tree.blackboard.ValueExists(key) ? EvaluationResult.Success : EvaluationResult.Failure;
    }
}