using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed;
    public float acceleration;

    [Space]
    public float jumpForce;

    [Space]
    public float groundCheckOffset;
    public float groundCheckRadius;
    public LayerMask groundCheckMask;

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
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpForce, rigidbody.velocity.z);
            }
        }
    }

    private void FixedUpdate()
    {
        IsGrounded = GetIsGrounded();

        MoveCharacter();
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

    private void MoveCharacter()
    {
        Vector3 target = controller.MovementDirection * moveSpeed;
        Vector3 current = rigidbody.velocity;

        Vector3 difference = target - current;
        difference.y = 0.0f;

        Vector3 force = Vector3.ClampMagnitude(difference, moveSpeed) * acceleration;
        rigidbody.velocity += force * Time.deltaTime;

        print(target);
    }
}
