using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MovingPlatform))]
public class MovingPlatformEditor : Editor
{
    int previousIndex;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Application.isPlaying) return;

        MovingPlatform target = base.target as MovingPlatform;

        target.index = Mathf.Clamp(target.index, 0, target.points.Length - 1);
        if (target.index != previousIndex)
        {
            target.transform.position = target.points[target.index].position;
        }
        else
        {
            target.points[target.index].position = target.transform.position;
        }

        previousIndex = target.index;
    }

    private void OnSceneGUI()
    {
        MovingPlatform target = base.target as MovingPlatform;
        Handles.color = new Color(1.0f, 150.0f / 255.0f, 13.0f / 255.0f, 1.0f);

        for (int i = 0; i < target.points.Length; i++)
        {
            Handles.DrawLine(target.points[i].position, target.points[(i + 1) % target.points.Length].position);
            Handles.DrawWireCube(target.points[i].position, Vector3.one * 0.2f);
        }

        Handles.color = Color.white;
    }
}
