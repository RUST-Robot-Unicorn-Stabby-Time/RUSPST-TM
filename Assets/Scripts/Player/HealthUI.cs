using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image healthBar;
    public Image previousHealthBar;

    protected Health health;

    void Start()
    {
        health = GetComponent<Health>();
    }

    protected virtual void Update()
    {
        healthBar.fillAmount = health.currentHealth;
        if (previousHealthBar) previousHealthBar.fillAmount = health.currentHealth + (health.damageTaken / health.maxHealth.GetFor(this));

        if (health.currentHealth <= 0)
        {
            healthBar.fillAmount = 0;
            if (previousHealthBar) previousHealthBar.fillAmount = 0;
        }
    }
}
