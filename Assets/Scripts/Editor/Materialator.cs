using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Materialator : EditorWindow
{
    [MenuItem("Window/Tools/Materialator")]
    public static void Open()
    {
        CreateWindow<Materialator>("Materialator");
    }

    Material mat;
    [SerializeField] GameObject[] prefabs = new GameObject[0];

    private void OnGUI()
    {
        mat = EditorGUILayout.ObjectField("Material", mat, typeof(Material), false) as Material;

        var serializedObject = new SerializedObject(this);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("prefabs"));

        if (mat && prefabs.Length > 0)
        {
            if (GUILayout.Button("Apply"))
            {
                var renderers = new List<Renderer>();
                foreach (var prefab in prefabs)
                {
                    renderers.AddRange(prefab.GetComponentsInChildren<Renderer>());
                }
                Undo.RecordObjects(renderers.ToArray(), "Materialator Application");

                foreach (var renderer in renderers)
                {
                    renderer.material = mat;
                }
            }
        }
    }
}
