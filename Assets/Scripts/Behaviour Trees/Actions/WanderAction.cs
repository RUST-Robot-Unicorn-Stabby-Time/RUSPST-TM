using UnityEngine;

public class WanderAction : BehaviourBase
{
    [SerializeField] Vector3 wanderCenter;
    [SerializeField] float wanderRadius;

    [Space]
    [SerializeField] float wanderSpeed;
    [SerializeField] Vector2 wanderDelay;

    [Space]
    [SerializeField] float agentSize;
    [SerializeField] float wallDetectionRadius;

    Vector3 wanderPoint;
    Phase phase;
    float waitTimer;
    float waitTime;

    private void OnEnable()
    {
        SetRandomWanderPoint();
    }

    protected override EvaluationResult OnExecute()
    {
        if (Tree.TryGetComponent(out EnemyActions actions))
        {
            switch (phase)
            {
                case Phase.Wander:
                    Vector3 direction = (wanderPoint - actions.transform.position);
                    direction.y = 0.0f;
                    direction.Normalize();

                    actions.MoveDirection = direction * wanderSpeed;
                    actions.FaceDirection = direction;

                    if ((wanderPoint - actions.transform.position).sqrMagnitude < 1.0f)
                    {
                        waitTimer = 0.0f;
                        waitTime = Random.Range(wanderDelay.x, wanderDelay.y);
                        phase = Phase.Wait;
                    }
                    break;

                case Phase.Wait:
                default:
                    actions.MoveDirection = Vector2.zero;
                    if (waitTimer > waitTime)
                    {
                        phase = Phase.Wander;
                        SetRandomWanderPoint();
                    }
                    waitTimer += Time.deltaTime;
                    break;
            }
        }

        return EvaluationResult.Success;
    }

    private void SetRandomWanderPoint()
    {
        float angle = Random.value * Mathf.PI * 2.0f;
        wanderPoint = wanderCenter + new Vector3(Mathf.Cos(angle), 0.0f, Mathf.Sin(angle)) * Random.value * wanderRadius;

        Vector3 vector = (wanderPoint - transform.position);
        vector.y = 0.0f;
        Ray ray = new Ray(transform.position, vector);
        if (Physics.SphereCast(ray, agentSize, out var hit, vector.magnitude))
        {
            wanderPoint = ray.GetPoint(hit.distance - wallDetectionRadius);
        }
    }

    protected override void Reset()
    {
        base.Reset();

        wanderCenter = transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(wanderCenter, wanderRadius);
    }

    public enum Phase
    {
        Wander,
        Wait
    }
}
