using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public int passCount;
    public EnemyWave nextWave;

    static int enemiesLeft;
    static event System.Action nextWaveEvent;

    private void Awake()
    {
        if (nextWave) nextWave.gameObject.SetActive(false);
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
        if (!nextWave) return;

        nextWave.gameObject.SetActive(true);
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
