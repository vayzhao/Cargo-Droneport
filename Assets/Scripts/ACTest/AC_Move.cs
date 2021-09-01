using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_Move : MonoBehaviour
{
    [Range(10f,30f)]
    public float moveSpeed = 20f;
    [Range(2f,10f)]
    public float rotationSpeed = 5f;
    public Transform pointA;
    public Transform pointB;

    private Vector3 dest;
    private Vector3 tempDest;
    public Animator animator;

    private enum State
    {
        Idle,
        Flying,
        Takeoff,
        Landing
    }

    [SerializeField]
    private State myState;

    // Start is called before the first frame update
    void Start()
    {
        dest = pointA.transform.position;
        myState = State.Takeoff;
        animator = this.GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (myState)
        {
            case State.Idle:
                break;
            case State.Flying:
                Flying();
                break;
            case State.Takeoff:
                TakeOff();
                break;
            case State.Landing:
                Landing();
                break;
            default:
                break;
        }
    }

    void Flying()
    {
        var rot = Quaternion.LookRotation(dest - transform.position);
        var fix = Mathf.Min(1f, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, fix);

        MoveTo(dest);
        if (HasReachDestination(dest))
        {
            var height = dest == pointA.position ? 36f : 2f;
            myState = State.Landing;
            tempDest = new Vector3(dest.x, height, dest.z);
            animator.SetInteger("Value", 3);
        }
    }

    void TakeOff()
    {
        MoveTo(dest);
        if (HasReachDestination(dest))
        {
            myState = State.Flying;
            dest = dest == pointA.position ? pointB.position : pointA.position;
            animator.SetInteger("Value", 2);
        }
    }

    void Landing()
    {
        MoveTo(tempDest);
        if (HasReachDestination(tempDest))
        {
            myState = State.Takeoff;
            animator.SetInteger("Value", 1);
        }
    }

    void MoveTo(Vector3 destPos)
    {
        var prevPos = this.transform.position;
        var nextPos = Vector3.MoveTowards(prevPos, destPos, moveSpeed * Time.deltaTime);
        this.transform.position = nextPos;
    }
    
    bool HasReachDestination(Vector3 destPos)
    {
        return Vector3.Distance(this.transform.position, destPos) <= 0.1f;
    }
}
