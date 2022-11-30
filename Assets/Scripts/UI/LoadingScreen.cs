using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LoadingScreen : MonoBehaviour
{
    AsyncOperation loadOperation;
    Image image;

    public static string NextSceneName { get; private set; }

    public static void LoadScene(string sceneName)
    {
        System.Action loadAction = () =>
        {
            NextSceneName = sceneName;
            SceneManager.LoadScene("LoadScene");
        };

        var transitionScreen = FindObjectOfType<TransitionScreen>();
        if (transitionScreen)
        {
            transitionScreen.Transition(loadAction);
        }
        else
        {
            loadAction();
        }
    }

    private void Start()
    {
        image = GetComponent<Image>();
        loadOperation = SceneManager.LoadSceneAsync(NextSceneName);

        if (loadOperation == null) loadOperation = SceneManager.LoadSceneAsync("MenuScene");
    }

    private void Update()
    {
        image.fillAmount = loadOperation.progress;
    }
}
