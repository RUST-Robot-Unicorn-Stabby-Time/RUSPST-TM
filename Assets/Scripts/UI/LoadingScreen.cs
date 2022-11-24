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
        NextSceneName = sceneName;
        SceneManager.LoadScene("LoadScene");
    }

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        loadOperation = SceneManager.LoadSceneAsync(NextSceneName);
    }

    private void Update()
    {
        image.fillAmount = loadOperation.progress;
    }
}
