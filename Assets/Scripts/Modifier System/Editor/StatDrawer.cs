using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Stat))]
public class StatDrawer : PropertyDrawer
{
    const int fields = 3;
    const float spacing = 2.0f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position.height -= spacing * (fields - 1);
        position.height /= fields;

        EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel++;

        position.y += position.height + spacing;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("key"));

        position.y += position.height + spacing;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("baseValue"));

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) * fields + spacing * (fields - 1);
    }
}
