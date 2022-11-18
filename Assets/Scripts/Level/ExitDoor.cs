using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] GameObject[] enableOnExit;
    [SerializeField] float distance;
    public FinishAction action;
    public string nextRoomName;
    public UnityEvent finishEvent;

    public List<System.Func<bool>> WinConditions { get; private set; } = new List<System.Func<bool>>();

    private void Start()
    {
        foreach (var go in enableOnExit)
        {
            go.SetActive(false);
        }
    }

    private void Update()
    {
        foreach (var condition in WinConditions)
        {
            if (!condition()) return;
        }

        foreach (var player in PlayerController.AlivePlayers)
        {
            if ((player.transform.position - transform.position).sqrMagnitude < distance * distance)
            {
                switch (action)
                {
                    case FinishAction.NextRoom:
                        FindObjectOfType<SceneLoader>().LoadScene(nextRoomName);
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

        foreach (var go in enableOnExit)
        {
            go.SetActive(true);
        }
    }

    public enum FinishAction
    {
        NextRoom,
        Custom
    }
}
