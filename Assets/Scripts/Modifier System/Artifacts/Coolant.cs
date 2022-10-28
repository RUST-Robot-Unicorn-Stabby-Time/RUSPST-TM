using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coolant : Artifact
{
    [Space]
    public float maxRageScaling;

    Rage rage;

    protected override void Awake()
    {
        base.Awake();

        rage = GetComponentInParent<Rage>();

        modifications.Add(new StatModification("maxRage", v => v * maxRageScaling));
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        rage.canGainRageInRage = false;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        rage.canGainRageInRage = true;
    }
}
