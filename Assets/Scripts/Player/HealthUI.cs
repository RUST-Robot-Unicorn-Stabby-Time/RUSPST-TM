using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Health health;
    public Stat maxhealth;
    private float currentHealth;
    public Image healthBar;

    void Start()
    {
        health = GetComponent<Health>();
    }
    void Update()
    {
        healthBar.fillAmount = health.currentHealth;
    }
}
