using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[DisallowMultipleComponent]
[RequireComponent(typeof(Health))]
public class HitReact : MonoBehaviour
{
    public float stunDuration;
    public Animator animator;
    public string hitAnimName;

    float lastDamageTime;

    Health health;
    CharacterMovement movement;

    public bool Stunned => Time.time < lastDamageTime + stunDuration;

    private void Awake()
    {
        health = GetComponent<Health>();
        movement = GetComponent<CharacterMovement>();
    }

    private void OnEnable()
    {
        health.DamageEvent += OnDamage;
    }

    private void OnDisable()
    {
        health.DamageEvent -= OnDamage;
    }

    private void OnDamage(DamageArgs obj)
    {
        lastDamageTime = Time.time;
        animator.Play(hitAnimName, 0, 0.0f);
    }
}
