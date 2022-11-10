using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificationStation : Artifact
{
    [Space]
    public float regenAmount;
    public float threshold;
    public float delay;
    public float damageIncrease;

    Health health;
    CharacterMovement movement;
    float lastMoveTime;

    protected override void Awake()
    {
        base.Awake();

        health = GetComponentInParent<Health>();
        movement = GetComponentInParent<CharacterMovement>();

        modifications.Add(new StatModification("maxHealth", v => v / (StandingStill() ? damageIncrease : 1.0f)));
    }

    private void Update()
    {
        if (StandingStill())
        {
            if (Time.time > lastMoveTime + delay)
            {
                health.currentHealth = Mathf.Min(health.currentHealth + regenAmount * Time.deltaTime, 1.0f);
            }
        }
        else
        {
            lastMoveTime = Time.time;
        }
    }

    private bool StandingStill()
    {
        return movement.GetComponent<Rigidbody>().velocity.magnitude < threshold;
    }
}
