using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] GameObject[] enableOnExit;

    private void OnEnable()
    {
        EnemyActions.AllEnemiesDeadEvent += OnAllEnemiesDead;
    }

    private void OnDisable()
    {
        EnemyActions.AllEnemiesDeadEvent -= OnAllEnemiesDead;
    }

    private void Start()
    {
        foreach (var go in enableOnExit)
        {
            go.SetActive(false);
        }
    }

    private void OnAllEnemiesDead()
    {
        foreach (var go in enableOnExit)
        {
            go.SetActive(true);
        }
    }
}
