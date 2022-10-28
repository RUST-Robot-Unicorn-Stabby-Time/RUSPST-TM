using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteampunkCogs : Artifact
{
    [Space]
    public float speedScaling;
    public float accelerationScaling;

    protected override void Awake()
    {
        base.Awake();

        modifications.Add(new StatModification("speed", v => v * speedScaling));
        modifications.Add(new StatModification("acceleration", v => v * accelerationScaling));
    }
}
