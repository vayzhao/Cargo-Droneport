using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is for the functionality of GUI menu, including
/// game start, quit game, poping up / hiding UI panels
/// <para>Contributor: Grace </para>
/// </summary>
public class Menu : MonoBehaviour
{
    public GameObject creditPanel; // parent gameobject for credit panel
    public GameObject optionPanel; // parent gameobject for option panel

    /// <summary>
    /// Method to enter the main game scene and initialize game level
    /// </summary>
    public void StartGame()
    {
        // TODO:
    }

    /// <summary>
    /// Method to terminate the program
    /// </summary>
    public void QuitGame()
    {
        // TODO:
    }
    
    /// <summary>
    /// Method to display or to hide the credit panel object
    /// </summary>
    /// <param name="isShowing">True for showing, False for hiding</param>
    public void DisplayCredit(bool isShowing)
    {
        // TODO:
    }

    /// <summary>
    /// Method to display or to hide the option panel object
    /// </summary>
    /// <param name="isShowing">True for showing, False for hiding</param>
    public void DisplayOption(bool isShowing)
    {
        // TODO:
    }
}
