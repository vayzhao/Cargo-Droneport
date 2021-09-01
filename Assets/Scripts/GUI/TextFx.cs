using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class to handle text fx, text fx includes floating text,
/// timer text and so on...
/// <para>Contributor: Weizhao </para>
/// </summary>
public class TextFx : MonoBehaviour
{
    private Color col;          // color of the text
    private TMP_Text text;      // text component of the object
    private RectTransform rect; // used to adjust position of a text

    /// <summary>
    /// Method to initialize a float text with message and color 
    /// set up, and also to start a cortoutine moving text 
    /// upward and fading its color
    /// </summary>
    /// <param name="message">message to display</param>
    /// <param name="color">color of the text</param>
    public void PlayFloatText(string message, Color color)
    {
        // get text & rect component of the object
        text = GetComponent<TMP_Text>();
        rect = GetComponent<RectTransform>();

        // setup color 
        col = color;
        text.color = color;

        // setup message and font size
        text.text = message;        
        text.fontSize = Blackboard.TEXT_FLOAT_SIZE;

        // start a coroutine to move the text upward and 
        // to fade text color in everyframe
        StartCoroutine(FloatText());        
    }

    /// <summary>
    /// The coroutine function to handle float text's 
    /// positioning and color fading 
    /// </summary>
    /// <returns></returns>
    IEnumerator FloatText()
    {
        // initial timer
        var timer = 0f;
        
        // run until the timer reaches the duration
        while (timer < Blackboard.TEXT_FLOAT_DURATION)
        {
            // fading color
            col.a = Mathf.Lerp(1f, 0f, timer / Blackboard.TEXT_FLOAT_DURATION);
            text.color = col;

            // moving the text upward
            rect.localPosition += Time.deltaTime * Vector3.up * Blackboard.TEXT_FLOAT_SPEED;

            // counting timer
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        // destroy the text object after exiting the while loop
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Method to initialize a timer text with prefix and timer seconds setup
    /// </summary>
    /// <param name="prefix">string before number</param>
    /// <param name="countFrom">timer counts from n second</param>
    /// <param name="color">color of the text</param>
    public void PlayTimerText(string prefix, int countFrom, Color color)
    {
        // get text component of the object
        text = GetComponent<TMP_Text>();

        // setup color & message
        text.color = color;
        text.text = $"{prefix}{countFrom}";
        text.fontSize = Blackboard.TEXT_TIMER_SIZE;

        // start a coroutine to count down the timer
        StartCoroutine(TimerText(prefix, countFrom));
    }
    public void PlayTimerText(int countFrom, Color color)
    {
        PlayTimerText("", countFrom, color);
    }

    /// <summary>
    /// The coroutine function to handle timer text's counting
    /// </summary>
    /// <param name="prefix">string before number</param>
    /// <param name="countRemain">the remain seconds</param>
    /// <returns></returns>
    IEnumerator TimerText(string prefix, int countRemain)
    {
        // run until the countRemain reaches 0
        while (countRemain > 1)
        {
            // decrease countRemain and update text
            countRemain--;
            text.text = $"{prefix}{countRemain}";
            yield return new WaitForSeconds(1f);
        }

        // destroy the text object after exiting the while loop
        Destroy(this.gameObject);       
    }
}
