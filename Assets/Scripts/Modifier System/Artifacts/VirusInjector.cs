using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusInjector : Artifact
{
    [Space]
    public float defaultDamageScale;
    public float critDamageScale;
    [Range(0.0f, 1.0f)]public float critChance;

    StatModification modification;
    float damageScale;

    protected override void Awake()
    {
        base.Awake();

        modifications.Add(new StatModification("damage", v => v * damageScale));
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        foreach (var weapons in GetComponentsInParent<PlayerWeapon>())
        {
            weapons.BeginAttackEvent += OnBeginAttack;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        foreach (var weapons in GetComponentsInParent<PlayerWeapon>())
        {
            weapons.BeginAttackEvent -= OnBeginAttack;
        }
    }

    private void OnBeginAttack()
    {
        damageScale = Random.value < critChance ? critDamageScale : defaultDamageScale;
    }
}
