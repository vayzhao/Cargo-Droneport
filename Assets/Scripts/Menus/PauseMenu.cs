using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is the script for the submenu (pause menu) for the game.
/// This will allow to player to pause the game.
/// In Unity3D, need to create:
/// Canvas (COMPLETED)
/// Resume button (NOT COMPLETED)
/// Menu button (COMPLETED)
/// Quit button (COMPLETED)
/// Contributor: Grace
/// </summary>
public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    //GameObjects
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        // Keybind ESC to open pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }
    }


    // Function to pause the game
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    // Function to resume the game
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    // Function to load menu
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    // Quit game
    public void QuitGame()
    {
        Application.Quit();
    }
}
