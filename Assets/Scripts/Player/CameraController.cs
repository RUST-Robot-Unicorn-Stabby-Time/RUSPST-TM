using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity;
    public Transform cameraTarget;
    public Vector2 verticalRotationClamp;

    [Space]
    public Transform modelFacing;
    public TransformWeight[] lookBones;

    [Space]
    public float smoothTime;

    [Space]
    public CinemachineVirtualCamera vCam;

    PlayerAnimator playerAnimator;
    Vector2 ssRotation;

    float offsetRotation;
    float targetOffsetRotation;
    float rotationVelocity;

    bool wasDirectionLocked;

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();

        PlayerController.UnlockControlsEvent += OnPause;

        ssRotation = new Vector2(transform.eulerAngles.y, 0.0f);
    }

    private void OnDestroy()
    {
        PlayerController.UnlockControlsEvent -= OnPause;
    }

    private void OnPause(bool isPaused)
    {
        enabled = !isPaused;
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;


        //Load settings and update camera related settings
        OptionsData.LoadOptions();//temp move to start screen
        if (vCam && OptionsData.instance != null)
        {
            vCam.m_Lens.FieldOfView = OptionsData.instance.fov;
        }

        mouseSensitivity = OptionsData.instance.sensitivity;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void FixedUpdate()
    {
        if (playerAnimator.DirectionLock.HasValue)
        {
            if (!wasDirectionLocked)
            {
                offsetRotation += ssRotation.x;
                ssRotation.x = 0.0f;
            }

            targetOffsetRotation = Mathf.Atan2(playerAnimator.DirectionLock.Value.x, playerAnimator.DirectionLock.Value.z) * Mathf.Rad2Deg;
            offsetRotation = Mathf.SmoothDampAngle(offsetRotation, targetOffsetRotation, ref rotationVelocity, smoothTime);
        }
        wasDirectionLocked = playerAnimator.DirectionLock.HasValue;

        ApplyMouseMovement();

        TransformTarget();
    }

    private void LateUpdate()
    {
        float angleDiff = Mathf.DeltaAngle(ssRotation.x + offsetRotation, modelFacing.eulerAngles.y);

        foreach (var bone in lookBones)
        {
            bone.bone.rotation *= Quaternion.Euler(Vector3.up * -angleDiff * bone.weight);
        }


    }

    private void TransformTarget()
    {
        ssRotation.y = Mathf.Clamp(ssRotation.y, verticalRotationClamp.x, verticalRotationClamp.y);
        float xRot = ssRotation.x + offsetRotation;

        transform.rotation = Quaternion.Euler(0.0f, xRot, 0.0f);
        cameraTarget.transform.rotation = Quaternion.Euler(-ssRotation.y, xRot, 0.0f);
    }

    private void ApplyMouseMovement()
    {
        Vector2 mouseInput = Mouse.current.delta.ReadValue();
        ssRotation += mouseInput * mouseSensitivity;
    }
}

[System.Serializable]
public class TransformWeight
{
    public Transform bone;
    public float weight;
}