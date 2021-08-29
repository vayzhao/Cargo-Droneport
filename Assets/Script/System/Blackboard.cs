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
    [Tooltip("default volume for sound effect")]
    public const float DEFAULT_VOLUME_SFX = 1f;

    [Tooltip("default volume for bgm")]
    public const float DEFAULT_VOLUME_BGM = 1f;

    [Tooltip("default value for mouse sensitivity")]
    public const float DEFAULT_MOUSE_SENSITIVITY = 1f;

    [Tooltip("default value for duration of the game")]
    public const float DEFAULT_LEVEL_TIMER = 90f;

    [Tooltip("minimum size of view for the main camera")]
    public const float MAINCAM_VIEW_MIN = 20f;
    [Tooltip("maximum size of view for the main camera")]
    public const float MAINCAM_VIEW_MAX = 100f;

    [Tooltip("distance to determine whether or not the drone has reach a dot")]
    public const float DISTANCE_DOT_REACH = 0.2f;

    [Tooltip("to determine whether or not the distance is great " +
        "enough to insert a new pathing dot when drawing paths")]
    public const float DISTANCE_DOT_GAP = 10f;

    [Tooltip("max height of the pathing network")]
    public const float MAX_MAP_HEIGHT = 50f;

    [Tooltip("speed ratio for a drone to turn")]
    public const float SPEED_RATIO_ROTATION = 0.2f;

    [Tooltip("speed ratio for a drone to takeoff/land")]
    public const float SPEED_RATIO_VERTICLE = 0.3f;

    #endregion

    #region Static Variables
    [Tooltip("game's current level")]
    public static int level = 1;

    [Tooltip("determine whether or not the game is paused")]
    public static bool isTheGamePaused = false;

    [Tooltip("a layer mask for ray cast to find the invisible ceiling")]
    public static int ceilingMask = LayerMask.GetMask("PathHeight");

    [Tooltip("a transform to hold all spawning cache")]
    public static Transform spawnHolder;

    #endregion
}