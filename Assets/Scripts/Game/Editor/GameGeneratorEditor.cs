using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameGenerator))]
public class GameGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameGenerator generator = target as GameGenerator;
        if (GUILayout.Button("Generate Game"))
        {
            generator.GenerateGame();
        }

        if (GUILayout.Button("Force Load Next Level"))
        {
            generator.gameData.LoadNextLevel();
        }
    }
}
