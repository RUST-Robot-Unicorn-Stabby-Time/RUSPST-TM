using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EditorAction : BehaviourBase
{
    [SerializeField] UnityEvent callback;

    protected override EvaluationResult OnExecute()
    {
        callback?.Invoke();
        return EvaluationResult.Success;
    }
}
