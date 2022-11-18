using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnyBoxScript : MonoBehaviour
{
    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;

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
            rigidbody.position = new Vector3(rigidbody.position.x, maxHeight, rigidbody.position.z);
            rigidbody.velocity = Vector3.zero;
        }
    }
}
