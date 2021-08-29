using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DroneFSM
{
    Idle,
    Move,
    Takeoff,
    Land
}

public class Drone : MonoBehaviour
{
    public DroneFSM fsm;
    public bool isPaused;
    public float speed = 10f;

    private int pathingIndex;
    private Vector3 destPos;
    private LineRenderer line;
    private Animator animator;

    [HideInInspector]
    public List<Vector3> pathingLocs;
    [HideInInspector]
    public List<GameObject> pathingDots;
    
    // Start is called before the first frame update
    void Start()
    {
        Reset();

        fsm = DroneFSM.Takeoff;
    }

    private void Reset()
    {
        line = GetComponent<LineRenderer>();
        pathingLocs = new List<Vector3>();
        pathingDots = new List<GameObject>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (fsm)
        {
            case DroneFSM.Idle:
                Idle();
                break;
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

    void Idle()
    {

    }

    void Move()
    {
        if (pathingLocs.Count == pathingIndex)
            return;

        var destPos = pathingLocs[pathingIndex];
        var prevPos = this.transform.position;
        var nextPos = Vector3.MoveTowards(prevPos, destPos, speed * Time.deltaTime);
        var destRot = Quaternion.LookRotation(destPos - prevPos);
        var nextRot = Quaternion.Lerp(transform.rotation, destRot, Mathf.Min(1f, speed * Blackboard.SPEED_RATIO_ROTATION * Time.deltaTime));

        transform.position = nextPos;
        transform.rotation = nextRot;

        if (line.positionCount > 0)
        {
            line.SetPosition(line.positionCount - 1, transform.position);
        }

        if ((destPos - nextPos).sqrMagnitude <= Blackboard.DISTANCE_DOT_REACH)
        {
            if (line.positionCount == 0 && pathingDots.Count > 0)
            {
                pathingDots[pathingIndex].SetActive(false);
            }
            else
            {
                line.positionCount--;
            }

            pathingIndex++;
        }
    }

    void TakeOff()
    {
        var pos = transform.position;

        if (pos.y < Blackboard.MAX_MAP_HEIGHT)
        {
            pos.y = Mathf.Clamp(pos.y + speed * Blackboard.SPEED_RATIO_VERTICLE * Time.deltaTime, 0f, Blackboard.MAX_MAP_HEIGHT);
            transform.position = pos;
        }
        else
        {
            fsm = DroneFSM.Move;
            animator.SetTrigger("Move");
        }
    }

    void Land()
    {

    }

    public void Selected()
    {
        pathingIndex = 0;
        pathingLocs.Clear();
        line.positionCount = 0;
    }

    public void Deselected()
    {
        DrawLines();
        RemoveDots();
    }

    void DrawLines()
    {
        line.positionCount = pathingLocs.Count - pathingIndex;

        for (int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, pathingLocs[pathingLocs.Count - i - 1]);
        }

        line.positionCount++;
        line.SetPosition(line.positionCount - 1, transform.position);

    }

    void RemoveDots()
    {
        foreach (var dot in pathingDots)
        {
            Destroy(dot.gameObject);
        }
        pathingDots.Clear();
    }



}
