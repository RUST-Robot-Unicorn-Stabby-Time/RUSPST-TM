using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EpilepsyWarning : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField][SceneReference] string nextScene;

    IEnumerator Start ()
    {
        float time = 0.0f;
        while (time < this.time)
        {
            if (Keyboard.current.escapeKey.wasReleasedThisFrame)
            {
                LoadingScreen.LoadScene(nextScene);
                yield break;
            }

            time += Time.deltaTime;
            yield return null;
        }

        LoadingScreen.LoadScene(nextScene);
    }
}
