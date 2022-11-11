using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawner : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] GameObject[] pool;
    [SerializeField] int maxInstances;

    List<GameObject> instances;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
    }
}
