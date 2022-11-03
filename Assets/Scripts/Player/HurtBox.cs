using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class HurtBox : MonoBehaviour
{
    public Stat damage;
    public Stat knockback;
    public Vector3 knockbackDirection;
    public Bounds damageBounds;
    public LayerMask collisionMask;
    public LayerMask rootMask;

    [Space]
    public GameObject hitFXPrefab;

    HashSet<Collider> hitObjects = new HashSet<Collider>();
    HashSet<Health> hurtObjects = new HashSet<Health>();

    public event System.Action<GameObject, DamageArgs> HitEvent;

    private void OnEnable()
    {
        hitObjects.Clear();
        hurtObjects.Clear();
    }

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + transform.rotation * damageBounds.center, damageBounds.extents / 2, transform.rotation, collisionMask);

        foreach (Collider collider in colliders)
        {
            if (collider.transform.root == transform.root) continue;
            if (rootMask != (rootMask | (1 << collider.transform.root.gameObject.layer))) continue;
            if (hitObjects.Contains(collider)) continue;

            if (collider.TryGetComponentInParent(out Health health))
            {
                if (hurtObjects.Contains(health)) continue;
                hurtObjects.Add(health);

                DamageArgs args = new DamageArgs(transform.root.gameObject, damage.GetFor(this));
                health.Damage(args);
                HitEvent?.Invoke(collider.gameObject, args);
            }

            if (collider.attachedRigidbody)
            {
                collider.attachedRigidbody.velocity += transform.TransformDirection(knockbackDirection).normalized * knockback.GetFor(this);
            }

            Vector3 direction = (collider.transform.position - transform.position).normalized;
            if (hitFXPrefab) Instantiate(hitFXPrefab, collider.ClosestPoint(transform.position), Quaternion.Euler(direction));

            hitObjects.Add(collider);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = isActiveAndEnabled ? Color.red : Color.yellow;
        Gizmos.DrawWireCube(damageBounds.center, damageBounds.size);

        if (isActiveAndEnabled)
        {
            Gizmos.color *= new Color(1.0f, 1.0f, 1.0f, 0.1f);
            Gizmos.DrawCube(damageBounds.center, damageBounds.size);
        }

        Gizmos.color = Color.white;
    }
}
