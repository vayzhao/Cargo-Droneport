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
    [Tooltip("the image component that display the item icon")]
    public Image itemTexture;
    private Spot spot;    // the spawn spot used by this demand
    private Transform canvas;  // the canvas to display demand's icon
    private Item requiredItem; // the item data of the demand
    
    /// <summary>
    /// Method to initialize a demand with 
    /// given itemId and spawn spot data
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="usedSpot"></param>
    public void Setup(int itemId, Spot usedSpot)
    {
        // setup the required item
        requiredItem = Blackboard.itemManager.allItems[itemId];

        // bind the used spot
        spot = usedSpot;
        spot.SetUsage(false);

        // find the canvas
        canvas = transform.GetChild(0);

        // change the item icon
        itemTexture.sprite = requiredItem.sprite;
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
            if (item.GetPackageItemId() == requiredItem.itemId)
            {
                item.Unpatch(transform);
                removeIndex = packages.IndexOf(item);
                break;
            }
        }
        var package = packages[removeIndex];
        packages.Remove(package);

        // free the spawn spot
        spot.SetUsage(true);

        // rewards the player
        Reward();

        // destroy the demand
        Destroy(this.gameObject, 3f);
    }

    /// <summary>
    /// Method to reward the player when a delivery task
    /// is completed
    /// </summary>
    void Reward()
    {
        // re-calculate box's position
        var boxPos = spot.position;
        boxPos.y = Blackboard.MAP_HEIGHT_MIN;

        // display float text
        Blackboard.guifxManager.DisplayFloatText(boxPos, "$20", Color.green);

        // TODO: Actual increase player's score & reputation
    }
}
