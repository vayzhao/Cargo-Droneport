using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to adjust option values
/// <para>Contributor: Grace </para>
/// </summary>
public class Option : MonoBehaviour
{
    public Slider sfxSlider;              // the slider for sound effect volume
    public Slider bgmSlider;              // the slider for bgm volume
    public Slider mouseSensiSlider;       // the slider for mouse sensitivity

    public static float volumeForSfx;     // the value for sound effect volume
    public static float volumeForBgm;     // the value for bgm volume
    public static float mouseSensitivity; // the value for mouse sensitivity
    
    /// <summary>
    /// In this method, you'll need to change values for 'volumeForSfx',
    /// 'volumeForBgm' & 'mouseSensitivity' back to the default value.
    /// Also update their slider's value 
    /// Default values are defined in Asset->Script->System->Blackboard.cs
    /// </summary>
    public void ResetOption()
    {
        // TODO: 
    }

    /// <summary>
    /// In this method, you'll need to apply the changes to the game by saving
    /// sliders values into the associated variables. 
    /// e.g volumeForSfx = sfxSlider.value;
    /// </summary>
    public void ConfirmOption()
    {
        // TODO:
    }
}