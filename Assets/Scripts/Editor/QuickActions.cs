using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine;

public class QuickActions : EditorWindow
{
    [MenuItem("Tools/Quick Actions")]
    public static void Open ()
    {
        CreateWindow<QuickActions>("Quick Actions");
    }

    private void OnGUI()
    {
        GUIStyle s = new GUIStyle();
        s.richText = true;
        s.normal.textColor = Color.white;

        EditorGUILayout.LabelField("<b>Scenes</b>", s);

        foreach (var path in Directory.GetFiles(Application.dataPath + "/Scenes/Final/", "*.unity", SearchOption.AllDirectories))
        {
            string relPath = path.Replace(Application.dataPath, "Assets");
            var asset = AssetDatabase.LoadAssetAtPath(relPath, typeof(SceneAsset));
            if (asset is SceneAsset)
            {
                if (GUILayout.Button($"Load {asset.name}"))
                {
                    EditorSceneManager.OpenScene(relPath);
                }
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("<b>Prefabs</b>", s);

        foreach (var path in SignificantPrefabs)
        {
            var asset = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
            if (GUILayout.Button($"Open {asset.name}"))
            {
                AssetDatabase.OpenAsset(asset);
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("<b>Misc</b>");
        if (GUILayout.Button("Play Game from Start"))
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Final/LobbyRoom");
            EditorApplication.EnterPlaymode();
        }

        if (GUILayout.Button("Spawn Debug Actions"))
        {
            var asset = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Level/Debug Actions.prefab", typeof(GameObject));
            PrefabUtility.InstantiatePrefab(asset);
        }
    }

    readonly List<string> SignificantPrefabs = new List<string>()
    {
        "Assets/Prefabs/DR Ruckus/RUCKUS Prefab.prefab",
        "Assets/Prefabs/Enemies/Medium Enemy.prefab",
    };
}
