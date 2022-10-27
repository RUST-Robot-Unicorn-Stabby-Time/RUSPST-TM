using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnController : MonoBehaviour
{
    PlayerAnimator playerAnimator;

    public LockOnTarget Target { get; private set; }

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        if (Target)
        {
            if (!Target.isActiveAndEnabled)
            {
                Target = null;
                ToggleTarget();
            }
            else
            {
                playerAnimator.DirectionLock = (Target.transform.position - transform.position).normalized;
            }
        }
        else
        {
            playerAnimator.DirectionLock = null;
        }
    }

    public void ToggleTarget()
    {
        if (Target)
        {
            Target.SetTargeted(false);
            Target = null;
        }
        else
        {
            LockOnTarget best = null;
            foreach (var target in LockOnTarget.Targets)
            {
                if (!best)
                {
                    best = target;
                    continue;
                }

                Vector3 tVector = target.transform.position - transform.position;
                Vector3 bVector = best.transform.position - transform.position;

                float tDot = Vector3.Dot(tVector.normalized, transform.forward);
                float bDot = Vector3.Dot(bVector.normalized, transform.forward);

                if (tDot > bDot)
                {
                    best = target;
                }
            }

            if (best)
            {
                best.SetTargeted(true);
                Target = best;
            }
        }
    }

    public void SwitchTarget(int direction)
    {
        if (!Target) return;

        direction = direction > 0.0f ? 1 : -1;

        LockOnTarget best = null;
        Vector3 cVector = Target.transform.position - transform.position;

        foreach (var target in LockOnTarget.Targets)
        {
            if (target == Target) continue;

            Vector3 tVector = target.transform.position - transform.position;
            float tDeltaAngle = Vector3.SignedAngle(cVector.normalized, tVector.normalized, Vector3.up);

            if (tDeltaAngle * direction < 0.0f) continue;

            if (!best)
            {
                best = target;
                continue;
            }

            Vector3 bVector = best.transform.position - transform.position;
            float bDeltaAngle = Vector3.SignedAngle(cVector.normalized, bVector.normalized, Vector3.up);

            if (Mathf.Abs(tDeltaAngle) < Mathf.Abs(bDeltaAngle))
            {
                best = target;
            }
        }

        if (best)
        {
            best.SetTargeted(true);
            Target.SetTargeted(false);
            Target = best;
            Target.SetTargeted(true);
        }
    }
}
