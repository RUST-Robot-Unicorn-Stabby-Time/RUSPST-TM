using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ExitDoor))]
public class ExitDoorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ExitDoor door = target as ExitDoor;
        switch (door.action)
        {
            case ExitDoor.FinishAction.NextRoom:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("data"));
                break;
            case ExitDoor.FinishAction.GenerateGame:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("generator"));
                break;
            case ExitDoor.FinishAction.Custom:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("finishEvent"));
                break;
            default:
                break;
        }
    }
}
