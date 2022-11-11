using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndUI : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;
    [SerializeField] bool isLastLevel;

    private void OnEnable()
    {
        PlayerController.AllPlayersDeadEvent += AllPlayersDeadEvent;
        EnemyActions.AllEnemiesDeadEvent += AllEnemiesDeadEvent;
    }

    private void OnDisable()
    {
        PlayerController.AllPlayersDeadEvent -= AllPlayersDeadEvent;
        EnemyActions.AllEnemiesDeadEvent -= AllEnemiesDeadEvent;
    }

    private void Start()
    {
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    private void AllPlayersDeadEvent()
    {
        loseScreen.SetActive(true);
    }

    private void AllEnemiesDeadEvent()
    {
        if (!isLastLevel) return;

        winScreen.SetActive(true);
        PlayerController.ReleaseControl(true);
    }

    public void LoadLobby ()
    {
        SceneManager.LoadSceneAsync("LobbyRoom");
    }

    public void LoadMainMenu ()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
}
