using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Node used in A* search algorithm
/// <para>Contributor: Weizhao </para>
/// </summary>
public class Node : MonoBehaviour
{
    public int index;            // index of the node
    public int[] neighbors;      // neighbors index of the node
    public float groundDistance; // distance between the node and the ground

    private void Start()
    {
        FindGroundHeight();
    }

    /// <summary>
    /// Method for the node to detect how far the distance between
    /// the node itself and the ground
    /// </summary>
    void FindGroundHeight()
    {
        // create a ray and a raycasthit
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        // find the distance to ground
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Environment")))
            groundDistance = hit.distance;
        else
            groundDistance = 0f;

        // change line render's distance
        LineRenderer lineRender = GetComponent<LineRenderer>();
        lineRender.SetPosition(1, Vector3.down * groundDistance);
    }

}