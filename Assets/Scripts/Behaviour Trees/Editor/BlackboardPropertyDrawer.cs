using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Blackboard))]
public class BlackboardPropertyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Blackboard blackboard = property.objectReferenceValue as Blackboard;

        if (blackboard)
        {
            return (blackboard.values.Count + 3) * 17.0f;
        }
        else
        {
            return 17.0f;
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.height = 17.0f;

        Blackboard blackboard = property.objectReferenceValue as Blackboard;

        GUIStyle s = new GUIStyle();
        s.richText = true;
        s.normal.textColor = Color.white;

        if (blackboard == null)
        {
            if (GUI.Button(position, "Create new Blackboard"))
            {
                property.objectReferenceValue = ScriptableObject.CreateInstance<Blackboard>();
            }
        }
        else
        {
            EditorGUI.LabelField(position, "<b>Blackboard</b>", s); position.y += 17.0f;
            EditorGUI.indentLevel++;

            if (blackboard.values.Count == 0)
            {
                EditorGUI.LabelField(position, "Blackboard is empty."); position.y += 17.0f;
            }
            else foreach (var pair in blackboard.values)
            {
                EditorGUI.LabelField(position, $"<b>{pair.Key}:</b> {pair.Value}", s); position.y += 17.0f;
            }

            if (GUI.Button(position, "Delete Blackboard"))
            {
                property.objectReferenceValue = null;
            }
            position.y += 17.0f;
        }
    }
}
