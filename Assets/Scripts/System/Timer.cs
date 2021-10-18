using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //Time limit
    public float timeLeft = 240.0f;

    // Only execute when timer is running
    public bool timerIsRunning = false;

    // Call GameOver function
    public GameObject gameOver;

    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        // Starts timer automatically
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }

            else
            {
                //Debug.Log("Time over");
                gameOver.SetActive(true);
            }

            DisplayTime(timeLeft);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
