using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnyBoxScript : MonoBehaviour
{
    [SerializeField] float minHeight = -10.0f;
    [SerializeField] Bounds spawnBounds = new Bounds(new Vector3(30.0f, 25.0f, -64.0f), new Vector3(15.93f, 0.0f, 77.0f));

    new Rigidbody rigidbody;
    Vector3 pos;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        pos = rigidbody.position;
    }

    private void FixedUpdate()
    {
        if (rigidbody.position.y < minHeight)
        {
            var offset = new Vector3
            {
                x = Random.Range(-spawnBounds.size.x, spawnBounds.size.x),
                y = Random.Range(-spawnBounds.size.y, spawnBounds.size.y),
                z = Random.Range(-spawnBounds.size.z, spawnBounds.size.z),
            };
            rigidbody.position = offset + spawnBounds.center;
            rigidbody.velocity = Vector3.zero;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnBounds.center, spawnBounds.size);
    }
}
