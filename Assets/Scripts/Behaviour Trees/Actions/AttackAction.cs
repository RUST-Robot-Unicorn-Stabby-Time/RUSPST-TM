using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BehaviourBase
{
    [SerializeField] int attackIndex;

    protected override EvaluationResult OnExecute()
    {
        Actions.Attack(attackIndex);
        return EvaluationResult.Success;
    }
}
