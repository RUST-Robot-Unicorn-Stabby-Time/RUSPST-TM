using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity;
    public Transform cameraTarget;
    public Vector2 verticalRotationClamp;

    Vector2 ssRotation;

    private void FixedUpdate()
    {
        ApplyMouseMovement();

        TransformTarget();
    }

    private void TransformTarget()
    {
        ssRotation.y = Mathf.Clamp(ssRotation.y, verticalRotationClamp.x, verticalRotationClamp.y);
        transform.rotation = Quaternion.Euler(0.0f, ssRotation.x, 0.0f);
        cameraTarget.transform.rotation = Quaternion.Euler(-ssRotation.y, ssRotation.x, 0.0f);
    }

    private void ApplyMouseMovement()
    {
        ssRotation += Mouse.current.delta.ReadValue() * mouseSensitivity;
    }
}