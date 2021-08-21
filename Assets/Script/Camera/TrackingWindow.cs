using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// A class to handle the second camera, including functions
/// for rescaling the tracking window, reset track target...etc.
/// <para>Contributor: Weizhao </para>
/// </summary>
[RequireComponent(typeof(Image))]
public class TrackingWindow : MonoBehaviour
{
    [Header("Scalable Range")]
    [Tooltip("Minimum size of the tracking window")]
    public float minSize = 100f;
    [Tooltip("Maximum size of the tracking window")]
    public float maxSize = 400f;

    [Header("Objects")]
    [Tooltip("The camera used for tracking")]
    public GameObject cam;
    [Tooltip("The object that is being tracked")]
    public GameObject obj;
    [Tooltip("Offset position between the camera to the tracked object")]
    public Vector3 offSetPos;
    [Tooltip("Offset rotation between the camera to the tracked object")]
    public Vector3 offSetRot;

    private float zoomScale;     // current zoom scale (0.0f ~ 1.0f)
    private bool isSelected;     // determine if the tracking window is selected by the user
    private Image backgroundImg; // boarder of the tracking window
    private RectTransform rect;  // rect transform of tge tracking window

    // Start is called before the first frame update
    void Start()
    {
        Reset();
        TrackObject(obj);
    }

    [ExecuteInEditMode]
    private void Reset()
    {
        // obtain tracking window's rect transform
        rect = GetComponent<RectTransform>();

        // obtain tracking window's boarder and disable it
        backgroundImg = GetComponent<Image>();
        backgroundImg.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
    }

    /// <summary>
    /// Method to switch 'isSelected' variable value
    /// It's called when the user clicks on the tracking window
    /// </summary>
    /// <param name="isSelecting">determine if the tracking window is clicked</param>
    public void ChangeSelection(bool isSelecting)
    {
        isSelected = isSelecting;
        backgroundImg.enabled = isSelected;
    }

    /// <summary>
    /// Method to rescale the tracking window, It is functioning when
    /// the user scoll the mosue wheel while tracking window is selected
    /// </summary>
    void Zoom()
    {
        // terminate the method if the tracking window is not selected
        if (!isSelected)
            return;
        
        // otherwise obtain mouse scroll delta value
        var change = Input.mouseScrollDelta.y;
        if (change != 0f)
        {
            // rescale the zoom level and compute the new size for the tracking window
            zoomScale = Mathf.Clamp(zoomScale + change * Time.deltaTime, 0f, 1f);
            var newSize = Mathf.Lerp(minSize, maxSize, zoomScale);

            // update the size for the tracking window
            rect.sizeDelta = Vector2.one * newSize;
        }
    }

    /// <summary>
    /// Method to bind the second camera to a drone
    /// </summary>
    /// <param name="trackedObject"></param>
    public void TrackObject(GameObject trackedObject)
    {
        // bind the tracked object to this script
        obj = trackedObject;
        
        // bind second camera to the object 
        cam.transform.parent = obj.transform;
        cam.transform.localPosition = offSetPos;
        cam.transform.localEulerAngles = offSetRot;
    }


}
