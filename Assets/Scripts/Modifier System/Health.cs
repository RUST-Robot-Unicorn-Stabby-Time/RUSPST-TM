using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Stat maxHealth;
    [Range(0.0f, 1.0f)] public float currentHealth = 1;

    public void Damage(DamageArgs damageArgs)
    {
        currentHealth -= damageArgs.damage / maxHealth.GetFor(this);

        if (currentHealth <= 0)
        {
            Die(damageArgs);
        }
    }

    public void Die(DamageArgs damageArgs)
    {
        gameObject.SetActive(false);
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
    public DamageArgs(GameObject damager, float damage)
    {
        this.damager = damager;
        this.damage = damage;
    }
}
