using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float damage;
    public float muzzleSpeed;
    public float size;
    public LayerMask collisionMask;
    public LayerMask rootMask;

    [Space]
    public GameObject hitPrefab;

    new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * muzzleSpeed;
    }

    private void FixedUpdate()
    {
        float speed = rigidbody.velocity.magnitude;
        Ray ray = new Ray(rigidbody.position, rigidbody.velocity.normalized);

        if (Physics.SphereCast(ray, size, out var hit, speed * Time.deltaTime + 0.1f, collisionMask))
        {
            ProcessHit(hit);

            if (hitPrefab) Instantiate(hitPrefab, hit.point, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    private void ProcessHit(RaycastHit hit)
    {
        if (hit.transform.root == transform.root) return;
        if (rootMask != (rootMask | (1 << hit.transform.root.gameObject.layer))) return;

        if (hit.transform.TryGetComponentInParent(out Health health))
        {
            health.Damage(new DamageArgs(null, damage));
        }
    }
}
