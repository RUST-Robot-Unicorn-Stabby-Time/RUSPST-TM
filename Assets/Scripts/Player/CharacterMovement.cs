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
    public float groundStickyness;
    
    IController controller;
    new Rigidbody rigidbody;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        controller = GetComponent<IController>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        controller.JumpEvent += OnJump;
    }

    private void OnDisable()
    {
        controller.JumpEvent -= OnJump;
    }

    private void OnJump(float inputValue)
    {
        if (inputValue > 0.5f)
        {
            if (IsGrounded)
            {
                float jumpForce = Mathf.Sqrt(2.0f * -Physics.gravity.y * jumpGravity * jumpHeight);
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpForce, rigidbody.velocity.z);
            }
        }
    }

    private void FixedUpdate()
    {
        IsGrounded = GetIsGrounded();

        MoveCharacter();

        float gravityScale = fallingGravity;
        if (rigidbody.velocity.y > 0.0f) gravityScale = jumpGravity;
        if (IsGrounded) gravityScale = groundStickyness;

        rigidbody.velocity += Physics.gravity * gravityScale * Time.deltaTime;
        rigidbody.useGravity = false;
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
        float moveSpeed = moveSpeedStat.GetFor(this);
        float acceleration = accelerationStat.GetFor(this);

        Vector3 target = controller.MovementDirection * moveSpeed;
        Vector3 current = rigidbody.velocity;

        Vector3 difference = target - current;
        difference.y = 0.0f;

        Vector3 force = Vector3.ClampMagnitude(difference, moveSpeed) * acceleration;
        rigidbody.velocity += force * Time.deltaTime;
    }
}
