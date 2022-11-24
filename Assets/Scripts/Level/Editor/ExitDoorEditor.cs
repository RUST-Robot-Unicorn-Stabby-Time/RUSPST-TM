using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(ExitDoor))]
public class ExitDoorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enableOnExit"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("distance"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("action"));

        ExitDoor door = target as ExitDoor;
        switch (door.action)
        {
            case ExitDoor.FinishAction.NextRoom:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("sceneLoader"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("nextRoomName"));
                break;
            case ExitDoor.FinishAction.Custom:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("finishEvent"));
                break;
            default:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
