using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject OptionsMenu;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (OptionsMenu.activeSelf == false)
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
    }
    //Pause and Play
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    private void OnDisable()
    {
        Resume();
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
        //scene manager
    }

    //Quit
    public void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

}
