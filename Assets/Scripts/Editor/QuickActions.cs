using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine;

[InitializeOnLoad]
public class QuickActions : EditorWindow
{
    [MenuItem("Tools/Quick Actions")]
    public static void Open()
    {
        CreateWindow<QuickActions>("Quick Actions");
    }

    Vector2 scrollPos;

    [MenuItem("Actions/Zero Group")]
    public static void ZeroGroup ()
    {
        foreach (var sel in Selection.gameObjects)
        {
            Vector3 offset = sel.transform.position;
            foreach (Transform child in sel.transform)
            {
                child.position += offset;
            }
            sel.transform.position = Vector3.zero;
        }
    }

    private void OnGUI()
    {
        GUIStyle s = new GUIStyle();
        s.richText = true;
        s.normal.textColor = Color.white;

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        EditorGUILayout.LabelField("<b>Scenes</b>", s);

        foreach (var path in Directory.GetFiles(Application.dataPath + "/Scenes/Final/", "*.unity", SearchOption.AllDirectories))
        {
            string relPath = path.Replace(Application.dataPath, "Assets");
            var asset = AssetDatabase.LoadAssetAtPath(relPath, typeof(SceneAsset));
            if (asset is SceneAsset)
            {
                GUIContent content = EditorGUIUtility.IconContent("d_SceneAsset Icon");
                content.text = $"Load {asset.name}";

                if (GUILayout.Button(content, GUILayout.MaxHeight(30.0f)))
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        EditorSceneManager.OpenScene(relPath);
                    }
                }
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("<b>Prefabs</b>", s);

        foreach (var path in SignificantPrefabs)
        {
            var asset = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));

            GUIContent content = EditorGUIUtility.IconContent("d_Prefab Icon");
            content.text = $"Open {asset.name}";

            if (GUILayout.Button(content, GUILayout.MaxHeight(30.0f)))
            {
                AssetDatabase.OpenAsset(asset);
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("<b>Misc</b>", s);

        {
            GUIContent content = EditorGUIUtility.IconContent("d_PlayButton");
            content.text = "Play Game from Start";

            if (GUILayout.Button(content, GUILayout.MaxHeight(30.0f)))
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Final/EpilepsyScreen.unity");
                EditorApplication.EnterPlaymode();
            }
        }

        {
            GUIContent content = EditorGUIUtility.IconContent("d_DebuggerAttached");
            content.text = "Spawn Debug Actions Prefab";

            if (GUILayout.Button(content, GUILayout.MaxHeight(30.0f)))
            {
                var asset = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Level/Debug Actions.prefab", typeof(GameObject));
                PrefabUtility.InstantiatePrefab(asset);
            }
        }

        {
            GUIContent content = EditorGUIUtility.IconContent("d_BuildSettings.Web.Small");
            content.text = "Open File Registry";

            if (GUILayout.Button(content, GUILayout.MaxHeight(30.0f)))
            {
                System.Diagnostics.Process.Start("https://trello.com/b/oytF4wo5/ruckus-file-registry");
            }
        }

        EditorGUILayout.EndScrollView();
    }

    readonly List<string> SignificantPrefabs = new List<string>()
    {
        "Assets/Prefabs/Player/RUCKUS Prefab.prefab",
        "Assets/Prefabs/Enemies/Medium Enemy.prefab",
    };
}
