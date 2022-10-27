using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardwareOverclock : Artifact
{
    [Space]
    public AnimationCurve damageScaling;

    Health health;

    protected override void Awake()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;

        base.Awake();

        health = GetComponentInParent<Health>();

        modifications.Add(new StatModification("damage", v => v * damageScaling.Evaluate(health.currentHealth)));
    }
}
