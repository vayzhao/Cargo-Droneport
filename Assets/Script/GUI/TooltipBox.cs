using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Informative message that appears when the user
/// interacts with an element in a GUI
/// <para>Contributor: Weizhao </para>
/// </summary>
public class TooltipBox : MonoBehaviour
{
    public Image bg;                     // background of the tooltip box
    public Image iconImg;                // UI component to display icon
    public Text nameText;                // UI component to display name
    public Text descText;                // UI component to display description

    private bool hasIcon;                // determine whether or not the tooltip has icon
    private RectTransform rectTransform; // the rect transform of the tooltip box

    /// <summary>
    /// Method to hide the tooltip box
    /// </summary>
    public void Hide()
    {
        // TODO:
    }

    /// <summary>
    /// Method to adjust the size of the tooltip box
    /// Height: depends on whether or not the tooltip box has icon
    /// Width: depends on how long the description is 
    /// </summary>
    void Resize()
    {
        // TODO:
    }

    /// <summary>
    /// Method to display the tooltip box with the given information
    /// and resize it afterwards
    /// </summary>
    /// <param name="pos">where the tooltip box pops up</param>
    /// <param name="icon">icon of the tooltip box</param>
    /// <param name="name">name of the tooltip box</param>
    /// <param name="desc">description of the tooltip box</param>
    public void Display(Vector3 pos, Sprite icon, string name, string desc)
    {
        // TODO:
    }
    public void Display(Vector3 pos, string desc)
    {
        // TODO:
    }
}