using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The manager to handle images within the game canvas
/// It includes functions like spawning images, spawning
/// sprite animations...
/// <para>Contributor: Weizhao</para>
/// </summary>
public class ImageManager : MonoBehaviour
{
    [Header("Canvas")]
    public Transform fxCanvas;

    [Header("Prefabs")]
    public GameObject iconPrefab;

    /// <summary>
    /// Method to create an image
    /// </summary>
    /// <returns></returns>
    public GameObject CreateImage()
    {
        return Instantiate(iconPrefab, fxCanvas);
    }

    /// <summary>
    /// Method to create an image with sprite setup
    /// </summary>
    /// <param name="sprite"></param>
    /// <returns></returns>
    public GameObject CreateImage(Sprite sprite)
    {
        // create the prefab
        var obj = CreateImage();
        
        // edit the sprite of the image
        obj.GetComponent<Image>().sprite = sprite;

        // return
        return obj;
    }

}
