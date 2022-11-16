using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    [SerializeField] int stages;
    [SerializeField] UnityEvent[] stageEvent;

    Health health;

    int currentStage;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void Start()
    {
        currentStage = -1;
    }

    private void OnEnable()
    {
        health.DamageEvent += OnDamage;
    }

    private void OnDisable()
    {
        health.DamageEvent -= OnDamage;
    }

    private void OnDamage(DamageArgs args)
    {
        int stage = stages - (int)(health.currentHealth * stages) - 2;
        if (stage != currentStage)
        {
            stageEvent[stage]?.Invoke();
        }
        currentStage = stage;
    }
}
