using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    // Start is called before the first frame update

    public Score score;
    public Text finalScoreText;
    public Text highScoreText;
    public InputField playerName;

    public List<string> highScoreName = new List<string>();
    public List<string> highScoreScore = new List<string>();

    void Start()
    {

        for (int i = 0; i < highScoreName.Count; i++)
        {
            highScoreText.text += highScoreName[i] + ": " + highScoreScore[i] + "\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        finalScoreText.text = "$" + score.money.ToString();




    }

    public void UpdateHighScore()
    {
 
                highScoreName.Add(playerName.text);
                highScoreScore.Add(score.money.ToString());

        for (int i = 0; i < highScoreName.Count; i++)
        {
            highScoreText.text += highScoreName[i] + ": " + highScoreScore[i] + "\n";
        }

    }
}
