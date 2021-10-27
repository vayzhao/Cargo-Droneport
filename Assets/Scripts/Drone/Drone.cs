using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The core script of drones
/// <para>Contributor: Weizhao, Grace</para>
/// </summary>

public class Drone : MonoBehaviour
{
    public float speed = 30f;
    public int capacity = 3;

    private DroneFSM fsm;
    private LandPurpose landPurpose;
    private int pathingIndex;
    private LineRenderer pathingLine;
    private Animator animator;
    private BoxCollider boxCollider;
    private Mat manipulatedMat;
    private Demand manipulatedDemand;    
    private Vector3 landingPos;
    private Vector3 posBeforeLand;

    [HideInInspector]
    public float currentSpeed;
    [HideInInspector]
    public List<Package> packages;
    [HideInInspector]
    public List<Vector3> pathingLocs;
    [HideInInspector]
    public List<GameObject> pathingDots;

    //Get player score
    public Score score;

    // Start is called before the first frame update
    void Start()
    {
        // find core components of the drone
        animator = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        pathingLine = GetComponentInChildren<LineRenderer>();

        // initialize packages and pathing data
        packages = new List<Package>();
        pathingLocs = new List<Vector3>();
        pathingDots = new List<GameObject>();
        
        // initially, the drone takes off
        fsm = DroneFSM.Takeoff;
        currentSpeed = speed;
        posBeforeLand = transform.position;
        posBeforeLand.y = Blackboard.MAP_HEIGHT_MAX;        
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

        UpdateCarriedMatIcon();
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
        // only draw lines when the drone is not being repaired
        if (!IsPaused())
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
        var nextPos = Vector3.MoveTowards(prevPos, destPos, currentSpeed * Time.deltaTime);
        var destRot = Quaternion.LookRotation(destPos - prevPos);
        var nextRot = Quaternion.Lerp(transform.rotation, destRot, Mathf.Min(1f, currentSpeed * Blackboard.SPEED_RATIO_ROTATION * Time.deltaTime));

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
        transform.position = Vector3.MoveTowards(transform.position, posBeforeLand, currentSpeed * Blackboard.SPEED_RATIO_VERTICLE * Time.deltaTime);

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
        transform.position = Vector3.MoveTowards(transform.position, landingPos, currentSpeed * Blackboard.SPEED_RATIO_VERTICLE * Time.deltaTime); 

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

    #region Rebirth
    /// <summary>
    /// Method to pause the drone, it's called when 2 drones collide 
    /// with each other
    /// </summary>
    public void Pause(float duration)
    {
        // switch fsm to repair
        fsm = DroneFSM.Repair;

        // hide pathing lines & disable collider
        boxCollider.enabled = false;
        pathingLine.positionCount = 0;

        // pop up a timer text and invoke unpause method in n seconds
        Blackboard.guifxManager.DisplayTimerText(transform.position, Blackboard.COUNTER_REBIRTH, Color.red);
        Invoke("Unpause", duration);
    }

    /// <summary>
    /// Method to unpause the drone, it's called in n second after
    /// Pause() method is called
    /// </summary>
    public void Unpause()
    {
        // resume fsm to takeoff
        fsm = DroneFSM.Takeoff;
        posBeforeLand = transform.position;
        posBeforeLand.y = Blackboard.MAP_HEIGHT_MAX;

        // reset movement's cache
        pathingIndex = 0;
        pathingLocs.Clear();
        
        // finally enable box collider in order to trigger next collision
        boxCollider.enabled = true;
    }

    /// <summary>
    /// Method to determine whether or not the drone is working
    /// </summary>
    /// <returns></returns>
    public bool IsPaused() { return fsm == DroneFSM.Repair; }
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
            // check to see if the drone has enough capacity
            // to pick up this mat
            if (packages.Count < capacity)
            {
                // find the mat component of the target
                manipulatedMat = target.GetComponent<Mat>();

                // trigger mat's receive function
                manipulatedMat.OnReceive();

                // switch landing purpose to collect
                landPurpose = LandPurpose.Collect;
            }
            else
            {
                // otherwise return
                return;
            }            
        }
        else if (target.GetComponent<Demand>())
        {
            // find the demand component of the target
            manipulatedDemand = target.GetComponent<Demand>();

            // check to see if the packages contains a demand item
            if (packages.ContainsDemandItem(manipulatedDemand))
            {
                // trigger demand's receive function
                manipulatedDemand.OnReceive();

                // switch landing purpose to place
                landPurpose = LandPurpose.Place;
            }
            else
            {
                // otherwise return
                return;
            }
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
        // collect this item
        var carriedItem = new Package(manipulatedMat, transform);
        packages.Add(carriedItem);

        // finish collecting the package
        manipulatedMat.OnReceiveComplete();

        // reset packages pivot
        SortPackage();
    }

    /// <summary>
    /// Land action - place
    /// </summary>
    void LandForPlacing()
    {
        // finish placing the package
        score.Money(20);
        manipulatedDemand.OnReceiveComplete(packages);

        // reset packages pivot
        SortPackage();
    }

    /// <summary>
    /// Method to update carried item positions
    /// </summary>
    void UpdateCarriedMatIcon()
    {
        // return if the drone is not carrying anything
        if (packages.Count == 0)
            return;

        // convert drone's position to screen position also
        // calculate the offset position based on the quantity
        // of items carried by the drone
        var origin = Camera.main.WorldToScreenPoint(transform.position);
        origin.y += 50f;
        origin.x -= (packages.Count - 1) * 25f;

        // update icon position for each carried item
        for (int i = 0; i < packages.Count; i++)
        {
            packages[i].UpdateIconPosition(origin, i);
        }
    }

    /// <summary>
    /// Method to sort the position of the boxes carried by the drone
    /// the sorted positions depend on the package quantity
    /// </summary>
    void SortPackage()
    {
        // return if the drone is not carrying any package
        if (packages.Count == 0)
            return;

        // check how many packages is the drone carrying
        if (packages.Count == 1)
        {
            packages[0].SetBoxPivot(Vector3.down * 0.15f, Vector3.zero);
        }
        else
        {
            var gap = 360f / packages.Count;
            for (int i = 1; i < packages.Count + 1; i++)
            {
                var angle = i * gap;
                var polarX = 0.2f * Mathf.Cos(angle * Mathf.Deg2Rad);
                var polarZ = 0.2f * Mathf.Sin(angle * Mathf.Deg2Rad);
                var polarPos = new Vector3(polarX, -0.1f, polarZ);
                packages[i - 1].SetBoxPivot(polarPos, -polarPos);               
            }
        }
    }
    #endregion

    #region Obstacles
    /// <summary>
    /// Method for drones to change speed based on the given factor
    /// </summary>
    /// <param name="factor">factor of speed</param>
    public void ChangeSpeed(float factor)
    {
        currentSpeed = speed * factor;
    }
    #endregion
}
