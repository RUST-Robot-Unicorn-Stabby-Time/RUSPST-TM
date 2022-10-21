using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float cooldown = 0f;
    public new Animator animation;
    public CharacterMovement characterMovement;
    public bool freezeMovement;

    [Space]
    public string attackAnimName;
    public int attackAnimLayer;

    private float lastClickTime = 0f;

    public void Attack()
    {
        if (Time.time > lastClickTime + cooldown)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        lastClickTime = Time.time;
        animation.Play("Attack", attackAnimLayer, 0.0f);
        if (freezeMovement) characterMovement.PauseMovement = true;

        yield return new WaitForSeconds(cooldown);

        if (freezeMovement) characterMovement.PauseMovement = false;
    }
}