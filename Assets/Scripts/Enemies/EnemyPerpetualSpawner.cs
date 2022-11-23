using System.Collections;
using UnityEngine;

public class EnemyPerpetualSpawner : MonoBehaviour
{
    [SerializeField] EnemyActions enemyPrefab;
    [SerializeField] float delay;

    EnemyActions instance;
    float lastAliveTime;

    private void Update()
    {
        if (Time.time > lastAliveTime + delay)
        {
            SpawnNewEnemy();
        }

        if (instance ? instance.gameObject.activeSelf : false)
        {
            lastAliveTime = Time.time;
        }
    }

    private void SpawnNewEnemy()
    {
        if (instance) Destroy(instance.gameObject);
        instance = Instantiate(enemyPrefab, transform.position, transform.rotation);
    }
}
