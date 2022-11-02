using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

[SelectionBase]
[DisallowMultipleComponent]
public class InputArbiter : MonoBehaviour
{
    // This will most likley change a lot during production depending on what input we require.
    // In that case, press/release actions will use events, and continuous values will be a getter.

    public Transform inputTransform;

    [Space]
    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction lightAttackAction;
    public InputAction heavyAttackAction;
    public InputAction rageAction;
    public InputAction lockAction;
    public InputAction switchLockAction;
    public InputAction dashAction;
    public InputAction grappleAction;

    [Space]
    public PlayerWeapon weapon;

    CharacterMovement movement;
    LockOnController lockOn;

    public Vector3 MovementDirection
    {
        get
        {
            Vector2 input = moveAction.ReadValue<Vector2>();
            return inputTransform.TransformDirection(input.x, 0.0f, input.y);
        }
    }

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
        lockOn = GetComponent<LockOnController>();

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
        moveAction.Enable();
        jumpAction.Enable();
        lightAttackAction.Enable();
        heavyAttackAction.Enable();
        rageAction.Enable();
        lockAction.Enable();
        switchLockAction.Enable();
        dashAction.Enable();
        grappleAction.Enable();

        if (weapon) lightAttackAction.performed += (ctx) => weapon.Attack();
        if (lockOn)
        {
            lockAction.performed += (ctx) => lockOn.ToggleTarget();
            switchLockAction.performed += (ctx) => lockOn.SwitchTarget(ctx.ReadValue<float>() > 0.0f ? 1 : -1);
        }

        if (TryGetComponent(out Rage rage))
        {
            rageAction.performed += (ctx) => rage.UseRage();
        }

        dashAction.performed += (ctx) => movement.Dash();

        if (TryGetComponent(out GrappleHook grappleHook))
        {
            grappleAction.performed += (ctx) => grappleHook.ToggleGrapple();
        }
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        lightAttackAction.Disable();
        heavyAttackAction.Disable();
        rageAction.Disable();
        lockAction.Disable();
        switchLockAction.Disable();
        dashAction.Disable();
        grappleAction.Disable();

        if (weapon) lightAttackAction.performed -= (ctx) => weapon.Attack();
        if (lockOn)
        {
            lockAction.performed -= (ctx) => lockOn.ToggleTarget();
            switchLockAction.performed -= (ctx) => lockOn.SwitchTarget(ctx.ReadValue<float>() > 0.0f ? 1 : -1);
        }

        if (TryGetComponent(out Rage rage))
        {
            rageAction.performed += (ctx) => rage.UseRage();
        }

        dashAction.performed -= (ctx) => movement.Dash();

        if (TryGetComponent(out GrappleHook grappleHook))
        {
            grappleAction.performed -= (ctx) => grappleHook.ToggleGrapple();
        }
    }

    private void Update()
    {
        movement.MovementDirection = MovementDirection;
        movement.Jump = jumpAction.ReadValue<float>() > 0.5f;
    }
}

// acts as arbiter between the new input system and genertic bipedal components.