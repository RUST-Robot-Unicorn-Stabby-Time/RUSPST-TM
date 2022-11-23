using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float cooldown = 0f;
    public new Animator animation;
    public CharacterMovement characterMovement;
    public Vector3 impulseForce;
    public float freezeMovementTime;
    public float directionLockDelay;

    [Space]
    public string attackAnimName;
    public int attackAnimLayer;

    [Space]
    [SerializeField] Cinemachine.CinemachineImpulseSource shakeSource;
    [SerializeField] float shakeDelay;

    [Space]
    [SerializeField] GameObject hitPrefab;
    [SerializeField] Transform hitPoint;
    [SerializeField] Vector3 hitOffset;
    [SerializeField] float hitFXdelay;

    [Space]
    [SerializeField] SoundProfile[] attackSounds;

    private float lastClickTime = 0f;

    PlayerAnimator playerAnimator;
    HitReact hitReact;

    public event System.Action BeginAttackEvent;
    public event System.Action FinishAttackEvent;

    public bool Attacking { get; private set; }
    public bool FreezeMovement => Time.time < lastClickTime + freezeMovementTime;

    public void Awake()
    {
        playerAnimator = transform.root.GetComponentInChildren<PlayerAnimator>();
        hitReact = GetComponent<HitReact>();
    }

    public void Attack()
    {
        if (Time.time > lastClickTime + cooldown)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        if (Attacking) yield break;

        BeginAttackEvent?.Invoke();

        foreach (var sound in attackSounds) sound.Play();

        Attacking = true;

        lastClickTime = Time.time;
        animation.Play(attackAnimName, attackAnimLayer, 0.0f);

        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.velocity += transform.TransformVector(impulseForce);
        }

        Vector3 direction = transform.forward;
        bool shooketh = false;
        bool fxSpawned = false;
        float time = 0.0f;
        while (time < cooldown)
        {
            if (time > shakeDelay && !shooketh)
            {
                if (shakeSource) shakeSource.GenerateImpulse();
                shooketh = true;
            }

            if (time < directionLockDelay)
            {
                if (playerAnimator)
                {
                    playerAnimator.DirectionLock = direction;
                }
            }
            else
            {
                direction = transform.forward;
            }

            if (hitPrefab)
            {
                if (time > hitFXdelay && !fxSpawned)
                {
                    Instantiate(hitPrefab, hitPoint.position + hitPoint.rotation * hitOffset, hitPoint.rotation * hitPrefab.transform.rotation);
                    fxSpawned = true;
                }
            }

            time += Time.deltaTime;
            yield return null;
        }

        playerAnimator.DirectionLock = null;

        Attacking = false;

        FinishAttackEvent?.Invoke();
    }
}