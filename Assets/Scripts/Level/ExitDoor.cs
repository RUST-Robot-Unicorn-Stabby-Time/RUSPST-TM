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

    private void OnAllEnemiesDead()
    {
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
