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
        loseScreen.transform.SetParent(null);
        loseScreen.SetActive(true);
    }

    private void AllEnemiesDeadEvent()
    {
        if (!isLastLevel) return;

        winScreen.transform.SetParent(null);
        winScreen.SetActive(true);
    }

    public void LoadLobby ()
    {
        LoadingScreen.LoadScene("LobbyRoom");
    }

    public void LoadMainMenu ()
    {
        LoadingScreen.LoadScene("MenuScene");
    }
}
