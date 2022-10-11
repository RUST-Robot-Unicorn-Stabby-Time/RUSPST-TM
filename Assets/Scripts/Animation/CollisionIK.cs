using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class CollisionIK : MonoBehaviour
{
    public Transform root;
    public Transform mid;
    public Transform tip;

    [Space]
    public Vector3 rootAngleOffset;
    public Vector3 midAngleOffset;

    public Vector3 tipOffset;

    [SerializeField] float rootAngle;
    [SerializeField] float midAngle;

    private void LateUpdate()
    {
        Vector3 tipPosition = tip.position - tipOffset;

        float rootMidLength = (mid.position - root.position).magnitude;
        float midTipLength = (tipPosition - mid.position).magnitude;
        float totalLength = rootMidLength + midTipLength;

        Vector3 legDirection = (tipPosition - root.position).normalized;
        Vector3 rootAxis = (root.rotation * Vector3.right).normalized;
        Vector3 midAxis = (mid.rotation * Vector3.right).normalized;

        Ray ray = new Ray(root.position, legDirection);
        if (Physics.Raycast(ray, out RaycastHit hit, totalLength))
        {
            float targetLength = hit.distance;
            rootAngle = GetAngleFromSides(rootMidLength, targetLength, midTipLength);
            midAngle = GetAngleFromSides(rootMidLength, midTipLength, targetLength);

            root.rotation = Quaternion.Euler(rootAxis * rootAngle + rootAngleOffset);
            mid.rotation = Quaternion.Euler(rootAxis * rootAngle + midAngleOffset);
            mid.rotation = Quaternion.Euler(midAxis * (rootAngle + (180.0f - midAngle)) + midAngleOffset);

            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
    }

    public float GetAngleFromSides (float a, float b, float c)
    {
        return Mathf.Acos((a * a + b * b - c * c)/(2 * a * b)) * Mathf.Rad2Deg;
    }
}
