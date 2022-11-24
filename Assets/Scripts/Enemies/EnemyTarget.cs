using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[DisallowMultipleComponent]
public class EnemyTarget : MonoBehaviour
{
    public float delayBetweenAttackers;

    float lastAttackTime;

    public static HashSet<EnemyTarget> Targets { get; private set; } = new HashSet<EnemyTarget>();

    private HashSet<EnemyBase> attackers = new HashSet<EnemyBase>();

    public void OnEnable()
    {
        Targets.Add(this);
    }

    public void OnDisable()
    {
        Targets.Remove(this);
    }

    public void RegisterAttacker(EnemyBase attacker)
    {
        attackers.Add(attacker);
    }

    public void DeregisterAttacker(EnemyBase attacker)
    {
        attackers.Remove(attacker);
    }

    public void Update()
    {
        if (Time.time < lastAttackTime + delayBetweenAttackers) return;

        EnemyBase bestAttacker = null;
        foreach (var attacker in attackers)
        {
            if (attacker.WantsToAttack && !attacker.Attacking)
            {
                if (!bestAttacker)
                {
                    bestAttacker = attacker;
                }
                else if (attacker.LastAttackTime > bestAttacker.LastAttackTime)
                {
                    bestAttacker = attacker;
                }
            }
        }

        if (bestAttacker)
        {
            bestAttacker.Attack();
            lastAttackTime = Time.time;
        }
    }
}
