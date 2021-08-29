using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The blackboard that holds all static and constant variables
/// <para>Contributor: Weizhao </para>
/// </summary>
public class Blackboard : MonoBehaviour
{
    #region Constant Variables
    // default volume for sound effect / bgm audio channel
    public const float DEFAULT_VOLUME_SFX = 1f;
    public const float DEFAULT_VOLUME_BGM = 1f;

    // default value for mouse sensitivity
    public const float DEFAULT_MOUSE_SENSITIVITY = 1f;

    // default value for duration of the game
    public const float DEFAULT_LEVEL_TIMER = 90f;

    // minimum and maximum size of view for the main camera
    public const float MAINCAM_VIEW_MIN = 20f;
    public const float MAINCAM_VIEW_MAX = 100f;

    // distance to determine whether or not the drone has reach a dot
    public const float DISTANCE_DOT_REACH = 0.2f;

    // path drawing: to determine whether or not the distance is 
    // greate enough to insert a new dot
    public const float DISTANCE_DOT_GAP = 10f;

    #endregion

    #region Static Variables
    // game's current level
    public static int level = 1;

    // determine whether or not the game is paused
    public static bool isTheGamePaused = false;

    #endregion
}