using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHivemind : MonoBehaviour
{
    public GameObject target;
    public int maxConcurrentAttackers = 3;

    [Space]
    public Ring[] rings;

    List<EnemyBase> RegisteredEnemies { get; set; } = new List<EnemyBase>();

    private void Update()
    {
        int id = 0;
        foreach (EnemyBase enemy in RegisteredEnemies)
        {
            Ring ring = GetRing(id, out int ringId);
            float percent = ringId / (float)ring.enemyCount;
            float angle = percent * Mathf.PI * 2.0f;

            Vector3 offset = new Vector3(Mathf.Cos(angle), 0.0f, Mathf.Sin(angle)) * ring.radius;
            enemy.Target = target;
            enemy.TargetPosition = target.transform.position + offset;

            id++;
        }
    }

    private void OnDrawGizmos()
    {
        int id = 0;
        foreach (EnemyBase enemy in RegisteredEnemies)
        {
            Ring ring = GetRing(id, out int ringId);
            float percent = ringId / (float)ring.enemyCount;            
            float angle = percent * Mathf.PI * 2.0f;

            Vector3 offset = new Vector3(Mathf.Cos(angle), 0.0f, Mathf.Sin(angle)) * ring.radius;
            UnityEditor.Handles.Label(enemy.transform.position, $"ID: {id}\nRingId: {ringId}\nPercent: {percent}\nAngle: {angle}");

            id++;
        }
    }

    private Ring GetRing(int id, out int ringIndex)
    {
        ringIndex = id;
        foreach (Ring ring in rings)
        {
            if (ring.enemyCount <= ringIndex)
            {
                ringIndex -= ring.enemyCount;
            }
            else return ring;
        }
        return rings[rings.Length - 1];
    }

    public void Register (EnemyBase enemy)
    {
        RegisteredEnemies.Add(enemy);
    }

    public void Deregister (EnemyBase enemy)
    {
        RegisteredEnemies.Remove(enemy);
    }
}

[System.Serializable]
public struct Ring
{
    public float radius;
    public int enemyCount;
}

public enum EnemyDirective
{
    Attack,
    Hover,
}