using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The manager to handle all GUI fx including 2D sprites,
/// texts so on...
/// <para>Contributor: Weizhao</para>
/// </summary>
public class GUIFxManager : MonoBehaviour
{
    [Header("Canvas")]
    public Transform fxCanvas;

    [Header("Prefabs")]
    public GameObject iconPrefab;
    public GameObject textPrefab;

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

    /// <summary>
    /// Method to create a text object
    /// </summary>
    /// <param name="worldPos">where the object is spawned at</param>
    /// <returns></returns>
    public GameObject CreateText(Vector3 worldPos)
    {
        // instantiate an object with text component in fxCanvas
        var obj = Instantiate(textPrefab, fxCanvas);

        // convert object's position from world position to screen position
        var pos = Camera.main.WorldToScreenPoint(worldPos);
        obj.transform.position = pos;

        // return the object
        return obj;
    }

    /// <summary>
    /// Method to quicky instantiate a float text
    /// </summary>
    /// <param name="worldPos">where the object is spawned at</param>
    /// <param name="message">text to display</param>
    /// <param name="color">color of the text</param>
    public void DisplayFloatText(Vector3 worldPos, string message, Color color)
    {
        // first to spawn a text object
        var obj = CreateText(worldPos);

        // play float text effect from the text object
        obj.GetComponent<TextFx>().PlayFloatText(message, color);        
    }

    /// <summary>
    /// Method to quickly instantiate a timer text
    /// </summary>
    /// <param name="worldPos">where the object is spawned at</param>
    /// <param name="countFrom">timer counts from n second</param>
    /// <param name="color">color of the text</param>
    public void DisplayTimerText(Vector3 worldPos, int countFrom, Color color)
    {
        // first to spawn a text object
        var obj = CreateText(worldPos);

        // play timer text from the text object
        obj.GetComponent<TextFx>().PlayTimerText(countFrom, color);
    }

}
