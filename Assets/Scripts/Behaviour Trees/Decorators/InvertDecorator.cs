using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertDecorator : BehaviourDecorator
{
    protected override EvaluationResult OnExecute()
    {
        if (!Child) return EvaluationResult.Failure;

        if (Child.Execute() == EvaluationResult.Success) return EvaluationResult.Failure;
        else return EvaluationResult.Success;
    }
}
