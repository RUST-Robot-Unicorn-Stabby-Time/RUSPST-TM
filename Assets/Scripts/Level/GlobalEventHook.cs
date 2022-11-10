using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventHook : MonoBehaviour
{
    public UnityEvent onAllEnemiesDead;
    public UnityEvent onAllPlayersDead;

    private void OnEnable()
    {
        EnemyActions.AllEnemiesDeadEvent += onAllEnemiesDead.Invoke;
        PlayerController.AllPlayersDeadEvent += onAllPlayersDead.Invoke;
    }

    private void OnDisable()
    {
        EnemyActions.AllEnemiesDeadEvent -= onAllEnemiesDead.Invoke;
        PlayerController.AllPlayersDeadEvent -= onAllPlayersDead.Invoke;
    }
}
