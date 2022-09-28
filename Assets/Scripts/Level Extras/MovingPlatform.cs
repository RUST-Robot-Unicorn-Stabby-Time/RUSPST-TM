using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    const float BasicallyZero = 0.00001f;

    public TargetPoint[] points;
    public int index;

    float localTime;
    new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (points == null) enabled = false;
        if (points.Length == 0) enabled = false;

        if (!enabled)
        {
            Debug.Log("Moving Platform is missing points", this);
        }
    }

    private void FixedUpdate()
    {
        TargetPoint point = points[index];
        TargetPoint next = points[(index + 1) % points.Length];

        if (localTime < point.moveTime)
        {
            Vector3 target = Vector3.Lerp(point.position, next.position, point.curve.Evaluate(localTime / point.moveTime));
            rigidbody.velocity = (target - rigidbody.position) / Time.deltaTime;
        }
        else if (localTime < point.moveTime + point.holdTime)
        {
            rigidbody.position = next.position;
            rigidbody.velocity = Vector3.zero;
        }
        else
        {
            index = (index + 1) % points.Length;
            localTime -= point.moveTime + point.holdTime;
        }

        localTime += Time.deltaTime;
    }

    [System.Serializable]
    public class TargetPoint
    {
        public Vector3 position = Vector3.zero;
        public float moveTime = 5.0f;
        public float holdTime = 2.0f;
        public AnimationCurve curve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);
    }
}
