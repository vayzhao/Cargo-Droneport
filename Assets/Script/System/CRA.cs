using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// CRA is the class for win / lose mechanic
/// <para>Contributor: Grace </para>
/// </summary>
public class CRA : MonoBehaviour
{
    public Text timerText;                // text component for timer
    public Text moneyText;                // text component for money
    public Text reputationText;           // text component for reputation
    public GameObject pausePanel;         // parent object for pause menu
    public GameObject victoryPanel;       // parent object for victory panel
    public GameObject failurePanel;       // parent object for failure panel

    private static float timerValue;      // exact value for timer
    private static float moneyValue;      // exact value for money that the player has 
    private static float reputationValue; // exact value for reputation that the player has

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Method to reset timer, money & reputation
    /// It's called when the game first start or restart
    /// The default value of timer is in Asset->Script->System->Blackboard.cs
    /// </summary>
    public void InitializeCRA()
    {
        // TODO: 
    }

    /// <summary>
    /// Method to determine whether or not the player has enough money
    /// </summary>
    /// <param name="amount">the amount of money required</param>
    /// <returns></returns>
    public bool hasEnoughMoney(float amount)
    {
        // TODO:
        return false;
    }

    /// <summary>
    /// Method to determine whether or not the player has enough reputation
    /// </summary>
    /// <param name="amount">the amount of reputation required</param>
    /// <returns></returns>
    public bool hasEnoughReputation(float amount)
    {
        // TODO:
        return false;
    }

    /// <summary>
    /// Method to pause / release the game, set time scale of the game
    /// to be 0f for pausing, 1f for releasing. 
    /// Also to display / hide a GUI panel, it could be pause menu,
    /// victory panel or failure panel, depends on where it is called from
    /// </summary>
    /// <param name="isPausing">True for pause, False for release</param>
    /// <param name="panel">the relevant panel object</param>
    void PauseTheGame(bool isPausing, GameObject panel)
    {
        // TODO:
    }

    /// <summary>
    /// Method for victory, pausing the game and popup victory panel
    /// </summary>
    void Win()
    {
        // TODO:
    }

    /// <summary>
    /// Method for failure, pausing the game and popup failure panel
    /// </summary>
    void Lose()
    {
        // TODO:
    }

    /// <summary>
    /// Method to reduce timer & update its GUI, should trigger
    /// lose function when the timer runs out
    /// </summary>
    void ReduceTimer()
    {
        // TODO:
    }

    /// <summary>
    /// Method for the player to gain reputation, should trigger
    /// win function when has certain amount of reputation
    /// </summary>
    /// <param name="amount">the amount of reputation gained</param>
    public void GainReputation(float amount)
    {
        // TODO:
    }

    /// <summary>
    /// Method for the player to gain money
    /// </summary>
    /// <param name="amount">the amount of money gained</param>
    public void GainMoney(float amount)
    {
        // TODO:
    }

    /// <summary>
    /// Method for the player to cost money
    /// </summary>
    /// <param name="amount">the amount of money costed</param>
    public void CostMoney(float amount)
    {
        // TODO:
    }

    /// <summary>
    /// Method to restart the game at current level, release the game
    /// and initialize CRA
    /// </summary>
    public void Restart()
    {
        // TODO:
    }

    /// <summary>
    /// Method to enter the next level, increment Blackboard.level
    /// and switch to the associated scene
    /// </summary>
    public void EnterNextLevel()
    {
        // TODO:
    }

    /// <summary>
    /// Method to go back to home scene
    /// </summary>
    public void BackToHomeScene()
    {
        // TODO:
    }
}
