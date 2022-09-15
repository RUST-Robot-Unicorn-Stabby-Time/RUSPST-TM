using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Statboard))]
public class StatboardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Application.isPlaying)
        {
            Statboard statboard = target as Statboard;

            EditorGUILayout.LabelField("Statboard Readout:");

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel++;

            foreach (var pair in statboard.finalValues)
            {
                EditorGUILayout.LabelField(pair.Key, pair.Value.ToString());
            }

            EditorGUI.indentLevel = indent;
        }
        else
        {
            EditorGUILayout.LabelField("Statboard Readout is only avalable in Playmode.");
        }
    }
}
