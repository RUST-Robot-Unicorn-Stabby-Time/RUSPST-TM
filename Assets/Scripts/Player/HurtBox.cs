using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public Stat damage;
    public Bounds damageBounds;

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + damageBounds.center, damageBounds.extents / 2, transform.rotation);

        foreach (Collider collider in colliders)
        {
            if (collider.transform.root != transform.root)
            {
                if (collider.TryGetComponent(out Health health))
                {
                    health.Damage(new DamageArgs(transform.root.gameObject, damage.GetFor(this)));
                }
            }
        }
    }
}
