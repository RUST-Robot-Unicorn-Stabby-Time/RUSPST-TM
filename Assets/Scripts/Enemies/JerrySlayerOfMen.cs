using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class JerrySlayerOfMen : EnemyBase
{
    public float waitDistance;

    [Space]
    public float attackDuration;
    public UnityEvent attackEvent;

    public override void Behave()
    {
        if (Target)
        {
            if (TargetPosition.HasValue) PathfindToPoint(TargetPosition.Value);
        }
        else
        {
            Movement.MovementDirection = Vector3.zero;
        }
    }

    private IEnumerator Attack()
    {
        Attacking = true;
        attackEvent?.Invoke();

        yield return new WaitForSeconds(attackDuration);

        Attacking = false;
    }
}
