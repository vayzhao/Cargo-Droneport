using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DroneFSM
{
    Move,
    Takeoff,
    Land
}

public enum LandPurpose
{
    Collect,
    Place
}

public class Drone : MonoBehaviour
{
    private DroneFSM fsm;
    private LandPurpose landPurpose;

    public bool isPaused;
    public float speed = 10f;

    private int pathingIndex;
    private LineRenderer pathingLine;
    private Animator animator;

    private Mat manipulatedMat;
    private Demand manipulatedDemand;
    private Transform carriedBox;
    private Vector3 landingPos;
    private Vector3 posBeforeLand;
    


    [HideInInspector]
    public List<Vector3> pathingLocs;
    [HideInInspector]
    public List<GameObject> pathingDots;
    
    // Start is called before the first frame update
    void Start()
    {
        Reset();

        // initially, the drone takes off
        fsm = DroneFSM.Takeoff;
        posBeforeLand = transform.position;
        posBeforeLand.y = Blackboard.MAP_HEIGHT_MAX;        
    }

    private void Reset()
    {
        pathingLine = GetComponent<LineRenderer>();
        pathingLocs = new List<Vector3>();
        pathingDots = new List<GameObject>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (fsm)
        {
            case DroneFSM.Move:
                Move();
                break;
            case DroneFSM.Takeoff:
                TakeOff();
                break;
            case DroneFSM.Land:
                Land();
                break;
            default:
                break;
        }
    }

    #region Drone Pathing
    /// <summary>
    /// It's called via DronePath.cs when the drone is first selected
    /// </summary>
    public void Selected()
    {
        pathingIndex = 0;
        pathingLocs.Clear();
        pathingLine.positionCount = 0;
    }

    /// <summary>
    /// It's called via DronePath.cs when the drone is unselected
    /// </summary>
    public void Deselected()
    {
        DrawLines();
        RemoveDots();
    }

    /// <summary>
    /// Method to setup line component connecting each pathing dots from drone towards
    /// destination in reverse order
    /// </summary>
    void DrawLines()
    {
        // reset line's position count
        pathingLine.positionCount = pathingLocs.Count - pathingIndex;

        // connect pathing dots 
        for (int i = 0; i < pathingLine.positionCount; i++)
            pathingLine.SetPosition(i, pathingLocs[pathingLocs.Count - i - 1]);

        // the last index position would be the drone's current position
        pathingLine.positionCount++;
        pathingLine.SetPosition(pathingLine.positionCount - 1, transform.position);
    }

    /// <summary>
    /// Method to remove all the pathing dots and clear
    /// up the list
    /// </summary>
    void RemoveDots()
    {
        foreach (var dot in pathingDots)
            Destroy(dot.gameObject);

        pathingDots.Clear();
    }
    #endregion

    #region Drone FSM
    /// <summary>
    /// Drone FSM Behaviour - Move
    /// In this method, the drone will move towards the destination along the pathing line,
    /// while moving, the drone will detect objective targets (material & demand) around it,
    /// when an objective target is found, the drone will switch to landing fsm
    /// </summary>
    void Move()
    {
        // detect objective targets around the drone
        DetectObjectiveTarget();

        // return if the drone is not going anywhere
        if (pathingLocs.Count == pathingIndex)
            return;

        // calculate the moving position and rotation
        var destPos = pathingLocs[pathingIndex];
        var prevPos = this.transform.position;
        var nextPos = Vector3.MoveTowards(prevPos, destPos, speed * Time.deltaTime);
        var destRot = Quaternion.LookRotation(destPos - prevPos);
        var nextRot = Quaternion.Lerp(transform.rotation, destRot, Mathf.Min(1f, speed * Blackboard.SPEED_RATIO_ROTATION * Time.deltaTime));

        // actual move the drone
        transform.position = nextPos;
        transform.rotation = nextRot;

        // draw a line shows where the drone is going to
        if (pathingLine.positionCount > 0)
            pathingLine.SetPosition(pathingLine.positionCount - 1, transform.position);

        // determine whether or not the drone has reach a turning point
        if ((destPos - nextPos).sqrMagnitude <= Blackboard.DISTANCE_DOT_REACH)
        {
            // if the pathing line has not been drawn, remove pathing dots that the drone has passed,
            // otherwise, decrease position count for the pathing line
            if (pathingLine.positionCount == 0 && pathingDots.Count > 0)
                pathingDots[pathingIndex].SetActive(false);
            else
                pathingLine.positionCount--;

            // increase the pathing index
            pathingIndex++;
        }
    }

