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
                var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(door.nextRoomName);
                sceneAsset = EditorGUILayout.ObjectField(sceneAsset, typeof(SceneAsset), false) as SceneAsset;
                door.nextRoomName = sceneAsset ? AssetDatabase.GetAssetPath(sceneAsset) : string.Empty;
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
