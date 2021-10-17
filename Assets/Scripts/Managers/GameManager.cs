using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The manager to handle game flow
/// <para>Contributor: Weizhao</para>
/// This game will be infinite with no win condition. The loss will be when the timer runs out. There will be a scoreboard. - Grace
/// </summary>
public class GameManager : MonoBehaviour
{
    //Game over screen off/on
    public GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        Blackboard.Initialize();
    }

    //  Method to end game Lose condition: time has run out
    //  Timer ended and all the objective have not been completed
    public void GameOver()
    {
        //Show game over screen
        gameOverUI.SetActive(true);
    }
}
