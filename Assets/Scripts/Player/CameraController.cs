using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity;
    public Transform cameraTarget;
    public Vector2 verticalRotationClamp;

    [Space]
    public Transform modelFacing;
    public TransformWeight[] lookBones;

    Vector2 ssRotation;

    private void Awake()
    {
        PauseMenu.pauseEvent += OnPause;
    }

    private void OnDestroy()
    {
        PauseMenu.pauseEvent -= OnPause;
    }

    private void OnPause(bool isPaused)
    {
        enabled = !isPaused;
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void FixedUpdate()
    {
        ApplyMouseMovement();

        TransformTarget();
    }

    private void LateUpdate()
    {
        float angleDiff = Mathf.DeltaAngle(ssRotation.x, modelFacing.eulerAngles.y);

        foreach (var bone in lookBones)
        {
            bone.bone.rotation *= Quaternion.Euler(Vector3.up * -angleDiff * bone.weight);
        }
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

[System.Serializable]
public class TransformWeight
{
    public Transform bone;
    public float weight;
}