using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

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

        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(levelPath);
        Context.StartCoroutine(LoadRoutine(loadingOperation));

        currentLevel++;
    }

    private IEnumerator LoadRoutine(AsyncOperation loadingOperation)
    {
        StartLevelLoading?.Invoke();

        while (!loadingOperation.isDone)
        {
            LoadingProgress?.Invoke(loadingOperation.progress);
            yield return null;
        }

        FinishedLevelLoading?.Invoke();
    }
}

// Holds data pertaining to the current run.