using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float cooldown = 0f;
    public new Animator animation;
    public CharacterMovement characterMovement;

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
        animation.Play("Attack");
        characterMovement.PauseMovement = true;

        yield return new WaitForSeconds(cooldown);

        characterMovement.PauseMovement = false;
    }
}