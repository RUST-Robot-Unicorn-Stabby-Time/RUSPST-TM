using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalVentilator : Artifact
{
    [Space]
    public AnimationCurve damageScaling;

    float timeSinceLastAttack;

    protected override void OnEnable()
    {
        base.OnEnable();

        foreach (var hurtbox in transform.root.GetComponentsInChildren<HurtBox>(true))
        {
            hurtbox.HitEvent += OnHit;
        }

        modifications.Add(new StatModification("damage", v => v * damageScaling.Evaluate(Time.time - timeSinceLastAttack)));
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        foreach (var hurtbox in transform.root.GetComponentsInChildren<HurtBox>(true))
        {
            hurtbox.HitEvent -= OnHit;
        }
    }

    private void OnHit(GameObject damagedObject, DamageArgs damageArgs)
    {
        timeSinceLastAttack = Time.time;
    }
}
