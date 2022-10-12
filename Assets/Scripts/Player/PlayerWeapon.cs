using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float cooldown = 0f;
    public new Animator animation;

    private float lastClickTime = 0f;

    public void Attack()
    {
        if (Time.time > lastClickTime + cooldown)
        {
            lastClickTime = Time.time;
            animation.Play("Attack");
        }
    }
}