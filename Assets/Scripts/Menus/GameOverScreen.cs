using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Screen that will appear when the game is over.
/// Contributor: Grace
/// </summary>
public class GameOverScreen : MonoBehaviour
{
    //Restart game
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    //Quit game
    public void QuitGame()
    {
        Application.Quit();
    }
}
