using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : Artifact
{
    public float multiplier;

    protected override void Awake()
    {
        base.Awake();

        modifications.Add(new StatModification("knockback", v => v * multiplier));
    }
}
