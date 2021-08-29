using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DroneFSM
{
    Idle,
    Move
}

public class Drone : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float rotationSpeed = 4f;


    private int pathingIndex;
    private Vector3 destPos;
    private LineRenderer line;

    [HideInInspector]
    public List<Vector3> pathingLocs;
    public List<GameObject> pathingDots;
    
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        line = GetComponent<LineRenderer>();
        pathingLocs = new List<Vector3>();
        pathingDots = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (pathingLocs.Count == pathingIndex)
            return;

        var destPos = pathingLocs[pathingIndex];
        var prevPos = this.transform.position;
        var nextPos = Vector3.MoveTowards(prevPos, destPos, movementSpeed * Time.deltaTime);
        var destRot = Quaternion.LookRotation(destPos - prevPos);
        var nextRot = Quaternion.Lerp(transform.rotation, destRot, Mathf.Min(1f, rotationSpeed * Time.deltaTime));

        transform.position = nextPos;
        transform.rotation = nextRot;

        if ((destPos - nextPos).sqrMagnitude <= Blackboard.DISTANCE_DOT_REACH)
        {
            pathingIndex++;
        }
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
