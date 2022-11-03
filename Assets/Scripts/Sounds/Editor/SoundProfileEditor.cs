using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundProfile))]
public class SoundProfileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SoundProfile profile = target as SoundProfile;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("clips"));
        
        EditorGUILayout.MinMaxSlider("Volume", ref profile.volumeRange.x, ref profile.volumeRange.y, 0.0f, 1.0f);
        EditorGUILayout.BeginHorizontal();
        profile.volumeRange.x = EditorGUILayout.FloatField("min", profile.volumeRange.x);
        profile.volumeRange.y = EditorGUILayout.FloatField("max", profile.volumeRange.y);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.MinMaxSlider("Pitch", ref profile.pitchRange.x, ref profile.pitchRange.y, 0.0f, 2.0f);
        EditorGUILayout.BeginHorizontal();
        profile.pitchRange.x = EditorGUILayout.FloatField("min", profile.pitchRange.x);
        profile.pitchRange.y = EditorGUILayout.FloatField("max", profile.pitchRange.y);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Play"))
        {
            profile.Play();
        }
    }
}
