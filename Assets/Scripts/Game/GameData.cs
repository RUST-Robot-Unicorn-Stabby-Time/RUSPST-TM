using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[UnityEditor.InitializeOnLoad]
[CreateAssetMenu(menuName = "Scriptable Objects/Game Data/Game Data")]
public class GameData : ScriptableObject
{
    public List<string> levelList;
    public string finalScene;
    public int currentLevel;

    [Space]
    public UnityEvent StartLevelLoading;
    public UnityEvent<float> LoadingProgress;
    public UnityEvent FinishedLevelLoading;

    public static float LoadPercent { get; private set; }

    private LiterallyDoesNothing _context;
    private LiterallyDoesNothing Context
    {
        get
        {
            if (!_context)
            {
                _context = new GameObject("GameData Context").AddComponent<LiterallyDoesNothing>();
                DontDestroyOnLoad(_context);
            }
            return _context;
        }
    }

    private void OnEnable()
    {
        currentLevel = 0;
    }

    public void LoadNextLevel()
    {
        string levelPath;
        if (currentLevel >= levelList.Count)
        {
            levelPath = finalScene;
            currentLevel = -1;
            levelList.Clear();
        }
        else
        {
            levelPath = levelList[currentLevel];
        }

        Context.StartCoroutine(LoadRoutine(levelPath));

        currentLevel++;
    }

    private IEnumerator LoadRoutine(string levelPath)
    {
        SceneManager.LoadScene("LoadScene", LoadSceneMode.Additive);

        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(levelPath);

        StartLevelLoading?.Invoke();

        LoadPercent = 0.0f;
        Time.timeScale = 0.0f;

        while (!loadingOperation.isDone)
        {
            LoadPercent = loadingOperation.progress;
            LoadingProgress?.Invoke(loadingOperation.progress * 0.9f);
            yield return null;
        }

        LoadPercent = 0.9f;

        yield return new WaitForSeconds(3.0f);

        LoadPercent = 0.0f;
        Time.timeScale = 1.0f;

        SceneManager.UnloadSceneAsync("LoadScene");

        FinishedLevelLoading?.Invoke();
    }
}

// Holds data pertaining to the current run.