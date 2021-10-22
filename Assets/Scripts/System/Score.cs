using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is where money and reputation stats are held.
/// Define money and reputation
/// Contributor: Grace
/// </summary>

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int money = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "$" + money.ToString();
    }

    public void Money(int paidMoney)
    {
        money += paidMoney; 
    }
}
