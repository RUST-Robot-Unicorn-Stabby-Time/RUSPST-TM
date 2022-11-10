using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToCondition : BehaviourBase
{
    [SerializeField] string pointKey = "Target";
    [SerializeField] float compareDistance = 5.0f;
    [SerializeField] Direction direction = Direction.Greater;

    protected override EvaluationResult OnExecute()
    {
        Vector3 point;
        if (Tree.blackboard.TryGetValue(pointKey, out Transform pointTransform))
        {
            point = pointTransform.position;
        }
        else if (!Tree.blackboard.TryGetValue(pointKey, out point)) return EvaluationResult.Failure;

        float distance = (point - Actions.transform.position).magnitude;
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
