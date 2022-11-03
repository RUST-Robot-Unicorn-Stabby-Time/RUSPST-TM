using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Philip : EnemyBase
{
    public Transform armBone;
    public Vector3 rotationOffset;
    public float shootTime;
    public float idleTime;
    public ProjectileWeapon weapon;
    public Vector3 aimOffset;

    float phaseTimer;

    public override void Behave()
    {
        if (Target)
        {
            float totalPhaseTime = shootTime + idleTime;
            float localPhaseTime = phaseTimer % totalPhaseTime;

            if (localPhaseTime > idleTime)
            {
                weapon.FireState = true;
            }
            else
            {
                weapon.FireState = false;
                Facing = (Target.transform.position + aimOffset - transform.position).normalized;
            }

            phaseTimer += Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (Target)
        {
            Quaternion rotation = Quaternion.LookRotation(Target.transform.position + aimOffset - armBone.transform.position);
            armBone.rotation = rotation * Quaternion.Euler(rotationOffset);
        }
    }
}
