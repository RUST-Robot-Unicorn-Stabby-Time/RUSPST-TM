using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathToPointAction : BehaviourBase
{
    [SerializeField] string pointKey = "EQS";
    [SerializeField] float goodEnoughDistance = 1.0f;

    NavMeshPath path;
    Vector3 lastQueriedPoint = Vector3.zero;

    private void OnDrawGizmos()
    {
        if (path != null)
        {
            Gizmos.color = Color.yellow;

            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                Gizmos.DrawLine(path.corners[i], path.corners[i + 1]);
            }
        }
    }

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

            Vector3 start = Actions.transform.position - Vector3.up;
            Vector3 end = point - Vector3.up;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(start, out hit, 1.0f, NavMesh.AllAreas))
            {
                start = hit.position;
            }

            if (NavMesh.SamplePosition(end, out hit, 1.0f, NavMesh.AllAreas))
            {
                end = hit.position;
            }

            if (NavMesh.CalculatePath(start, end, NavMesh.AllAreas, path))
            {
                lastQueriedPoint = point;
            }
        }

        Vector3 v;
        if (path.corners.Length > 1)
        {
            v = (path.corners[1] - Actions.transform.position);
        }
        else
        {
            v = (point - Actions.transform.position);
        }

        if (v.magnitude > goodEnoughDistance * goodEnoughDistance)
        {
            Actions.MoveDirection = v.normalized;
            Actions.FaceDirection = v.normalized;
        }
        else
        {
            Actions.MoveDirection = Vector3.zero;
        }
        return EvaluationResult.Success;
    }
}
