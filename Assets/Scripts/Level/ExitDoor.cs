using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] GameObject[] enableOnExit;
    [SerializeField] float distance;
    public FinishAction action;
    [HideInInspector] public GameData data;
    [HideInInspector] public GameGenerator generator;
    [HideInInspector] public UnityEvent finishEvent;

    bool lastWave;

    private void OnEnable()
    {
        EnemyWave.lastWaveSpawned += OnLastWaveSpawned;
        EnemyActions.AllEnemiesDeadEvent += FinishLevel;
    }

    private void OnDisable()
    {
        EnemyWave.lastWaveSpawned -= OnLastWaveSpawned;
        EnemyActions.AllEnemiesDeadEvent -= FinishLevel;
    }

    private void OnLastWaveSpawned() => lastWave = true;

    private void Start()
    {
        foreach (var go in enableOnExit)
        {
            go.SetActive(false);
        }
    }

    private void Update()
    {
        foreach (var player in PlayerController.AlivePlayers)
        {
            if ((player.transform.position - transform.position).sqrMagnitude < distance * distance)
            {
                switch (action)
                {
                    case FinishAction.NextRoom:
                        data.LoadNextLevel();
                        break;
                    case FinishAction.GenerateGame:
                        generator.GenerateGame();
                        break;
                    case FinishAction.Custom:
                    default:
                        finishEvent?.Invoke();
                        break;
                }

                enabled = false;
                return;
            }
        }
    }

    private void FinishLevel()
    {
        if (!lastWave) return;

        foreach (var go in enableOnExit)
        {
            go.SetActive(true);
        }
    }

    public enum FinishAction
    {
        NextRoom,
        GenerateGame,
        Custom
    }
}
