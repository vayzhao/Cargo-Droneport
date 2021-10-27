using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;
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
    //public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public Slider volumeSlider;
    Resolution[] resolutions;

    void Start()
    {
        GetResolutions();

        var volume = PlayerPrefs.GetFloat("volume");
        if (volume == 0f)
            volume = 1f;
        volumeSlider.value = volume;

        //resolutions = Screen.resolutions;

        // Clear resolutions in dropdown
        //resolutionDropdown.ClearOptions();

        // Create a list of resolutions
        //List<string> options = new List<string>();

        //int currentResolutionIndex = 0;

        // Width X Height screen resolution
        //for (int i = 0; i < resolutions.Length; i++)
        //{
        //    string option = resolutions[i].width + " x " + resolutions[i].height;
        //    options.Add(option);

        //    if (resolutions[i].width == Screen.currentResolution.width &&
        //        resolutions[i].height == Screen.currentResolution.height)
        //    {
        //        currentResolutionIndex = i;
        //    }
        //}

        //resolutionDropdown.AddOptions(options);
        //resolutionDropdown.value = currentResolutionIndex;
        //resolutionDropdown.RefreshShownValue();
    }

    /// <summary>
    /// Fetch support resolution
    /// </summary>
    void GetResolutions()
    {
        // clear resolutions in dropdown
        resolutionDropdown.ClearOptions();

        // get the full-screen resolutions support by the monitor (with no duplicate)
        resolutions = Screen.resolutions.Select(x => new Resolution{width = x.width,height=x.height}).OrderBy(x => x.width).Distinct().ToArray();

        // apply resolution data into the dropdown panel
        resolutionDropdown.AddOptions(resolutions.Select(x => $"{x.width} x {x.height}").ToList());

        Invoke("UpdateDropdown", Time.deltaTime * 2);
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
        PlayerPrefs.SetFloat("volume", volume);
        FindObjectOfType<AudioSource>().volume = volume;

        //audioMixer is no need if we have 1 channel only
        //audioMixer.SetFloat("volume", volume);
    }

    // Change quality of graphics
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Enable/Disable fullscreen
    public void SetFullscreen(bool isFullscreen)
    {
        // automatically change to the best resolution when switching to full screen
        if (isFullscreen)
            SetResolution(resolutions.Length - 1);

        Screen.fullScreen = isFullscreen;

        Invoke("UpdateDropdown", Time.deltaTime * 2);
    }

    private void UpdateDropdown()
    {
        // refresh dropdown state
        resolutionDropdown.enabled = !Screen.fullScreen;

        // refresh dropdown color
        var img = resolutionDropdown.gameObject.GetComponent<Image>();
        img.color = resolutionDropdown.enabled ? Color.white : Color.gray;

        // refresh value in resolution dropdown
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
            {
                resolutionDropdown.value = i;
                resolutionDropdown.RefreshShownValue();
                break;
            }
        }
    }
}

