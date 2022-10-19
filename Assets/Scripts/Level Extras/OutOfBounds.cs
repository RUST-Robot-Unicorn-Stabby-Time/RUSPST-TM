using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public float damage;
    public List<Transform> respawnPoints;

    private void FixedUpdate()
    {
        foreach (var damagable in FindObjectsOfType<Health>())
        {
            float dot = Vector3.Dot(damagable.transform.position - transform.position, transform.forward);
            if (dot < 0.0f)
            {
                damagable.Damage(new DamageArgs(gameObject, damage));
                damagable.transform.position = GetSpawnPoint();
            }
        }
    }

    private Vector3 GetSpawnPoint()
    {
        if (respawnPoints?.Count == 0) return Vector3.zero;

        return respawnPoints[Random.Range(0, respawnPoints.Count)].position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.1f);
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(Vector3.zero, new Vector3(100.0f, 100.0f, 0.0f));
        Gizmos.color = Color.white;
        Gizmos.matrix = Matrix4x4.identity;
    }
}
