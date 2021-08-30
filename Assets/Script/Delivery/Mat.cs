using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A class that holds material data
/// <para>Contributor: Weizhao</para>
/// </summary>
public class Mat : MonoBehaviour
{
    [HideInInspector]
    public Item item;         // the item data of the material
    [HideInInspector]            
    public Transform box;     // the box model to represents the material

    private Transform canvas; // the canvas to display material's icon 

    /// <summary>
    /// Method to instantiate a mat class with given itemId 
    /// </summary>
    /// <param name="itemId"></param>
    public Mat(int itemId)
    {
        // setup the item
        item = Blackboard.itemManager.allItems[itemId];

        // find the box model
        box = transform.GetChild(0);

        // find the canvas
        canvas = transform.GetChild(1);
        canvas.GetComponentInChildren<Image>().sprite = item.sprite;
    }

    /// <summary>
    /// This method is called when the drone starts landing
    /// to pick the package
    /// </summary>
    public void OnReceive()
    {
        // set this gameobject's layer to default
        this.gameObject.layer = Blackboard.defaultMask;

        // disable the canvas
        canvas.gameObject.SetActive(false);
    }

    /// <summary>
    /// This method is called after the item is
    /// picked by the drone
    /// </summary>
    public void OnReceiveComplete()
    {
        // destroy this gameobject
        Destroy(this.gameObject);
    }    
}
