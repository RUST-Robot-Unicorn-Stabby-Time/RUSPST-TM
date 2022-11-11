using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerAnimator : MonoBehaviour
{
    public Animator target;

    [Space]
    public Transform root;
    public float rotationSpeed;
    public float moveTiltMax;
    public float moveTiltSlope;
    public float turningTiltMax;
    public float turningTiltSlope;

    CharacterMovement movement;

    Quaternion targetRotation = Quaternion.identity;
    Quaternion rootRotation = Quaternion.identity;

    public Vector3? DirectionLock { get; set; }

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
    }

    private void OnEnable()
    {
        movement.JumpEvent += OnJump;
    }

    private void OnJump()
    {
        target.Play("Jump", 0, 0.0f);
    }

    private void OnDisable()
    {
        movement.JumpEvent -= OnJump;
    }

    private void LateUpdate()
    {
        UpdatePlayerModel();
    }

    private void UpdatePlayerModel()
    {
        Vector3 planarVelocity = movement.DrivingRigidbody.velocity - movement.LocalVelocity;
        planarVelocity -= transform.up * Vector3.Dot(transform.up, planarVelocity);

        float speed = planarVelocity.magnitude;

        target.SetFloat("speed", speed);
        target.SetBool("falling", !movement.IsGrounded);
        target.SetFloat("speedMulti", Mathf.Max(movement.MoveSpeed.GetFor(this), speed));

        if (planarVelocity.sqrMagnitude > 0.01f || DirectionLock.HasValue)
        {
            Quaternion targetRotation;
            if (DirectionLock.HasValue)
            {
                targetRotation = Quaternion.LookRotation(DirectionLock.Value, transform.up);
            }
            else
            {
                targetRotation = Quaternion.LookRotation(planarVelocity, Vector3.up);
            }

            float angleDelta = Quaternion.Angle(targetRotation, rootRotation);
            float rotationSpeed = angleDelta * this.rotationSpeed * (DirectionLock.HasValue ? 10.0f : speed);
            rootRotation = Quaternion.RotateTowards(rootRotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        root.rotation = rootRotation;

        float fSpeed = Vector3.Dot(root.forward, planarVelocity);
        float rSpeed = Vector3.Dot(root.right, planarVelocity);
        Vector3 tilt = new Vector3
        {
            x = TiltInterpolation(fSpeed, moveTiltMax, moveTiltSlope),
            y = 0.0f,
            z = TiltInterpolation(-rSpeed, turningTiltMax, turningTiltSlope),
        };
        root.rotation *= Quaternion.Euler(tilt);
    }

    public float TiltInterpolation(float x, float max, float slope)
    {
        return Mathf.Atan(x * slope) * max * (2.0f / Mathf.PI);
    }
}
