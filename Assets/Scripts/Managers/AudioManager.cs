using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle the audio mixer used in game
/// <para>Contributor: Weizhao </para>
/// </summary>
public class AudioManager : MonoBehaviour
{
    public AudioSource bgmSource; // channel used to play bgm
    public AudioSource sfxSource; // channel used to play sfx

    public AudioClip sfx_pickUp;
    public AudioClip sfx_place;
    public AudioClip sfx_bomb;

    void Start()
    {
        var volume = PlayerPrefs.GetFloat("volume");
        bgmSource.volume = volume;
        sfxSource.volume = volume;

    }

    public void PlayPickUpSound()
    {
        sfxSource.PlayOneShot(sfx_pickUp);
    }
    public void PlayPlaceSound()
    {
        sfxSource.PlayOneShot(sfx_place);
    }

    public void PlayBombSound()
    {
        sfxSource.PlayOneShot(sfx_bomb);
    }

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