using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// This is the settings menu to change volume, graphics and window.
/// In Unity3D, need to create:
/// Canvas (COMPLETED)
/// Volume text and slider (COMPLETED)
/// Resolution text and dropdown (COMPLETED)
/// Graphics text and dropdown (COMPLETED)
/// Fullscreen toggle (COMPLETED)
/// </summary>

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        // Clear resolutions in dropdown
        resolutionDropdown.ClearOptions();

        // Create a list of resolutions
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        // Width X Height screen resolution
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }



        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    //Change screen resolution
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Change volume
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    // Change quality of graphics
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Enable/Disable fullscreen
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}

