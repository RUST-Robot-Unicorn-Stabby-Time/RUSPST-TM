using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDamage : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float damage;
    [SerializeField] float force;
    [SerializeField] AnimationCurve falloff;
    [SerializeField] LayerMask collisionMask;
    [SerializeField] LayerMask rootMask;

    private void Start()
    {
        foreach (var query in Physics.OverlapSphere(transform.position, range, collisionMask))
        {
            if (query.transform.root == transform.root) continue;
            if (!query.attachedRigidbody) continue;
            if (rootMask != (rootMask | (1 << query.attachedRigidbody.gameObject.layer))) continue;

            float distance = (query.transform.position - transform.position).magnitude;
            if (distance > range) continue;

            float scale = falloff.Evaluate(range - distance / range);

            Health health = query.GetComponentInParent<Health>();
            if (health)
            {
                health.Damage(new DamageArgs(transform.root.gameObject, damage * scale, true));
            }

            if (query.attachedRigidbody)
            {
                query.attachedRigidbody.velocity += (query.transform.position - transform.position).normalized * force * scale / query.attachedRigidbody.mass;
            }
        }
    }
}
