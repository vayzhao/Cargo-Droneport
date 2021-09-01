using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for waypoint graph and its pathing algorithm
/// <para>Contributor: Weizhao </para>
/// </summary>
public class Pathing : MonoBehaviour
{
    [Tooltip("all nodes in the current level")]
    public Node[] nodes;

    private int start;                           // the starting node
    private int goal;                            // the goal node
    private List<int> openSet = new List<int>(); // openList for A* search algorithm

    // used to record node's path
    // e.g cameFrom[toWhere] = fromWhere
    private Dictionary<int, int> cameFrom = 
        new Dictionary<int, int>();             
    
    // used to record f score values for all nodes
    private Dictionary<int, float> fScore =
        new Dictionary<int, float>();            

    // used to record g score values for all nodes
    private Dictionary<int, float> gScore =
        new Dictionary<int, float>();            

    private void Start()
    {

    }

    /// <summary>
    /// Method to draw a waypoint graph gizmos in scene window
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < nodes.Length; i++)
        {
            for (int j = 0; j < nodes[i].neighbors.Length; j++)
            {
                if (i < nodes[i].neighbors[j])
                {
                    var a = nodes[i].transform.position;
                    var b = nodes[nodes[i].neighbors[j]].transform.position;
                    Gizmos.DrawLine(a, b);
                }
            }
        }
    }

    /// <summary>
    /// Method to find the cloest node around the given position
    /// </summary>
    /// <param name="where">relative position</param>
    /// <returns></returns>
    public int FindCloestNode(Vector3 where)
    {
        // initialize shortest index & distance
        var shortestIndex = -1;
        var shortestDistance = Mathf.Infinity;

        // look for each node
        for (int i = 0; i < nodes.Length; i++)
        {
            var tentativeDistance = Vector3.Distance(
                where, nodes[i].transform.position);
            if (tentativeDistance < shortestDistance)
            {
                shortestIndex = i;
                shortestDistance = tentativeDistance;
            }
        }
        return shortestIndex;
    }

    /// <summary>
    /// Find a path towards the destination using A star 
    /// search algorithm
    /// </summary>
    /// <param name="a">the starting node</param>
    /// <param name="b">the goal node</param>
    /// <returns></returns>
    public List<int> FindPath(int a, int b)
    {
        // bind starting node and goal node to the script
        start = a;
        goal = b;

        // reset data for A* search algorithm
        ResetAlgorithm();

        // continue as long as the open list is not empty
        while (openSet.Count > 0)
        {
            // find the node from open set that has the lowest f score
            int current = PopBest();

            // return the path when the goal is found
            if (current == goal)
                return ReconstructPath(current);

            // otherwise remove current node from open set
            openSet.Remove(current);

            // find the neighboring nodes of the current node
            var neighbors = nodes[current].neighbors;
            for (int i = 0; i < neighbors.Length; i++)
            {
                // calculate the tentative g score for this neighbor node
                var neighbor = neighbors[i];
                var tentativeG = gScore[current] + d(current, neighbor);

                // the tentative g score should be less than the actual g score,
                // otherwise it means you're travelling backward
                if (tentativeG < gScore[neighbor])
                {
                    // add / edit this neighbor node in the cameFrom dictionary
                    if (cameFrom.ContainsKey(neighbor))
                        cameFrom[neighbor] = current;
                    else
                        cameFrom.Add(neighbor, current);

                    // bind f score & g score value to this neighbor node
                    gScore[neighbor] = tentativeG;
                    fScore[neighbor] = tentativeG + h(neighbor);

                    // add this neighbor node into the open set
                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Initialize each node's f score & g score
    /// </summary>
    void ResetAlgorithm()
    {
        // clean up data from last execution
        cameFrom.Clear();
        openSet.Clear();

        // initialize f score & g score for all nodes
        for (int i = 0; i < nodes.Length; i++)
        {
            fScore[i] = Mathf.Infinity;
            gScore[i] = Mathf.Infinity;
        }

        // add starting node into the open set initialize
        // its f score & g score value
        openSet.Add(start);
        fScore[start] = h(start);
        gScore[start] = 0;
    }

    /// <summary>
    /// Find the node in open set that has the lowest f score 
    /// </summary>
    /// <returns></returns>
    int PopBest()
    {
        var lowest = openSet[0];
        for (int i = 0; i < openSet.Count; i++)
        {
            if (fScore[lowest] > fScore[openSet[i]])
            {
                lowest = openSet[i];
            }
        }
        return lowest;
    }

    /// <summary>
    /// Reconstruct the path by moving nodes from cameFrom into a new list and reverse it
    /// </summary>
    /// <param name="n">the current node</param>
    /// <returns></returns>
    List<int> ReconstructPath(int n)
    {
        var path = new List<int> { n };
        while (cameFrom.ContainsKey(n))
        {
            n = cameFrom[n];
            path.Add(n);
        }
        path.Reverse();
        return path;
    }

    /// <summary>
    /// Heuristic functions used in A start search algorithm
    /// </summary>
    /// <param name="n">the checking node</param>
    /// <returns></returns>
    float f(int n) { return g(n) + h(n); }
    float g(int n) { return d(start, n); }
    float h(int n) { return d(n, goal); }
    float d(int a, int b) {
        return Vector3.Distance(nodes[a].transform.position,
            nodes[b].transform.position);
    }
}