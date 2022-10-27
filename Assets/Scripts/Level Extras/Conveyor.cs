using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody))]
public class Conveyor : MonoBehaviour
{
    [SerializeField] Vector3 force;

    new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 pos = rigidbody.position;
        rigidbody.position -= transform.TransformDirection(force) * Time.deltaTime;
        rigidbody.MovePosition(pos);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = transform.localToWorldMatrix;

        Vector3 dir = force.normalized;
        Vector3 offset = Vector3.up;
        Vector3 tangent = Vector3.Cross(dir, Vector3.up);

        Gizmos.DrawLine(offset + dir, offset - dir);
        Gizmos.DrawLine(offset + dir, offset + dir * 0.9f + Vector3.up * 0.1f);
        Gizmos.DrawLine(offset + dir, offset + dir * 0.9f + Vector3.up * -0.1f);
        Gizmos.DrawLine(offset + dir, offset + dir * 0.9f + tangent * 0.1f);
        Gizmos.DrawLine(offset + dir, offset + dir * 0.9f + tangent * -0.1f);

        Gizmos.color = Color.white;
    }
}
