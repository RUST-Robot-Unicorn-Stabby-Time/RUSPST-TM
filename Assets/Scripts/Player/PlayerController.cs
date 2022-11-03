using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    public Transform inputTransform;

    [Space]
    public PlayerWeapon weapon;

    Vector2 moveInput;
    CharacterMovement movement;
    
    public Vector3 MovementDirection
    {
        get
        {
            return inputTransform.TransformDirection(moveInput.x, 0.0f, moveInput.y);
        }
    }

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
        
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

    public void OnMove(InputValue value) => moveInput = value.Get<Vector2>();
    public void OnJump(InputValue value) => SetStateOnComponent<CharacterMovement>((c, s) => c.Jump = s, value);
    public void OnLightAttack() => weapon.Attack();
    public void OnHeavyAttack() { }
    public void OnRage() => CallMethodOnComponemt<Rage>(r => r.UseRage());
    public void OnLock() => CallMethodOnComponemt<LockOnController>(r => r.ToggleTarget());
    public void OnSwitchLock(InputValue value) => SetAxisOnComponent<LockOnController>((r, v) => r.SwitchTarget(Util.Sign(v)), value);
    public void OnDash(InputValue value) => CallMethodOnComponemt<CharacterMovement>(c => c.Dash());

    private void Update()
    {
        movement.MovementDirection = MovementDirection;
    }

    private void SetStateOnComponent<T> (System.Action<T, bool> action, InputValue value)
    {
        if (TryGetComponent<T>(out T component))
        {
            action(component, value.Get<float>() > 0.5f);
        }
    }

    private void SetAxisOnComponent<T>(System.Action<T, float> action, InputValue value)
    {
        if (TryGetComponent<T>(out T component))
        {
            action(component, value.Get<float>());
        }
    }

    private void CallMethodOnComponemt<T>(System.Action<T> action)
    {
        if (TryGetComponent<T>(out T component))
        {
            action(component);
        }
    }
}