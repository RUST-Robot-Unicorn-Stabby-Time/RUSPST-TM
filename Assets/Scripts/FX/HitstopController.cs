using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitstopController : MonoBehaviour
{
    float time;

    public static HitstopController Instance { get; private set; }

    private void Awake()
    {
        if (Instance) Destroy(Instance.gameObject);
        Instance = this;
    }

    public void Play(float duration)
    {
        time = Time.unscaledTime + duration;
    }

    private void Update()
    {
        if (PauseMenu.gameIsPaused) return;

        Time.timeScale = Time.unscaledTime > time ? 1.0f : 0.0f;
    }
}
