using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    public Transform inputTransform;

    [Space]
    public PlayerWeapon[] weapons;

    PlayerInput input;
    Vector2 moveInput;
    CharacterMovement movement;
    HitReact hitReact;

    public static HashSet<PlayerController> AlivePlayers { get; } = new HashSet<PlayerController>();
    public static event System.Action AllPlayersDeadEvent;
    public static int ControlUnlocks { get; private set; }

    public static event System.Action<bool> UnlockControlsEvent;

    public Vector3 MovementDirection
    {
        get
        {
            return inputTransform.TransformDirection(moveInput.x, 0.0f, moveInput.y);
        }
    }

    public static void UnlockControls (bool state)
    {
        if (state) ControlUnlocks++;
        else ControlUnlocks--;
        UnlockControlsEvent?.Invoke(state);
    }

    private void Awake()
    {
        ControlUnlocks = 0;
        movement = GetComponent<CharacterMovement>();
        hitReact = GetComponent<HitReact>();
        input = GetComponent<PlayerInput>();

        UnlockControlsEvent += OnControlsUnlocked;
    }

    private void Start()
    {
        OnControlsUnlocked();
    }

    private void OnDestroy()
    {
        UnlockControlsEvent -= OnControlsUnlocked;
    }

    private void OnControlsUnlocked(bool state = false)
    {
        input.enabled = ControlUnlocks == 0;
    }

    private void OnEnable()
    {
        AlivePlayers.Add(this);

        if (TryGetComponent(out Health health))
        {
            health.DeathEvent += OnDeath;
        }

        OnControlsUnlocked();
    }

    private void OnDeath(DamageArgs obj)
    {
        AlivePlayers.Remove(this);
        if (AlivePlayers.Count == 0) AllPlayersDeadEvent?.Invoke();
    }

    private void OnDisable()
    {
        AlivePlayers.Remove(this);

        if (TryGetComponent(out Health health))
        {
            health.DeathEvent -= OnDeath;
        }
    }

    public void OnMove(InputValue value) => moveInput = value.Get<Vector2>();
    public void OnJump(InputValue value) => SetStateOnComponent<CharacterMovement>((c, s) => c.JumpState = s, value);
    public void OnLightAttack()
    {
        if (weapons.Length >= 1)
            weapons[0].Attack();
    }

    public void OnHeavyAttack()
    {
        if (weapons.Length >= 2)
            weapons[1].Attack();
    }

    public void OnRage() => CallMethodOnComponent<Rage>(r => r.UseRage());
    public void OnLock() => CallMethodOnComponent<LockOnController>(r => r.ToggleTarget());
    public void OnSwitchLock(InputValue value) => SetAxisOnComponent<LockOnController>((r, v) => r.SwitchTarget(Util.Sign(v)), value);
    
    private void Update()
    {
        if (hitReact ? !hitReact.Stunned : true)
        {
            movement.MoveDirection = MovementDirection;
        }
        else
        {
            movement.MoveDirection = Vector3.zero;
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