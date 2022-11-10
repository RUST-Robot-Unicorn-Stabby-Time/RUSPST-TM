using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float cooldown = 0f;
    public new Animator animation;
    public CharacterMovement characterMovement;
    public Vector3 impulseForce;

    [Space]
    public string attackAnimName;
    public int attackAnimLayer;

    private float lastClickTime = 0f;

    PlayerAnimator playerAnimator;
    HitReact hitReact;

    public event System.Action BeginAttackEvent;
    public event System.Action FinishAttackEvent;

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
        BeginAttackEvent?.Invoke();

        lastClickTime = Time.time;
        animation.Play("Attack", attackAnimLayer, 0.0f);

        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.velocity += transform.TransformVector(impulseForce);
        }

        if (playerAnimator)
        {
            playerAnimator.DirectionLock = transform.forward;
        }

        float time = 0.0f;
        while (time < cooldown)
        {
            if (hitReact.Stunned)
            {
                break;
            }

            time += Time.deltaTime;
            yield return null;
        }

        playerAnimator.DirectionLock = null;

        FinishAttackEvent?.Invoke();
    }
}