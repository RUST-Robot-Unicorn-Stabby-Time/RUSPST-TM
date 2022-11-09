using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerController.ReleaseControl(true);
        transform.SetParent(null);
    }

    private void OnDisable()
    {
        PlayerController.ReleaseControl(false);
    }

    public void LoadScene (string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Quit ()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
