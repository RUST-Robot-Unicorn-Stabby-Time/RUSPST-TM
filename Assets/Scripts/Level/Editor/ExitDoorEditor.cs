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
                door.data = EditorGUILayout.ObjectField(door.data , typeof(GameData), false) as GameData;
                break;
            case ExitDoor.FinishAction.GenerateGame:

                door.generator = EditorGUILayout.ObjectField(door.generator, typeof(GameGenerator), false) as GameGenerator;
                break;
            case ExitDoor.FinishAction.Custom:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("finishEvent"));
                break;
            default:
                break;
        }
    }
}
