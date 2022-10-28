using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image healthBar;
    public Image previousHealthBar;

    private Health health;

    void Start()
    {
        health = GetComponent<Health>();
    }

    void Update()
    {
        healthBar.fillAmount = health.currentHealth;
        previousHealthBar.fillAmount = health.currentHealth + (health.damageTaken / health.maxHealth.GetFor(this));

        if (health.currentHealth <= 0)
        {
            healthBar.fillAmount = 0;
            previousHealthBar.fillAmount = 0;
        }
    }
}
