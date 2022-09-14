using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

[SelectionBase]
[DisallowMultipleComponent]
public class InputArbiter : MonoBehaviour, IController
{
    // This will most likley change a lot during production depending on what input we require.
    // In that case, press/release actions will use events, and continuous values will be a getter.

    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction lightAttackAction;
    public InputAction heavyAttackAction;

    public Vector3 MovementDirection
    {
        get
        {
            Vector2 input = moveAction.ReadValue<Vector2>();
            return transform.TransformDirection(input.x, 0.0f, input.y);
        }
    }
    
    public event System.Action<float> JumpEvent;
    public event System.Action<float> LightAttackEvent;
    public event System.Action<float> HeavyAttackEvent;

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        lightAttackAction.Enable();
        heavyAttackAction.Enable();

        jumpAction.performed += (ctx) => JumpEvent(ctx.ReadValue<float>());
        lightAttackAction.performed += (ctx) => LightAttackEvent(ctx.ReadValue<float>());
        heavyAttackAction.performed += (ctx) => HeavyAttackEvent(ctx.ReadValue<float>());
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        lightAttackAction.Disable();
        heavyAttackAction.Disable();

        jumpAction.performed -= (ctx) => JumpEvent(ctx.ReadValue<float>());
        lightAttackAction.performed -= (ctx) => LightAttackEvent(ctx.ReadValue<float>());
        heavyAttackAction.performed -= (ctx) => HeavyAttackEvent(ctx.ReadValue<float>());
    }
}

// acts as arbiter between the new input system and genertic bipedal components.