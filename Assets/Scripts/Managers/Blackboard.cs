using System;
using System.Collections.Generic;
using UnityEngine;

#region Enums
public enum DroneFSM
{
    Move,
    Takeoff,
    Land,
    Repair
}
public enum LandPurpose
{
    Collect,
    Place
}

#endregion
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

    [Tooltip("distance to determine whether or not the drone is close to objective")]
    public const float DISTANCE_OBJECTIVE_REACH = 4f;

    [Tooltip("distance to determine whether or not the drone is colliding with another drone")]
    public const float DISTANCE_DRONE_COLLISION = 5f;

    [Tooltip("max height of the pathing network")]
    public const float MAP_HEIGHT_MAX = 50f;

    [Tooltip("min height of the pathing network")]
    public const float MAP_HEIGHT_MIN = 1.75f;

    [Tooltip("speed ratio for a drone to turn")]
    public const float SPEED_RATIO_ROTATION = 0.2f;

    [Tooltip("speed ratio for a drone to takeoff/land")]
    public const float SPEED_RATIO_VERTICLE = 0.65f + 0.35f;

    [Tooltip("duration of a float text")]
    public const float TEXT_FLOAT_DURATION = 1f;

    [Tooltip("font size of a float text")]
    public const float TEXT_FLOAT_SIZE = 30f;

    [Tooltip("the verticle velocity of a float text")]
    public const float TEXT_FLOAT_SPEED = 30f;

    [Tooltip("font size of a timer text")]
    public const float TEXT_TIMER_SIZE = 40f;

    [Tooltip("duration for a drone to rebirth")]
    public const int COUNTER_REBIRTH = 10;

    #endregion

    #region Static Variables
    [Tooltip("game's current level")]
    public static int level = 1;

    [Tooltip("determine whether or not the game is paused")]
    public static bool isTheGamePaused = false;

    [Tooltip("a layer mask for ray cast to find the invisible ceiling")]
    public static int ceilingMask = LayerMask.GetMask("PathHeight");

    [Tooltip("a layer mask for ray cast to find the objective targets")]
    public static int objectiveMask = LayerMask.GetMask("Objective");

    [Tooltip("The drone layer mask")]
    public static int droneMask = LayerMask.GetMask("Drone");

    [Tooltip("the default layer mask")]
    public static int defaultMask = LayerMask.GetMask("Default");

    [Tooltip("a transform to hold all spawning cache")]
    public static Transform spawnHolder;

    [Tooltip("a manager to handle all item data")]
    public static ItemManager itemManager;

    [Tooltip("a manager to handle all image components")]
    public static GUIFxManager guifxManager;

    [Tooltip("a manager to handle all spawn settings")]
    public static DemandSpawner demandSpawner;

    #endregion

    /// <summary>
    /// Method to find all core objects in the scene,
    /// it's called via Gamemanager when the game first starts.
    /// </summary>
    public static void Initialize()
    {
        itemManager = FindObjectOfType<ItemManager>();
        guifxManager = FindObjectOfType<GUIFxManager>();
        demandSpawner = FindObjectOfType<DemandSpawner>();
        spawnHolder = GameObject.FindGameObjectWithTag("CacheHolder").transform;
    }
}