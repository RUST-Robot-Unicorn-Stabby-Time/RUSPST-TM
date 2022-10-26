using System.Collections.Generic;
using UnityEngine;

public class EnemyHivemind : MonoBehaviour
{
    public GameObject target;

    List<EnemyBase> RegisteredEnemies { get; set; } = new List<EnemyBase>();

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
    public int capacity;
    public bool allowedToAttack;
}

public enum EnemyDirective
{
    Attack,
    Hover,
}