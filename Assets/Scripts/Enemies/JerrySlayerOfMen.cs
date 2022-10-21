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
    public UnityEvent attackEvent;

    public override void Behave()
    {
        if (Target)
        {
            if (Attacking) return;

            float distance = (Target.transform.position - transform.position).magnitude;

            if (WantsToAttack)
            {
                if (distance > maxWaitDistance) WantsToAttack = false;
            }
            else
            {
                PathfindToPoint(Target.transform.position);
            }
        }
        else
        {
            Movement.MovementDirection = Vector3.zero;
        }
    }

    public override void Attack()
    {
        base.Attack();

        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        Attacking = true;
        attackEvent?.Invoke();

        yield return new WaitForSeconds(attackDuration);

        Attacking = false;
    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minWaitDistance);

        base.OnDrawGizmosSelected();
    }
}
