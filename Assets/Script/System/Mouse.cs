using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Functionality for mouse events
/// <para>Contributor: Weizhao </para>
/// </summary>
public class Mouse : MonoBehaviour
{
    public TooltipBox tooltipBox;          // parent object for tooltip box
    private TrackingWindow trackingWindow; // parent object for tracking window
    private DronePath dronePath;

    private void Start()
    {
        dronePath = GetComponent<DronePath>();
        trackingWindow = FindObjectOfType<TrackingWindow>();        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            LeftClick();
        else if (Input.GetMouseButtonDown(1))
            RightClick();
    }

    /// <summary>
    /// Method for mouse to enter an GUI element
    /// (e.g playing sound effects or poping up tooltip)
    /// </summary>
    private void OnMouseEnter()
    {
        // TODO: 
        Debug.Log("Mouse Enter");
    }

    /// <summary>
    /// Method for mouse to exit an GUI element
    /// (e.g playing sound effect or hiding tooltip)
    /// </summary>
    private void OnMouseExit()
    {
        // TODO:
        Debug.Log("Mouse Exit");
    }

    /// <summary>
    /// Method for mouse to press an GUI element
    /// (e.g playing sound effect)
    /// </summary>
    private void OnMouseDown()
    {
        // TODO:
        Debug.Log("Mouse Down");
    }

    /// <summary>
    /// Return an array of objects around the mouse position
    /// </summary>
    /// <returns></returns>
    Collider[] GetClickedObjects()
    {
        // TODO:
        return null;
    }

    /// <summary>
    /// Method for mouse-left click
    /// </summary>
    void LeftClick()
    {
        // create a ray going from camera to the screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // use ray cast to capture drones
        if (Physics.Raycast(ray, out hit, Mathf.Infinity,Blackboard.droneMask))
        {
            // when drone is captured, issue the second camera to track it
            // trackingWindow.TrackObject(hit.collider.gameObject);
            dronePath.Select(hit.collider.gameObject);
        }
    }

    /// <summary>
    /// Method for mouse-right click
    /// </summary>
    void RightClick()
    {
        // TODO:
    }
}