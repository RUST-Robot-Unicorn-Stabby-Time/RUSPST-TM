using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class JerrySlayerOfMen : EnemyBase
{
    public float minWaitDistance;
    public float maxWaitDistance;

    [Space]
    public float attackDuration;
    public float attackDistance;
    public UnityEvent attackEvent;

    public override void Behave()
    {
        MovementDirection = Vector3.zero;
        Facing = null;

        if (Target)
        {
            if (Attacking)
            {
                Vector3 direction = (Target.transform.position - transform.position).normalized;
                MovementDirection = direction;
                Facing = direction;
                return;
            }

            float distance = (Target.transform.position - transform.position).magnitude;

            if (WantsToAttack)
            {
                if (distance > maxWaitDistance) WantsToAttack = false;

                MovementDirection = Vector3.zero;
                Facing = Target.transform.position - transform.position;
            }
            else
            {
                if (distance < minWaitDistance) WantsToAttack = true;

                PathfindToPoint(Target.transform.position);
                Facing = Target.transform.position - transform.position;
            }
        }
    }

    public override void Attack()
    {
        if (!isActiveAndEnabled) return;

        base.Attack();

        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        if (Attacking) yield break;

        Attacking = true;
        WantsToAttack = false;

        Vector3 point = Target.transform.position;

        while ((point - transform.position).sqrMagnitude > attackDistance * attackDistance)
        {
            MovementDirection = (point - transform.position).normalized;
            yield return null;
        }

        attackEvent?.Invoke();

        yield return new WaitForSeconds(attackDuration);

        Attacking = false;
    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minWaitDistance);
        Gizmos.DrawWireSphere(transform.position, maxWaitDistance);

        base.OnDrawGizmosSelected();
    }
}
