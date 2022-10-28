using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public static event System.Action<bool> pauseEvent;

    public GameObject pauseMenuUI;
    public GameObject OptionsMenu;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (gameIsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    //Pause and Play
    public void Resume()
    {
        pauseEvent.Invoke(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    void Pause()
    {
        pauseEvent.Invoke(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    //OptionsMenu
    public void OpenOptions()
    {
        pauseMenuUI.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    //Back To Menu
    public void BackToMenu()
    {

    }

    //Quit
    public void Quit()
    {
        Application.Quit();
    }

}
