using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Package is a class that instantiated when a drone
/// picked up an material. 
/// <para>Contributor: Weizhao</para>
/// </summary>
public class Package 
{
    private Item item;          // the item inside the package
    private Transform box;      // the model of the package
    private Transform carrier;  // the drone
    private GameObject iconObj; // the image obj to show what the package item is

    /// <summary>
    /// Method to instantiate a package
    /// </summary>
    /// <param name="theMat">the material information</param>
    /// <param name="carrier">the drone</param>
    public Package(Mat theMat, Transform carrier)
    {
        // binding carrier and item type
        this.carrier = carrier;
        this.item = theMat.item;

        // parent the box and reset its local position
        box = theMat.box;
        box.parent = carrier;
        box.localPosition = Vector3.down * 0.075f;

        // create an icon above the carrier
        iconObj = Blackboard.guifxManager.CreateImage(item.sprite);
    }

    /// <summary>
    /// Method to update material sprite's position everyframe,
    /// the position's offset is based on the quantity of item
    /// that the drone carries
    /// </summary>
    /// <param name="origin">position above the drone</param>
    /// <param name="index">index of the carried item</param>
    public void UpdateIconPosition(Vector3 origin, int index)
    {
        iconObj.transform.position = origin + new Vector3(index * 50f, 0f, 0f);
    }

    /// <summary>
    /// Method for demand to take package from the drone
    /// </summary>
    /// <param name="demandTransform"></param>
    public void Unpatch(Transform demandTransform)
    {
        // parenting box to demand transform
        box.parent = demandTransform;

        // remove the sprite gameobjects
        MonoBehaviour.Destroy(iconObj);
    }

    /// <summary>
    /// Method to return package item's id
    /// </summary>
    /// <returns></returns>
    public int GetPackageItemId() { return item.itemId; }

    /// <summary>
    /// Method to reset box's position and rotation
    /// </summary>
    /// <param name="pos">new local position</param>
    /// <param name="angles">new local euler angles</param>
    public void SetBoxPivot(Vector3 pos, Vector3 angles)
    {
        box.localPosition = pos;
        box.localEulerAngles = angles;
    }
}