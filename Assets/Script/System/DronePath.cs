using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePath : MonoBehaviour
{


    private Drone theDrone;
    public GameObject selectionFx;
    private bool isSelected;
    private Vector3 lastHitPoint;
    public MeshCollider pathHeightCol;
    public GameObject pathingDot;
    public float maxHeight = 35f;

    private Transform spawnHolder;
    private int heightMask;

    // Start is called before the first frame update
    void Start()
    {
        // find spawn holder of pathing dot fx
        spawnHolder = selectionFx.transform.parent;
        
        // reset height for the invisible ceiling
        pathHeightCol.transform.position = new Vector3(
            pathHeightCol.transform.position.x,
            maxHeight,
            pathHeightCol.transform.position.z);

        // get layer mask of the invisible ceiling
        heightMask = LayerMask.GetMask("PathHeight");
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
            DrawingPath();
    }

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

    void DrawingPath()
    {
        // update selectionFx's position
        selectionFx.transform.position = theDrone.transform.position;

        // check to see if the user has let go the mouse
        // base case: deselect the drone
        // otherwise: insert a new dot
        if (Input.GetMouseButtonUp(1))
        {
            Deselect();
        }
        else
        {
            InsertDot(Blackboard.DISTANCE_DOT_GAP);
        }
    }

    void InsertDot(float minDistance)
    {
        // get mouse position and create a ray shooting from camera to the mouse position
        var mousePos = Input.mousePosition;
        var mouseRay = Camera.main.ScreenPointToRay(mousePos);

        // create a raycasthit to capture the mouse position in a specific height
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit, Mathf.Infinity, heightMask))
        {
            // find this hit point
            var thisHitPoint = hit.point;

            // if the hit point is far enough from the last hit point, insert a new dot
            if ((lastHitPoint - thisHitPoint).sqrMagnitude > minDistance)
            {
                // save this hit point as the last hit point
                lastHitPoint = thisHitPoint;

                // create a new pathing dot
                var dot = Instantiate(pathingDot, spawnHolder);
                dot.transform.position = lastHitPoint;

                // add this pathing dot and position into the drone's data
                theDrone.pathingDots.Add(dot);
                theDrone.pathingLocs.Add(lastHitPoint);
            }
        }
    }
}
