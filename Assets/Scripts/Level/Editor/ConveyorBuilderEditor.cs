using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ConveyorBuilder))]
public class ConveyorBuilderEditor : Editor
{
    private void OnSceneGUI()
    {
        ConveyorBuilder builder = target as ConveyorBuilder;

        EditorGUI.BeginChangeCheck();
        float size = HandleUtility.GetHandleSize(builder.transform.position);
        float newLength = Handles.ScaleValueHandle(builder.Length, builder.transform.position + builder.transform.forward * size * 1.5f, builder.transform.rotation, size, Handles.CubeHandleCap, 0.25f);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(builder, "Change Conveyor Length");
            builder.Length = newLength;
        }

        builder.Bake();
    }
}
