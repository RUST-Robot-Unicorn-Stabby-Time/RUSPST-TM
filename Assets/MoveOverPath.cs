using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOverPath : MonoBehaviour
{
    [SerializeField] Transform pathParent;
    [SerializeField] float moveSpeed;
    [SerializeField] float velocityRecentering;
    [SerializeField] float velocityDamping;

    [Space]
    [SerializeField] Vector3 xPostScaling;
    [SerializeField] Vector3 yPostScaling;
    [SerializeField] Vector3 zPostScaling;

    Vector3 swingPosition;
    Vector3 swingVelocity;

    float distance = 0.0f;

    private void Start()
    {
        distance = GetDistanceFromPoint(transform.position);
    }

    private void FixedUpdate()
    {
        swingVelocity -= swingPosition * velocityRecentering * Time.deltaTime;
        swingVelocity -= swingVelocity * velocityDamping * Time.deltaTime;

        swingPosition += swingVelocity * Time.deltaTime;
    }

    private void Update()
    {
        float totalDistance = GetTotalDistance();
        distance += moveSpeed * Time.deltaTime;
        bool resetDelta = false;

        if (distance > totalDistance)
        {
            distance %= totalDistance;
            resetDelta = true;
        }

        Vector3 target = GetPointFromDistance(distance);
        Vector3 delta = target - transform.position;
        transform.position = target;
        
        if (resetDelta)
        {
            swingVelocity = Vector3.zero;
        }
        else
        {
            swingVelocity -= delta / Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(swingPosition.x * xPostScaling + swingPosition.y * yPostScaling + swingPosition.z * zPostScaling);
    }

    public float GetDistanceFromPoint(Vector3 point)
    {
        float workingDistance = 0.0f;

        for (int i = 0; i < pathParent.childCount - 1; i++)
        {
            Vector3 a = pathParent.GetChild(i).position;
            Vector3 b = pathParent.GetChild(i + 1).position;
            Vector3 pDirection = (b - a).normalized;

            float dot = Vector3.Dot(pDirection, (point - a).normalized);
            if (dot > 0.99f)
            {
                float dist = Vector3.Dot(pDirection, (point - a));
                if (dist < (b - a).magnitude) return dist + workingDistance;
            }

            workingDistance += (b - a).magnitude;
        }

        return workingDistance;
    }

    public Vector3 GetPointFromDistance (float distance)
    {
        float totalDistance = GetTotalDistance();
        distance %= totalDistance;

        for (int i = 0; i < pathParent.childCount - 1; i++)
        {
            Vector3 a = pathParent.GetChild(i).position;
            Vector3 b = pathParent.GetChild(i + 1).position;

            float abDistance = (a - b).magnitude;
            if (distance < abDistance)
            {
                float percent = distance / abDistance;
                return Vector3.Lerp(a, b, percent);
            }

            distance -= abDistance;
        }

        return pathParent.GetChild(pathParent.childCount - 1).position;
    }

    private float GetTotalDistance()
    {
        float totalDistance = 0.0f;

        for (int i = 0; i < pathParent.childCount - 1; i++)
        {
            Vector3 a = pathParent.GetChild(i).position;
            Vector3 b = pathParent.GetChild(i + 1).position;

            totalDistance += (a - b).magnitude;
        }
        return totalDistance;
    }

    private void OnDrawGizmosSelected()
    {
        if (!pathParent) return;

        Gizmos.color = Color.yellow;
        
        float distance = GetDistanceFromPoint(transform.position);
        Gizmos.DrawSphere(GetPointFromDistance(distance), 0.1f);

        Gizmos.color = Color.white;
    }

    private void OnDrawGizmos()
    {
        if (!pathParent) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < pathParent.childCount; i++)
        {
            if (i < pathParent.childCount - 1)
            {
                Gizmos.DrawLine(pathParent.GetChild(i).position, pathParent.GetChild(i + 1).position);
            }

            Gizmos.DrawSphere(pathParent.GetChild(i).position, 0.05f);
        }

        Gizmos.color = Color.white;
    }
}
