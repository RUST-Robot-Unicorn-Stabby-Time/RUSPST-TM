using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class OptionsData
{
    public float fov = 90;
    public float sensitivity = 0.3f;

    public float maxVolume = 1;
    public float musicVolume = 1;
    public float sfxFloat = 1;

    public int xResolution = 1280;
    public int yResolution = 720;

    public int fullscreen = (int)FullScreenMode.Windowed;

    public static string OptionsPath => Application.persistentDataPath + "/options.json";

    public static OptionsData instance;

    public static void LoadOptions()
    {
        if (File.Exists(OptionsPath))
        {
            string data = File.ReadAllText(OptionsPath);
            instance = JsonUtility.FromJson<OptionsData>(data);
        }
        else
        {
            instance = new OptionsData();
        }
    }

    public static void SaveOptions()
    {
        if (instance == null)
        {
            instance = new OptionsData();
        }
        string data = JsonUtility.ToJson(instance);
        File.WriteAllText(OptionsPath, data);
    }
}