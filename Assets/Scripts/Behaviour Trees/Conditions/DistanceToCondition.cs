using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToCondition : BehaviourBase
{
    [SerializeField] string aKey = "Target";
    [SerializeField] string bKey = "position";
    [SerializeField] float compareDistance = 5.0f;
    [SerializeField] Direction direction = Direction.Greater;

    protected override EvaluationResult OnExecute()
    {
        if (!GetPointFromBlackboard(aKey, out Vector3 a)) return EvaluationResult.Failure;
        if (!GetPointFromBlackboard(bKey, out Vector3 b)) return EvaluationResult.Failure;

        float distance = (a - b).magnitude;
        bool result = false;
        switch (direction)
        {
            case Direction.Less:
                result = distance < compareDistance;
                break;
            case Direction.Greater:
                result = distance > compareDistance;
                break;
            default:
                break;
        }
        return result ? EvaluationResult.Success : EvaluationResult.Failure;
    }

    public enum Direction
    {
        Less,
        Greater,
    }
}
