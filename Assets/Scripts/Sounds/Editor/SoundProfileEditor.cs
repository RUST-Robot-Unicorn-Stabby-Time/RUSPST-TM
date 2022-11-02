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

        profile.clip = EditorGUILayout.ObjectField("clip", profile.clip, typeof(AudioClip), false) as AudioClip;
        EditorGUILayout.MinMaxSlider("Volume", ref profile.volumeRange.x, ref profile.volumeRange.y, 0.0f, 1.0f);
        EditorGUILayout.MinMaxSlider("Pitch", ref profile.pitchRange.x, ref profile.pitchRange.y, 0.0f, 1.0f);

        if (GUILayout.Button("Play"))
        {
            profile.Play();
        }
    }
}
