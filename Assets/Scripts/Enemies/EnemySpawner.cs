using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnHeight;
    [SerializeField] float gravity;
    [SerializeField] float postDelay;

    [Space]
    [SerializeField] Transform root;
    [SerializeField] Animator animator;
    [SerializeField] string landFlag;

    [Space]
    [SerializeField] UnityEvent landEvent;

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
        SetEnemyState(false);

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
        animator.SetBool(landFlag, true);

        landEvent?.Invoke();

        float time = 0.0f;
        while (time < postDelay)
        {
            time += Time.deltaTime;
            yield return null;
        }

        SetEnemyState(true);
        enabled = false;
    }

    public void SetEnemyState (bool state)
    {
        TrySetComponentState<CharacterMovement>(state);
        TrySetComponentState<PlayerAnimator>(state);
        TrySetComponentState<BehaviourTree>(state);

        GetComponent<Rigidbody>().isKinematic = !state;
    }

    public void TrySetComponentState<T> (bool state) where T : Behaviour
    {
        if (TryGetComponent(out T result))
        {
            result.enabled = state;
        }
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem ("Actions/Swap Spawners for Enemies")]
    public static void SwapSpawnersWithEnemies ()
    {
        var prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Enemies/Medium Enemy.prefab");
        foreach (var spawner in FindObjectsOfType<EnemySpawner>())
        {
            var instance = UnityEditor.PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            instance.transform.SetParent(spawner.transform.parent);
            instance.transform.position = spawner.transform.position + Vector3.up;
            instance.transform.rotation = spawner.transform.rotation;

            DestroyImmediate(spawner.gameObject);
        }
    }
#endif
}
