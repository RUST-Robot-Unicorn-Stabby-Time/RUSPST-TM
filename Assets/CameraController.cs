using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity;
    public Transform cameraTarget;

    Vector2 ssRotation;

    private void Update()
    {
        ApplyMouseMovement();

        TransformTarget();
    }

    private void TransformTarget()
    {
        cameraTarget.transform.rotation = Quaternion.Euler(0.0f, ssRotation.x, 0.0f);
    }

    private void ApplyMouseMovement()
    {
        ssRotation += Mouse.current.delta.ReadValue() * mouseSensitivity;
    }
}
