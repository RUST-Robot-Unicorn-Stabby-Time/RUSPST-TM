using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearBattery : Artifact
{
    [Space]
    public float rageGain;

    Rage rage;
    Health health;

    protected override void Awake()
    {
        base.Awake();

        rage = GetComponentInParent<Rage>();
        health = GetComponentInParent<Health>();

        modifications.Add(new StatModification("rageGain", v => v * rageGain));
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

    private void OnDamage(DamageArgs damageArgs)
    {
        rage.ragePercent = 0.0f;
    }
}
