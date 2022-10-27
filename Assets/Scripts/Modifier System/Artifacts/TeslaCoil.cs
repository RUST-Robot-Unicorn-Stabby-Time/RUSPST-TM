using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaCoil : Artifact
{
    [Space]
    [Range(0.0f, 1.0f)]public float thorns;

    Health health;

    protected override void Awake()
    {
        base.Awake();

        health = GetComponentInParent<Health>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        health.DamageEvent += OnDamage;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        health.DamageEvent -= OnDamage;
    }

    private void OnDamage(DamageArgs args)
    {
        if (!args.damager) return;
        
        if (args.damager.TryGetComponent(out Health health))
        {
            health.Damage(new DamageArgs(transform.root.gameObject, args.damage * thorns));
        }
    }
}
