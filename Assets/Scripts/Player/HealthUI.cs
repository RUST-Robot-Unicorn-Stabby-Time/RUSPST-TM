using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image healthBar;

    private Health health;

    void Start()
    {
        health = GetComponent<Health>();
    }

    void Update()
    {
        healthBar.fillAmount = health.currentHealth;
    }
}
