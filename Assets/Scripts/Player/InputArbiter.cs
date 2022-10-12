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

    [Space]
    public PlayerWeapon weapon;

    CharacterMovement movement;

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
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        lightAttackAction.Enable();
        heavyAttackAction.Enable();

        lightAttackAction.performed += (ctx) => weapon.Attack();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        lightAttackAction.Disable();
        heavyAttackAction.Disable();
    }

    private void Update()
    {
        movement.MovementDirection = MovementDirection;
        movement.Jump = jumpAction.ReadValue<float>() > 0.5f;
    }
}

// acts as arbiter between the new input system and genertic bipedal components.