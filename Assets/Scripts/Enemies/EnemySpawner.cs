using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnHeight;
    [SerializeField] float gravity;
    [SerializeField] float postDelay;

    [Space]
    [SerializeField] Transform root;
    [SerializeField] Animator animator;
    [SerializeField] string landAnimation;

    [Space]
    [SerializeField] GameObject enemyPrefab;

    Vector3? rootOffset;

    private void OnEnable()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void LateUpdate()
    {
        if (rootOffset.HasValue)
        {
            root.position = rootOffset.Value;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        float height = spawnHeight;
        float velocity = 0.0f;

        while (height > 0.0f)
        {
            rootOffset = transform.position + Vector3.up * height;

            velocity += gravity * Time.deltaTime;
            height += velocity * Time.deltaTime;
            yield return null;
        }

        rootOffset = null;
        root.transform.position = transform.position;
        animator.Play(landAnimation);

        float time = 0.0f;
        while (time < postDelay)
        {
            time += Time.deltaTime;
            yield return null;
        }

        Instantiate(enemyPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
