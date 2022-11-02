using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public Transform directionDriver;
    public float force;
    public LineRenderer lineRenderer;
    public LayerMask collisionMask;

    new Rigidbody rigidbody;

    Vector3? grappled;
    float targetDistance;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        lineRenderer.enabled = grappled.HasValue;

        if (!grappled.HasValue) return;

        Vector3 vector = grappled.Value - rigidbody.position;
        float distance = vector.magnitude;

        //if (distance > targetDistance)
        {
            float delta = distance - targetDistance;
            rigidbody.velocity += vector.normalized * force * Time.deltaTime / rigidbody.mass;
        }

        lineRenderer.SetPosition(0, lineRenderer.transform.position);
        lineRenderer.SetPosition(1, grappled.Value);
    }

    public void ToggleGrapple()
    {
        if (grappled.HasValue)
        {
            grappled = null;
        }
        else
        {
            Ray ray = new Ray(directionDriver.position, directionDriver.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, collisionMask))
            {
                grappled = hit.point;
                targetDistance = hit.distance;
            }
        }
    }
}
