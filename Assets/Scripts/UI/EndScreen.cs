using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerController.UnlockControls(true);
        transform.SetParent(null);
    }

    private void OnDisable()
    {
        PlayerController.UnlockControls(false);
    }

    public void LoadScene(string name)
    {
        LoadingScreen.LoadScene(name);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
