using System.Collections.Generic;
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
    HitReact hitReact;

    public static HashSet<PlayerController> AlivePlayers { get; } = new HashSet<PlayerController>();
    public static event System.Action AllPlayersDeadEvent;

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
        hitReact = GetComponent<HitReact>();

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
        AlivePlayers.Add(this);
    }

    private void OnDisable()
    {
        AlivePlayers.Remove(this);
        if (AlivePlayers.Count == 0) AllPlayersDeadEvent?.Invoke();
    }

    public void OnMove(InputValue value) => moveInput = value.Get<Vector2>();
    public void OnJump(InputValue value) => SetStateOnComponent<CharacterMovement>((c, s) => c.Jump = s, value);
    public void OnLightAttack() => weapon.Attack();
    public void OnHeavyAttack() { }
    public void OnRage() => CallMethodOnComponent<Rage>(r => r.UseRage());
    public void OnLock() => CallMethodOnComponent<LockOnController>(r => r.ToggleTarget());
    public void OnSwitchLock(InputValue value) => SetAxisOnComponent<LockOnController>((r, v) => r.SwitchTarget(Util.Sign(v)), value);
    public void OnDash(InputValue value) => CallMethodOnComponent<CharacterMovement>(c => c.Dash());

    private void Update()
    {
        if (hitReact ? !hitReact.Stunned : true)
        {
            movement.MovementDirection = MovementDirection;
        }
        else
        {
            movement.MovementDirection = Vector3.zero;
        }
    }

    private void SetStateOnComponent<T> (System.Action<T, bool> action, InputValue value)
    {
        if (hitReact ? hitReact.Stunned : false) return;

        if (TryGetComponent<T>(out T component))
        {
            action(component, value.Get<float>() > 0.5f);
        }
    }

    private void SetAxisOnComponent<T>(System.Action<T, float> action, InputValue value)
    {
        if (hitReact ? hitReact.Stunned : false) return;

        if (TryGetComponent<T>(out T component))
        {
            action(component, value.Get<float>());
        }
    }

    private void CallMethodOnComponent<T>(System.Action<T> action)
    {
        if (hitReact ? hitReact.Stunned : false) return;

        if (TryGetComponent<T>(out T component))
        {
            action(component);
        }
    }
}