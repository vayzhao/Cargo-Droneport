using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle the audio mixer used in game
/// <para>Contributor: Weizhao </para>
/// </summary>
public class AudioManager : MonoBehaviour
{
    private static AudioSource bgmChannel; // channel used to play bgm
    private static AudioSource sfxChannel; // channel used to play sfx

    /// <summary>
    /// Method to initialize bgm & sfx channels by creating
    /// child object and bind audio source to it
    /// </summary>
    void Initialize()
    {
        // TODO:
    }

    /// <summary>
    /// Method to setup bgm
    /// </summary>
    /// <param name="clip">the bgm</param>
    public static void PlayBgm(AudioClip clip)
    {
        // TODO:
    }

    /// <summary>
    /// Method to play a sfx clip through sfx channel
    /// </summary>
    /// <param name="clip">the sfx</param>
    public static void PlaySoundEffect(AudioClip clip)
    {
        // TODO:
    }

    /// <summary>
    /// Method to pause the bgm, it is called when the game
    /// is paused
    /// </summary>
    public static void PauseBGM()
    {
        // TODO:
    }
}