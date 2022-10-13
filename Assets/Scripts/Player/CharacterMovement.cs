using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Stat moveSpeedStat;
    public Stat accelerationStat;

    [Space]
    public float jumpHeight;
    public float jumpGravity;
    public float fallingGravity;

    [Space]
    public float groundCheckOffset;
    public float groundCheckRadius;
    public LayerMask groundCheckMask;

    [Space]
    public float groundMaxSlope;
    public float groundLookaheadTime;
    public float groundStickiness;
    
    new Rigidbody rigidbody;

    public bool IsGrounded { get; private set; }
    public float JumpForce => Mathf.Sqrt(2.0f * -Physics.gravity.y * jumpGravity * jumpHeight);
    public Vector3 LocalVelocity { get; private set; }

    public Vector3 MovementDirection { get; set; }
    public bool Jump { get; set; }

    public bool PauseMovement { get; set; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void JumpCharacter()
    {
        if (Jump)
        {
            if (IsGrounded)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, JumpForce, rigidbody.velocity.z);
            }
        }
    }

    private void FixedUpdate()
    {
        IsGrounded = GetIsGrounded();

        MoveCharacter();
        JumpCharacter();

        if (IsGrounded && rigidbody.velocity.y < JumpForce * 0.5f)
        {
            StickToGround();
        }
        else
        {
            float gravityScale = fallingGravity;
            if (rigidbody.velocity.y > 0.0f) gravityScale = jumpGravity;

            rigidbody.velocity += Physics.gravity * gravityScale * Time.deltaTime;
        }
        rigidbody.useGravity = false;
    }

    private void StickToGround()
    {
        float moveSpeed = moveSpeedStat.GetFor(this);
        Ray ray = new Ray(transform.position + Vector3.up * groundCheckOffset, Vector3.down + MovementDirection * groundLookaheadTime);
        Vector3 direction = Vector3.down;

        if (Physics.Raycast(ray, out RaycastHit hit, groundCheckRadius))
        {
            float angle = Mathf.Acos(Vector3.Dot(hit.normal, Vector3.up)) * Mathf.Rad2Deg;
            if (angle < groundMaxSlope)
            {
                direction = -hit.normal;
                Debug.DrawLine(ray.origin, ray.GetPoint(hit.distance), Color.green);
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.GetPoint(hit.distance), Color.yellow);
            }
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.GetPoint(10.0f), Color.red);
        }

         rigidbody.velocity += direction * groundStickiness * Time.deltaTime;
    }

    private bool GetIsGrounded()
    {
        Collider[] queryList = new Collider[2];
        Physics.OverlapSphereNonAlloc(transform.position + Vector3.up * groundCheckOffset, groundCheckRadius, queryList, groundCheckMask);
        foreach (var query in queryList)
        {
            if (query)
            {
                if (query.transform.root != transform.root)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = GetIsGrounded() ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position + Vector3.up * groundCheckOffset, groundCheckRadius);
        Gizmos.color = Color.white;
    }

    private void MoveCharacter()
    {
        Vector3 referenceFrame = GetGroundVelocity();

        float moveSpeed = moveSpeedStat.GetFor(this);
        float acceleration = accelerationStat.GetFor(this);

        Vector3 target = MovementDirection * moveSpeed + referenceFrame;
        
        if (PauseMovement)
        {
            target = Vector3.zero;
        }

        Vector3 current = rigidbody.velocity;

        Vector3 difference = target - current;
        difference.y = 0.0f;

        Vector3 force = Vector3.ClampMagnitude(difference, moveSpeed) * acceleration;
        rigidbody.velocity += force * Time.deltaTime;

        LocalVelocity = rigidbody.velocity - referenceFrame;
    }

    private Vector3 GetGroundVelocity()
    {
        Ray ray = new Ray(transform.position + Vector3.up * groundCheckOffset, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, groundCheckRadius))
        {
            if (hit.rigidbody)
            {
                return hit.rigidbody.velocity;
            }
        }

        return Vector3.zero;
    }
}
