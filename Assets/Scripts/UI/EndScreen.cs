using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerController.ReleaseControl(true);
    }

    private void OnDisable()
    {
        PlayerController.ReleaseControl(false);
    }

    public void LoadScene (string name)
    {
        SceneManager.LoadScene(name);
    }
}
