using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float cooldown = 0f;
    public new Animator animation;
    public CharacterMovement characterMovement;
    public Vector3 impulseForce;
    public bool freezeMovement;

    [Space]
    public string attackAnimName;
    public int attackAnimLayer;

    [Space]
    [SerializeField] Cinemachine.CinemachineCollisionImpulseSource shakeSource;
    [SerializeField] float shakeDelay;

    [Space]
    [SerializeField] GameObject hitPrefab;
    [SerializeField] Transform hitPoint;
    [SerializeField] Vector3 hitOffset;
    [SerializeField] float hitFXdelay;

    private float lastClickTime = 0f;

    PlayerAnimator playerAnimator;
    HitReact hitReact;

    public event System.Action BeginAttackEvent;
    public event System.Action FinishAttackEvent;

    public bool Attacking { get; private set; }
    public bool FreezeMovement => freezeMovement;

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

        Attacking = true;

        lastClickTime = Time.time;
        animation.Play(attackAnimName, attackAnimLayer, 0.0f);

        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.velocity += transform.TransformVector(impulseForce);
        }

        if (playerAnimator)
        {
            playerAnimator.DirectionLock = transform.forward;
        }

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

            if (hitPrefab)
            {
                if (time > hitFXdelay && !fxSpawned)
                {
                    Instantiate(hitPrefab, hitPoint.position + hitOffset, hitPoint.rotation * hitPrefab.transform.rotation);
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