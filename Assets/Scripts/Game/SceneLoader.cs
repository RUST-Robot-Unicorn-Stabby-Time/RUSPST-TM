using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[SelectionBase]
[DisallowMultipleComponent]
public class SceneLoader : MonoBehaviour
{
    bool busy;

    public static float LoadPercent { get; private set; }

    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadScene (string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        if (busy) yield break;

        busy = true;

        var loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        SceneManager.LoadScene("LoadScene", LoadSceneMode.Additive);

        LoadPercent = 0.0f;
        while (!loadOperation.isDone)
        {
            LoadPercent = loadOperation.progress;
            yield return null;
        }

        LoadPercent = 1.0f;
        SceneManager.UnloadSceneAsync("LoadScene");
        LoadPercent = 0.0f;

        busy = false;
    }
}

