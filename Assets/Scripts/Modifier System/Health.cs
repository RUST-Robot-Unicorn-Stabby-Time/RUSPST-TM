using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour

{
    public Stat maxHealth;
    public float currentHealth = 1;

    public void Die(DamageArgs damageArgs)
    {
        gameObject.SetActive(false);
    }
    public void Damage(DamageArgs damageArgs)
    {
        currentHealth -= damageArgs.damage / maxHealth.GetFor(this);

        if (currentHealth <= 0)
        {
            Die(damageArgs);
        }
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
