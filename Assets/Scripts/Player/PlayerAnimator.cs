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

    Statboard statboard;
    CharacterMovement movement;

    Quaternion rootRotation = Quaternion.identity;

    private void Awake()
    {
        statboard = GetComponent<Statboard>();
        movement = GetComponent<CharacterMovement>();
    }

    private void LateUpdate()
    {
        UpdatePlayerModel();
    }

    private void UpdatePlayerModel()
    {
        Vector3 planarVelocity = movement.LocalVelocity;
        planarVelocity -= transform.up * Vector3.Dot(transform.up, planarVelocity);

        float speed = planarVelocity.magnitude;

        target.SetFloat("speed", speed);
        target.SetBool("falling", !movement.IsGrounded);
        target.SetFloat("speedMulti", Mathf.Max(movement.moveSpeedStat.GetFor(this), speed));

        if (planarVelocity.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(planarVelocity, Vector3.up);
            float angleDelta = Quaternion.Angle(targetRotation, rootRotation);
            rootRotation = Quaternion.RotateTowards(rootRotation, targetRotation, angleDelta * rotationSpeed * speed * Time.deltaTime);
        }
        root.rotation = Quaternion.Inverse(transform.rotation) * rootRotation * root.rotation;

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
