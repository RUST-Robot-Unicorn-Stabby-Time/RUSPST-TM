using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiofuelReactor : Artifact
{
    [Space]
    public float damagePerTick;
    public float damageFrequency;
    [Range(0.0f, 1.0f)]public float lifesteal;

    Health health;

    float tickTime;

    protected override void Awake()
    {
        base.Awake();

        health = GetComponentInParent<Health>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        tickTime = 0.0f;

        foreach (var hurtbox in transform.root.GetComponentsInChildren<HurtBox>(true))
        {
            hurtbox.HitEvent += OnHit;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        foreach (var hurtbox in transform.root.GetComponentsInChildren<HurtBox>(true))
        {
            hurtbox.HitEvent -= OnHit;
        }
    }

    private void Update()
    {
        if (tickTime > + 1.0f / damageFrequency)
        {
            health.currentHealth -= damagePerTick / health.maxHealth.GetFor(this);

            tickTime -= 1.0f / damageFrequency;
        }

        tickTime += Time.deltaTime;
    }

    private void OnHit(GameObject hitObject, DamageArgs damageArgs)
    {
        float heal = damageArgs.damage * lifesteal;
        health.currentHealth = Mathf.Min(health.currentHealth + heal / health.maxHealth.GetFor(this), 1.0f);
    }
}
