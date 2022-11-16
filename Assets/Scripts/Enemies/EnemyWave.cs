using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public int passCount;

    static int enemiesLeft;
    static event System.Action nextWaveEvent;
    public static event System.Action lastWaveSpawned;

    private void Awake()
    {
        gameObject.SetActive(transform.GetSiblingIndex() == 0);
    }

    private void OnEnable()
    {
        enemiesLeft = passCount;
        EnemyActions.EnemyDiedEvent += EnemyDeathEvent;
        
        while(transform.childCount > 0)
        {
            GameObject enemy = transform.GetChild(0).gameObject;
            enemy.SetActive(true);
            enemy.transform.SetParent(null);
        }

        nextWaveEvent += AdvanceWave;
    }

    private void OnDisable()
    {
        nextWaveEvent -= AdvanceWave;
        EnemyActions.EnemyDiedEvent -= EnemyDeathEvent;
    }

    private void AdvanceWave()
    {
        int siblingIndex = transform.GetSiblingIndex();
        if (siblingIndex + 2 >= transform.parent.childCount)
        {
            lastWaveSpawned?.Invoke();
            return;
        }

        transform.parent.GetChild(siblingIndex + 1).gameObject.SetActive(true);
        Destroy(gameObject);
    }

    public static void EnemyDeathEvent (EnemyActions enemy)
    {
        enemiesLeft--;

        if (enemiesLeft <= 0)
        {
            nextWaveEvent?.Invoke();
        }
    }
}
