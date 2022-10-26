using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class BonerHelper
{
    [MenuItem("Tools/Auto Generate Rig Colliders")]
    public static void AutogenerateRigColliders ()
    {
        foreach (var selection in Selection.gameObjects)
        {
            if (selection.transform.childCount != 1) continue;

            Vector3 a = selection.transform.position;
            Vector3 b = selection.transform.GetChild(0).position;

            Vector3 offset = (a - b) / 2.0f;
            float distance = Vector3.Dot(selection.transform.up, offset) * 2.0f;
            offset = selection.transform.up * distance;

            CapsuleCollider collider = selection.GetOrAddComponent<CapsuleCollider>();
            collider.center = offset;
            collider.height = Mathf.Abs(distance);
            collider.radius = 0.02f;
        }
    }
}
