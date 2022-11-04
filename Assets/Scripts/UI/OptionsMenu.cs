using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class OptionsMenu : MonoBehaviour
{
    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    [Header("Sliders")]
    public Slider fovSlider;
    public Slider SensetivitySlider;

    [Header("Audio")]
    public Slider masterVolume;
    public Slider musicVolume;
    public Slider fxVolume;

    [Header("Text")]
    public TMP_Text fovText;
    public TMP_Text sensetivityText;
    public TMP_Text masterVolumeText;
    public TMP_Text musicVolumeText;
    public TMP_Text SFXVolume;

    [Header("Resolution Dropdown")]
    public TMP_Dropdown resolutionDropdown;

    [Header("Cinemachine Camera")]
    public CinemachineVirtualCamera cinemachine;

    //Resolution List
    public static readonly Vector2Int[] Resolutions = new Vector2Int[]
    {
        new Vector2Int (1280, 720),
        new Vector2Int (1920, 1080),
        new Vector2Int (2560, 1440),
    };

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

        //Display current settings
        fovText.text = Mathf.Round(OptionsData.instance.fov).ToString();
        sensetivityText.text = Mathf.Round(100 * OptionsData.instance.sensitivity).ToString();
        masterVolumeText.text = Mathf.Round(100 * OptionsData.instance.maxVolume).ToString();
        musicVolumeText.text = (Mathf.Round(100 * OptionsData.instance.musicVolume)).ToString();
        SFXVolume.text = (Mathf.Round(100 * OptionsData.instance.sfxFloat)).ToString();

        //Display Slider
        fovSlider.value = OptionsData.instance.fov;
        SensetivitySlider.value = OptionsData.instance.sensitivity;
        masterVolume.value = OptionsData.instance.maxVolume;
        musicVolume.value = OptionsData.instance.musicVolume;
        fxVolume.value = OptionsData.instance.sfxFloat;
    }

    public void Update()
    {
        //Display current settings
        fovText.text = Mathf.Round(fovSlider.value).ToString();
        sensetivityText.text = Mathf.Round(100 * SensetivitySlider.value).ToString();
        masterVolumeText.text = Mathf.Round(100 * masterVolume.value).ToString();
        musicVolumeText.text = Mathf.Round(100 * musicVolume.value).ToString();
        SFXVolume.text = Mathf.Round(100 * fxVolume.value).ToString();

        //Display Slider
        AudioListener.volume = masterVolume.value;

        //Change Setting
        OptionsData.instance.fov = fovSlider.value;
        OptionsData.instance.sensitivity = SensetivitySlider.value;
        OptionsData.instance.maxVolume = masterVolume.value;
        OptionsData.instance.musicVolume = musicVolume.value;
        OptionsData.instance.sfxFloat = fxVolume.value;
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
}