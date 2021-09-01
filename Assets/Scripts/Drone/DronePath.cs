using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script for users to draw paths for drones
/// <para>Contributor: Weizhao</para>
/// </summary>
public class DronePath : MonoBehaviour
{
    [Header("Objects")]
    [Tooltip("Pathing dot prefab object")]
    public GameObject pathingDot;
    [Tooltip("Visual effect to display when the drone is selected")]
    public GameObject selectionFx;
    [Tooltip("Mesh collider for the ceiling, the ceiling is used for referencing" +
        "the maximum height of the pathing network")]
    public MeshCollider pathHeightCol;

    private Drone theDrone;         // the drone class of the selected drone
    private bool isSelected;        // determine whether or not the drone is being selected
    private Vector3 lastHitPoint;   // to record the last mouse position when drawing pathing dots

    // Start is called before the first frame update
    void Start()
    {      
        // reset height for the invisible ceiling
        pathHeightCol.transform.position = new Vector3(
            pathHeightCol.transform.position.x,
            Blackboard.MAP_HEIGHT_MAX,
            pathHeightCol.transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
            DrawingPath();
    }

    /// <summary>
    /// It's called via Mouse.cs when a drone is selected
    /// </summary>
    /// <param name="drone">the selected drone</param>
    public void Select(GameObject drone)
    {
        // switch isSelected to true and enable ceiling collider
        isSelected = true;
        selectionFx.SetActive(true);
        pathHeightCol.enabled = true;        

        // find drone component of the selected drone and trigger its select method
        theDrone = drone.GetComponent<Drone>();
        theDrone.Selected();
        
        // reset last hit point
        lastHitPoint = drone.transform.position;
    }

    /// <summary>
    /// It's called when the user let go the left-click while the drone
    /// is being selected
    /// </summary>
    public void Deselect()
    {
        // switch isSelected to false and hide selectionFx
        isSelected = false;
        selectionFx.SetActive(false);
        selectionFx.transform.position = Vector3.down * 100f;

        // insert the last dot at the mouse position then disable the ceiling
        InsertDot(Mathf.NegativeInfinity);
        pathHeightCol.enabled = false;

        // trigger drone's deselect method
        theDrone.Deselected();
    }

    /// <summary>
    /// It's called every frame when a drone is being selected, this method
    /// will keep binding the selectionFx to the drone and also check the 
    /// mouse position every frame, when the current mouse position is greater
    /// than the previous mouse position, insert a pathing dot
    /// </summary>
    void DrawingPath()
    {
        // update selectionFx's position
        selectionFx.transform.position = theDrone.transform.position;

        // check to see if the user has let go the mouse
        // base case: deselect the drone
        // otherwise: insert a new dot
        if (Input.GetMouseButtonUp(0) || theDrone.IsPaused())
        {
            Deselect();
        }
        else
        {
            InsertDot(Blackboard.DISTANCE_DOT_GAP);
        }
    }

    /// <summary>
    /// Method to insert a pathing dot
    /// </summary>
    /// <param name="minDistance">requred gap distance</param>
    void InsertDot(float minDistance)
    {
        // get mouse position and create a ray shooting from camera to the mouse position
        var mousePos = Input.mousePosition;
        var mouseRay = Camera.main.ScreenPointToRay(mousePos);

        // create a raycasthit to capture the mouse position in a specific height
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit, Mathf.Infinity, Blackboard.ceilingMask))
        {
            // find this hit point
            var thisHitPoint = hit.point;

            // if the hit point is far enough from the last hit point, insert a new dot
            if ((lastHitPoint - thisHitPoint).sqrMagnitude > minDistance)
            {
                // save this hit point as the last hit point
                lastHitPoint = thisHitPoint;

                // create a new pathing dot
                var dot = Instantiate(pathingDot, Blackboard.spawnHolder);
                dot.transform.position = lastHitPoint;

                // add this pathing dot and position into the drone's data
                theDrone.pathingDots.Add(dot);
                theDrone.pathingLocs.Add(lastHitPoint);
            }
        }
    }
}
