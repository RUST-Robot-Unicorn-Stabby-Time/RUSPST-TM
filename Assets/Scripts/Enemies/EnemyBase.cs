using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[SelectionBase]
[DisallowMultipleComponent]
public abstract class EnemyBase : MonoBehaviour
{
    [Header("PATHFINDING")]
    public float jumpLookahead;
    public float jumpVerticalOffset;
    public LayerMask lookaheadMask;

    [Space]
    public float goodEnoughDistance;

    [Space]
    public float pathfindInterval;
    public LayerMask enviromentMask;

    NavMeshPath path;
    int pathIndex;
    float pathfindTimer;

    GameObject _target;
    EnemyHivemind hivemind;

    public bool Attacking { get; protected set; }
    public bool AllowedToAttack { get; set; }
    public bool ReadyToAttack { get; set; }

    public GameObject Target 
    { 
        get
        {
            if (!_target)
            {
                _target = FindObjectOfType<InputArbiter>().gameObject;
            }
            return _target;
        }
        set => _target = value;
    }
    public CharacterMovement Movement { get; private set; }
    
    protected virtual void Awake()
    {
        Movement = GetComponent<CharacterMovement>();

        path = new NavMeshPath();
    }

    private void OnEnable()
    {
        hivemind = GetComponentInParent<EnemyHivemind>();
        if (hivemind)
        {
            hivemind.Register(this);
        }
    }

    private void OnDisable()
    {
        if (hivemind)
        {
            hivemind.Deregister(this);
        }
    }

    private void Update()
    {
        Behave();
    }

    public abstract void Behave();

    protected void PathfindToPoint(Vector3 endPoint)
    {
        Vector3 startPoint = GetPointOnGround(transform.position);
        endPoint = GetPointOnGround(endPoint);

        if (pathfindTimer > pathfindInterval)
        {
            NavMesh.CalculatePath(startPoint, endPoint, NavMesh.AllAreas, path);
            pathIndex = 0;
            pathfindTimer -= pathfindInterval;
        }
        pathfindTimer += Time.deltaTime;

        if (path.status != NavMeshPathStatus.PathInvalid && pathIndex < path.corners.Length)
        {
            endPoint = path.corners[pathIndex];
            if ((endPoint - startPoint).magnitude < goodEnoughDistance)
            {
                pathIndex++;
            }
        }
        else
        {
            Movement.MovementDirection = Vector3.zero;
            return;
        }

        Vector3 vectorTo = (endPoint - startPoint);

        float distance = vectorTo.magnitude;
        Vector3 direction = vectorTo / distance;

        Movement.MovementDirection = direction;
        if (distance < goodEnoughDistance)
        {
            Movement.MovementDirection = Vector3.zero;
        }

        Movement.Jump = false;
        if (Physics.Raycast(transform.position + Vector3.up * jumpVerticalOffset, Movement.MovementDirection, jumpLookahead, lookaheadMask))
        {
            Movement.Jump = true;
        }
    }

    private Vector3 GetPointOnGround(Vector3 targetPoint)
    {
        Ray ray = new Ray(targetPoint + Vector3.up * 0.1f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, enviromentMask))
        {
            Debug.DrawLine(targetPoint, hit.point, Color.red);
            return hit.point;
        }

        return targetPoint;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (path?.corners.Length > 0)
        {
            Gizmos.DrawSphere(path.corners[0], 0.1f);
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                Gizmos.DrawLine(path.corners[i], path.corners[i + 1]);
                Gizmos.DrawSphere(path.corners[i + 1], 0.1f);
            }
        }

        Gizmos.color = Color.white;
    }
}
