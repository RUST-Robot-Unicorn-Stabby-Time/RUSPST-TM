using System.Collections.Generic;
using UnityEngine;

public class EnemyHivemind : MonoBehaviour
{
    public GameObject target;
    public int maxConcurrentAttackers = 3;

    [Space]
    public float waitOuterRadius;
    public float waitInnerRadius;

    List<EnemyBase> RegisteredEnemies { get; set; } = new List<EnemyBase>();

    private void Update()
    {
        foreach (EnemyBase enemy in RegisteredEnemies)
        {
            Vector3 vectorToPlayer = enemy.transform.position - target.transform.position;
            float distanceToPlayer = vectorToPlayer.magnitude;
            Vector3 directionToPlayer = vectorToPlayer / distanceToPlayer;

            if (distanceToPlayer > waitOuterRadius)
            {
                if ((enemy.TargetPosition - target.transform.position).magnitude > waitInnerRadius)
                {
                    float a = Random.value * Mathf.PI * 2.0f;
                    float d = Random.value * waitInnerRadius;
                    Vector3 offset = new Vector3(Mathf.Cos(a), 0.0f, Mathf.Sin(a)) * d;
                    enemy.TargetPosition = target.transform.position + offset;
                }
            }
        }
    }

    public void Register (EnemyBase enemy)
    {
        RegisteredEnemies.Add(enemy);
    }

    public void Deregister (EnemyBase enemy)
    {
        RegisteredEnemies.Remove(enemy);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, waitInnerRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, waitOuterRadius);
        Gizmos.color = Color.white;
    }
}

[System.Serializable]
public struct Ring
{
    public float radius;
    public int capacity;
    public bool allowedToAttack;
}

public enum EnemyDirective
{
    Attack,
    Hover,
}