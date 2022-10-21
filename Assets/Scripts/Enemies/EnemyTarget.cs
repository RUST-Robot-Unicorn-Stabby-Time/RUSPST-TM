using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[DisallowMultipleComponent]
public class EnemyTarget : MonoBehaviour
{
    public int concurrentAttackers;
    public float delay;

    public static HashSet<EnemyTarget> Targets { get; private set; } = new HashSet<EnemyTarget>();

    private HashSet<EnemyBase> attackers = new HashSet<EnemyBase>();

    public void OnEnable ()
    {
        Targets.Add(this);
    }

    public void OnDisable ()
    {
        Targets.Remove(this);
    }

    public void RegisterAttacker (EnemyBase attacker)
    {
        attackers.Add(attacker);
    }

    public void DeregisterAttacker(EnemyBase attacker)
    {
        attackers.Remove(attacker);
    }

    public void Update ()
    {
        List<EnemyBase> validAttackers = new List<EnemyBase>();
        int currentAttackers = 0;
        foreach (var attacker in attackers)
        {
            if (attacker.WantsToAttack) validAttackers.Add(attacker);
            if (attacker.Attacking) currentAttackers++;
        }

        validAttackers.Sort((a, b) => Util.Sign(a.LastAttackTime - b.LastAttackTime));

        for (int i = 0; i < concurrentAttackers - currentAttackers && i < validAttackers.Count; i++)
        {
            validAttackers[i].Attack();
        }
    }
}
