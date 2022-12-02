using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using System;

public class OptionsMenu : MonoBehaviour
{
    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    [Header("Sliders")]
    public Slider fovSlider;
    public Slider sensetivitySlider;

    [Header("Audio")]
    public Slider masterVolume;
    public Slider musicVolume;
    public AudioMixer musicMixer;

    [Header("Text")]
    public TMP_InputField fovText;
    public TMP_InputField sensetivityText;
    public TMP_InputField masterVolumeText;
    public TMP_InputField musicVolumeText;

    [Header("Resolution Dropdown")]
    public TMP_Dropdown resolutionDropdown;

    //Resolution List
    public static readonly Vector2Int[] Resolutions = new Vector2Int[]
    {
        new Vector2Int (1920, 1080)
    };

    public OptionsPairing[] options;

    private void Awake()
    {
        options = new OptionsPairing[]
        {
            new OptionsPairing(fovSlider, fovText, new Vector2(60, 100), new Vector2(60, 100), v => OptionsData.instance.fov = v, () => OptionsData.instance.fov),
            new OptionsPairing(sensetivitySlider, sensetivityText, new Vector2(0, 100), new Vector2(0, 1), v => OptionsData.instance.sensitivity = v, () => OptionsData.instance.sensitivity),
            new OptionsPairing(masterVolume, masterVolumeText, new Vector2(0, 100), new Vector2(0, 1), v => OptionsData.instance.maxVolume = v, () => OptionsData.instance.maxVolume),
            new OptionsPairing(musicVolume, musicVolumeText, new Vector2(0, 100), new Vector2(-80, 0), v => OptionsData.instance.musicVolume = v, () => OptionsData.instance.musicVolume),
        };
    }

    //Open Options
    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    //Save and load data
    public void OnEnable()
    {
        OptionsData.LoadOptions();
        resolutionDropdown.ClearOptions();
        List<string> resolutionLabels = new List<string>();

        foreach (Vector2Int resolution in Resolutions)
        {
            resolutionLabels.Add($"{resolution.x} x {resolution.y}");
        }

        resolutionDropdown.AddOptions(resolutionLabels);

        foreach (var o in options)
        {
            o.field.text = Mathf.Round(Remap(o.GetCallback(), o.outRange, o.inRange)).ToString();
            o.slider.value = Remap(o.GetCallback(), o.outRange, o.inRange);

            o.slider.onValueChanged.AddListener((v) => OnOptionChanged(o, v));
            o.field.onValueChanged.AddListener((v) => OnOptionChanged(o, float.Parse(v)));
        }
    }

    private void OnOptionChanged(OptionsPairing option, float value)
    {
        option.slider.value = value;
        option.field.text = Mathf.Round(value).ToString();
        option.SetCallback(Remap(value, option.inRange, option.outRange));
    }

    public void Update()
    {
        AudioListener.volume = OptionsData.instance.maxVolume;
        musicMixer.SetFloat("MusicVol", OptionsData.instance.musicVolume);
    }

    public void OnDisable()
    {
        OptionsData.SaveOptions();
    }

    //Set Resolution
    public void SetResolution(int index)
    {
        Screen.SetResolution(Resolutions[index].x, Resolutions[index].y, Screen.fullScreenMode);
    }

    public class OptionsPairing
    {
        public Slider slider;
        public TMP_InputField field;

        public Vector2 inRange;
        public Vector2 outRange;

        public System.Action<float> SetCallback;
        public System.Func<float> GetCallback;

        public OptionsPairing(Slider slider, TMP_InputField field, Vector2 inRange, Vector2 outRange, Action<float> setCallback, Func<float> getCallback)
        {
            this.slider = slider;
            this.field = field;
            this.inRange = inRange;
            this.outRange = outRange;
            SetCallback = setCallback;
            GetCallback = getCallback;
        }
    }

    public float Remap (float i, Vector2 f, Vector2 t)
    {
        float p = Mathf.InverseLerp(f.x, f.y, i);
        return Mathf.Lerp(t.x, t.y, p);
    }
}