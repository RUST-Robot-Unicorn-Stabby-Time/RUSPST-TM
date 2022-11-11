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
            if (path == null) path = new NavMeshPath();
            NavMesh.CalculatePath(Actions.transform.position - Vector3.up, point, 0, path);
            corner = 0;
            lastQueriedPoint = point;
        }

        Vector3 v;
        if (corner < path.corners.Length)
        {
            v = (path.corners[corner] - Actions.transform.position);

            if (v.sqrMagnitude < goodEnoughDistance * goodEnoughDistance)
            {
                corner++;
            }
        }
        else
        {
            v = (point - Actions.transform.position);
        }
        Actions.MoveDirection = v.normalized;
        Actions.FaceDirection = v.normalized;

        return EvaluationResult.Success;
    }
}
