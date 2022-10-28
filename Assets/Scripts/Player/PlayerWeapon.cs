using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float cooldown = 0f;
    public new Animator animation;
    public CharacterMovement characterMovement;
    public bool freezeMovement;
    public Vector3 impulseForce;

    [Space]
    public string attackAnimName;
    public int attackAnimLayer;

    private float lastClickTime = 0f;

    PlayerAnimator playerAnimator;

    public event System.Action BeginAttackEvent;

    public void Awake()
    {
        playerAnimator = transform.root.GetComponentInChildren<PlayerAnimator>();
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
        if (freezeMovement) characterMovement.PauseMovement = true;

        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.velocity += transform.TransformVector(impulseForce);
        }

        if (playerAnimator)
        {
            playerAnimator.DirectionLock = transform.forward;
        }

        yield return new WaitForSeconds(cooldown);

        playerAnimator.DirectionLock = null;
        if (freezeMovement) characterMovement.PauseMovement = false;
    }
}