using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathToPointAction : BehaviourBase
{
    [SerializeField] string pointKey = "EQS";
    [SerializeField] float goodEnoughDistance = 1.0f;

    int corner = 0;
    NavMeshPath path;
    Vector3 lastQueriedPoint = Vector3.zero;

    protected override EvaluationResult OnExecute()
    {
        Vector3 point;
        if (Tree.blackboard.TryGetValue(pointKey, out Transform target))
        {
            point = target.position;
        }
        else if (!Tree.blackboard.TryGetValue(pointKey, out point)) return EvaluationResult.Failure;

        if ((lastQueriedPoint - point).sqrMagnitude > goodEnoughDistance * goodEnoughDistance || path == null)
        {
            NavMesh.CalculatePath(Actions.transform.position - Vector3.up, point, 0, path);
            corner = 0;
            lastQueriedPoint = point;
        }

        if (corner < path.corners.Length)
        {
            Vector3 v = (path.corners[corner] - Actions.transform.position);
            Actions.MoveDirection = v.normalized;
            Actions.FaceDirection = v.normalized;

            if (v.sqrMagnitude < goodEnoughDistance * goodEnoughDistance)
            {
                corner++;
            }
        }

        return EvaluationResult.Success;
    }
}
