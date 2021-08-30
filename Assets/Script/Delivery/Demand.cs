using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A class that holds demand data
/// <para>Contributor: Weizhao</para>
/// </summary>
public class Demand : MonoBehaviour
{
    private Transform canvas;  // the canvas to display demand's icon
    private Item requiredItem; // the item data of the demand
    
    /// <summary>
    /// Method to instantiate a demand class with given itemId
    /// </summary>
    /// <param name="itemId"></param>
    public Demand(int itemId)
    {
        // setup the item
        requiredItem = Blackboard.itemManager.allItems[itemId];

        // find the canvas
        canvas.transform.GetChild(0);
        canvas.GetComponentInChildren<Image>().sprite = requiredItem.sprite;
    }

    /// <summary>
    /// Method to return required item's id
    /// </summary>
    /// <returns></returns>
    public int GetRequiredItemId() { return requiredItem.itemId; }

    /// <summary>
    /// This method is called when the drone starts landing
    /// to place the package
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
    /// placed by the drone
    /// </summary>
    /// <param name="packages"></param>
    public void OnReceiveComplete(List<Package> packages)
    {
        // run through package list and collect items that 
        // required by the demand
        var removeIndex = -1;
        foreach (var item in packages)
        {
            if (item.Equals(requiredItem))
            {
                item.Unpatch(transform);
                removeIndex = packages.IndexOf(item);
            }
        }
        packages.RemoveAt(removeIndex);

        // destroy the demand
        Destroy(this.gameObject, 3f);
    }
}