    /// <summary>
    /// Drone FSM Behaviour - TakeOff
    /// </summary>
    void TakeOff()
    {
        // drone taking off
        transform.position = Vector3.MoveTowards(transform.position, posBeforeLand, speed * Blackboard.SPEED_RATIO_VERTICLE * Time.deltaTime);

        // check to see if the drone has finished taking off
        if ((transform.position - posBeforeLand).sqrMagnitude < Blackboard.DISTANCE_DOT_REACH)
        {
            // switch drone fsm to move
            fsm = DroneFSM.Move;
        }
    }

    /// <summary>
    /// Drone FSM Behavriou - Land
    /// </summary>
    void Land()
    {
        // drone landing
        transform.position = Vector3.MoveTowards(transform.position, landingPos, speed * Blackboard.SPEED_RATIO_VERTICLE * Time.deltaTime); 

        // check to see if the drone has finished landing
        if ((transform.position-landingPos).sqrMagnitude < Blackboard.DISTANCE_DOT_REACH)
        {
            // check the purpose of landing
            switch (landPurpose)
            {
                case LandPurpose.Collect:
                    LandForCollecting();
                    break;
                case LandPurpose.Place:
                    LandForPlacing();
                    break;
                default:
                    break;
            }

            // switch drone fsm to take off
            fsm = DroneFSM.Takeoff;
        }
    }

    #endregion

    #region Objective
    void DetectObjectiveTarget()
    {
        // using physics overlap to scan objective targets around the drone
        Collider[] objective = Physics.OverlapSphere(transform.position,
            Blackboard.DISTANCE_OBJECTIVE_REACH, Blackboard.objectiveMask);

        // return if no objective target is found
        if (objective.Length == 0)
            return;

        // otherwise, check the type of the target
        var target = objective[0].transform;
        if (target.GetComponent<Mat>())
        {
            // find the mat component of the target
            manipulatedMat = target.GetComponent<Mat>();

            // trigger mat's receive function
            manipulatedMat.OnReceive();

            // switch landing purpose to collect
            landPurpose = LandPurpose.Collect;
        }
        else if (target.GetComponent<Demand>())
        {
            // find the demand component of the target
            manipulatedDemand = target.GetComponent<Demand>();

            // trigger demand's receive function
            manipulatedDemand.OnReceive();

            // switch landing purpose to place
            landPurpose = LandPurpose.Place;
        }
        else
        {
            // return if the target type is not <Mat>, neither <Demand>
            return;           
        }

        // store drone's current position
        posBeforeLand = transform.position;

        // calculate landing position
        landingPos = target.transform.position;
        landingPos.y = Blackboard.MAP_HEIGHT_MIN;

        // switch drone fsm to land
        fsm = DroneFSM.Land;
    }
    
    /// <summary>
    /// Land action - collect
    /// </summary>
    void LandForCollecting()
    {
        // parent the box and reset its local position
        carriedBox = manipulatedMat.box;
        carriedBox.parent = transform;
        carriedBox.localPosition = new Vector3(0f,-0.075f,0f);

        // finish collecting the package
        manipulatedMat.ReceiveComplete();
    }

    /// <summary>
    /// Land action - place
    /// </summary>
    void LandForPlacing()
    {
        // finish placing the package
        manipulatedDemand.ReceiveComplete(carriedBox);
    }
    #endregion



}
