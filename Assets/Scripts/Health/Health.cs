using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Stat maxHealth;
    [Range(0.0f, 1.0f)] public float currentHealth = 1;

    public float healingTimer = 6;
    [Range(0.0f, 1.0f)] public float healPercentage = 0.2f;
    private float hitTime;
    [HideInInspector] public float damageTaken;

    [Space]
    public GameObject hitFX;
    public GameObject deadBody;

    public float decaySpeed = 2;

    public event System.Action<DamageArgs> DamageEvent;
    public event System.Action<DamageArgs> DeathEvent;

    public HashSet<HurtBox> hurtBoxes;

    private void OnEnable()
    {
        hurtBoxes = new HashSet<HurtBox>(GetComponentsInChildren<HurtBox>());

        foreach (var hurtbox in hurtBoxes)
        {
            hurtbox.HitEvent += (g, a) => OnDealDamage(a);
        }
    }

    private void OnDisable()
    {
        foreach (var hurtbox in hurtBoxes)
        {
            hurtbox.HitEvent -= (g, a) => OnDealDamage(a);
        }
    }

    private void Update()
    {
        if (Time.time > hitTime + healingTimer)
        {
            if (damageTaken >= 0)
            {
                damageTaken -= decaySpeed * Time.deltaTime;
            }
            else
            {
                damageTaken = 0;
            }
        }
    }

    [ContextMenu("OnDealDamage")]
    public void OnDealDamage() => OnDealDamage(new DamageArgs(null, 20.0f, true));
    public void OnDealDamage(DamageArgs damageArgs)
    {
        currentHealth += (damageTaken * healPercentage) / maxHealth.GetFor(this);
        damageTaken -= damageTaken * healPercentage;
    }

    public void Damage(DamageArgs damageArgs)
    {
        damageTaken = damageArgs.damage;
        currentHealth -= damageArgs.damage / maxHealth.GetFor(this);

        DamageEvent?.Invoke(damageArgs);

        if (hitFX) Instantiate(hitFX, transform.position, transform.rotation);

        if (currentHealth > 0)
        {
            hitTime = Time.time;
        }
        else
        {
            Die(damageArgs);
        }
    }

    public void Die() => Die(new DamageArgs(null, 0.0f, false));
    public void Die(DamageArgs damageArgs)
    {
        DeathEvent?.Invoke(damageArgs);

        if (deadBody) Instantiate(deadBody, transform.position, transform.rotation);

        gameObject.SetActive(false);
    }

    [ContextMenu("TakeDamage")]
    public void TakeDamageTest()
    {
        Damage(new DamageArgs(null, 20, true));
    }

    public void Revive()
    {
        gameObject.SetActive(true);
        currentHealth = 1.0f;
    }
}

public struct DamageArgs
{
    public GameObject damager;
    public float damage;
    public bool stun;

    public DamageArgs(GameObject damager, float damage, bool stun)
    {
        this.damager = damager;
        this.damage = damage;
        this.stun = stun;
    }
}
