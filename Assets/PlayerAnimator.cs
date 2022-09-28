using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator target;

    [Space]
    public Transform root;
    public float rotationSpeed;
    public float moveTilt;
    public float turningTilt;

    Statboard statboard;
    new Rigidbody rigidbody;
    CharacterMovement movement;

    Quaternion rootRotation = Quaternion.identity;

    private void Awake()
    {
        statboard = GetComponent<Statboard>();
        rigidbody = GetComponent<Rigidbody>();
        movement = GetComponent<CharacterMovement>();
    }

    private void LateUpdate()
    {
        Vector3 planarVelocity = rigidbody.velocity;
        planarVelocity -= transform.up * Vector3.Dot(transform.up, planarVelocity);

        float speed = planarVelocity.magnitude;
        if (statboard.TryGetStat("speed", out float moveSpeed))
        {
            speed /= moveSpeed;
        }

        target.SetFloat("speed", speed);
        target.SetBool("falling", !movement.IsGrounded);

        Quaternion targetRotation = Quaternion.LookRotation(planarVelocity, Vector3.up);
        float angleDelta = Quaternion.Angle(targetRotation, rootRotation);
        rootRotation = Quaternion.RotateTowards(rootRotation, targetRotation, angleDelta * rotationSpeed * speed * Time.deltaTime);
        root.rotation = Quaternion.Inverse(transform.rotation) * rootRotation * root.rotation;

        float fSpeed = Vector3.Dot(root.forward, planarVelocity);
        float rSpeed = Vector3.Dot(root.right, planarVelocity);
        Vector3 tilt = new Vector3(fSpeed * moveTilt, 0.0f, -rSpeed * turningTilt);
        root.rotation *= Quaternion.Euler(tilt);
    }
}
