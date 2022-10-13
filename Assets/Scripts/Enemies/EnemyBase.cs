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
    public Vector3 TargetPosition { get; set; }

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

    protected void PathfindToPoint(Vector3 targetPoint)
    {
        if (pathfindTimer > pathfindInterval)
        {
            NavMesh.CalculatePath(transform.position, targetPoint, NavMesh.AllAreas, path);
            pathIndex = 0;
            pathfindTimer -= pathfindInterval;
        }
        pathfindTimer += Time.deltaTime;

        if (path.status != NavMeshPathStatus.PathInvalid && pathIndex < path.corners.Length)
        {
            targetPoint = path.corners[pathIndex];
            if ((targetPoint - transform.position).magnitude < goodEnoughDistance)
            {
                pathIndex++;
            }
        }

        Vector3 vectorTo = (targetPoint - transform.position);

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
}
