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

    [Space]
    public float agroRange;

    NavMeshPath path;
    PlayerAnimator playerAnimator;
    int pathIndex;
    float pathfindTimer;

    EnemyTarget _target;

    public bool WantsToAttack { get; protected set; }
    public bool Attacking { get; protected set; }
    public float LastAttackTime { get; private set; }
    public Vector3 MovementDirection
    {
        get => Movement.MovementDirection;
        set
        {
            Movement.MovementDirection = value;
            if (value.magnitude > 0.001f) transform.forward = new Vector3(value.x, 0.0f, value.z);
        }
    }
    public Vector3? Facing
    {
        get => playerAnimator.DirectionLock;
        set => playerAnimator.DirectionLock = value.HasValue ? new Vector3(value.Value.x, 0.0f, value.Value.z).normalized : (Vector3?)null;
    }

    public EnemyTarget Target 
    {
        get => _target;
        set
        {
            if (_target) _target.DeregisterAttacker(this);
            _target = value;
            if (_target) _target.RegisterAttacker(this);
        }
    }
    public CharacterMovement Movement { get; private set; }

    public static HashSet<EnemyBase> AliveEnemies { get; } = new HashSet<EnemyBase>();
    public static event System.Action AllEnemiesDeadEvent;
    public static event System.Action<EnemyBase> EnemyDiedEvent;

    protected virtual void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        Movement = GetComponent<CharacterMovement>();

        path = new NavMeshPath();
    }

    private void OnEnable()
    {
        AliveEnemies.Add(this);
    }

    private void OnDisable()
    {
        if (Target)
        {
            Target.DeregisterAttacker(this);
        }

        AliveEnemies.Remove(this);
        EnemyDiedEvent?.Invoke(this);
        if (AliveEnemies.Count == 0) AllEnemiesDeadEvent?.Invoke();

        Attacking = false;
    }

    private void Update()
    {
        if (!Target)
        {
            foreach (var target in EnemyTarget.Targets)
            {
                if ((target.transform.position - transform.position).sqrMagnitude < agroRange * agroRange)
                {
                    Target = target;
                    break;
                }
            }
        }

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

        Vector3 vectorTo = (endPoint - startPoint);

        float distance = vectorTo.magnitude;
        Vector3 direction = vectorTo / distance;

        MovementDirection = direction;
        if (distance < goodEnoughDistance)
        {
            MovementDirection = Vector3.zero;
        }

        Movement.Jump = false;
        if (Physics.Raycast(transform.position + Vector3.up * jumpVerticalOffset, MovementDirection, jumpLookahead, lookaheadMask))
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

    public virtual void Attack()
    {
        LastAttackTime = Time.time;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agroRange);

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
