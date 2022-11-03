using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public int passCount;
    public EnemyWave nextWave;

    HashSet<Health> trackedEnemies;

    static int enemiesLeft;
    static event System.Action nextWaveEvent;

    private void Awake()
    {
        if (nextWave) nextWave.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        enemiesLeft = passCount;
        trackedEnemies = new HashSet<Health>();

        //for (int i = 0; i < transform.childCount; i++)
        while(transform.childCount > 0)
        {
            GameObject enemy = transform.GetChild(0).gameObject;
            enemy.SetActive(true);
            enemy.transform.SetParent(null);

            if (enemy.TryGetComponent(out Health health))
            {
                health.DeathEvent += OnEnemyDeathEvent;
                trackedEnemies.Add(health);
            }
        }

        nextWaveEvent += AdvanceWave;
    }

    private void OnDisable()
    {
        foreach (var enemy in trackedEnemies)
        {
            enemy.DeathEvent -= OnEnemyDeathEvent;
        }

        nextWaveEvent -= AdvanceWave;
    }

    private void AdvanceWave()
    {
        if (!nextWave) return;

        nextWave.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    public static void OnEnemyDeathEvent (DamageArgs args)
    {
        enemiesLeft--;

        if (enemiesLeft <= 0)
        {
            nextWaveEvent?.Invoke();
        }
    }
}
