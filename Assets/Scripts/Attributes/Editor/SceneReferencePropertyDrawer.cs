using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SceneReference))]
public class SceneReferencePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(property.stringValue);
        sceneAsset = EditorGUILayout.ObjectField(sceneAsset, typeof(SceneAsset), false) as SceneAsset;
        property.stringValue = sceneAsset ? AssetDatabase.GetAssetPath(sceneAsset) : string.Empty;
    }
}
