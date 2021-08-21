using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamDemo : MonoBehaviour
{
    public float speed = 10f;
    public float rotateSpeed = 10f;
    public Pathing pathing;
    public List<int> path;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
        pathing = FindObjectOfType<Pathing>();
        path = new List<int>();
        animator = this.GetComponentInChildren<Animator>();
        animator.SetInteger("Value", 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (path.Count == 0)
        {
            var start = pathing.FindCloestNode(transform.position);
            var goal = Random.Range(0, 25);
            path = pathing.FindPath(start, goal);
        }

        if (Vector3.Distance(transform.position, pathing.nodes[path[0]].transform.position) < 0.1f)
        {
            path.RemoveAt(0);
        }
        else
        {
            var dest = pathing.nodes[path[0]].transform.position;
            var nextPos = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
            var rot = Quaternion.LookRotation(nextPos - transform.position);
            var fix = Mathf.Min(1f, rotateSpeed * Time.deltaTime);
            var nextRot = Quaternion.Lerp(transform.rotation, rot, fix);

            transform.position = nextPos;
            transform.rotation = nextRot;
        }

    }


}
