using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventHook : MonoBehaviour
{
    public UnityEvent onAllEnemiesDead;

    private void OnEnable()
    {
        EnemyBase.AllEnemiesDeadEvent += onAllEnemiesDead.Invoke;
        EnemyBase.AllEnemiesDeadEvent += () => print("you did it :)");
    }

    private void OnDisable()
    {
        EnemyBase.AllEnemiesDeadEvent -= onAllEnemiesDead.Invoke;
    }
}
