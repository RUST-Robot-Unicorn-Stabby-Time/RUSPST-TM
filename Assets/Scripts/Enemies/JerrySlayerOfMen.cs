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
        if (Target)
        {
            if (Attacking)
            {
                MovementDirection = (Target.transform.position - transform.position).normalized;
                return;
            }

            float distance = (Target.transform.position - transform.position).magnitude;

            if (WantsToAttack)
            {
                if (distance > maxWaitDistance) WantsToAttack = false;

                MovementDirection = Vector3.zero;
            }
            else
            {
                if (distance < minWaitDistance) WantsToAttack = true;

                PathfindToPoint(Target.transform.position);
            }
        }
        else
        {
            MovementDirection = Vector3.zero;
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
