using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHighscore : MonoBehaviour
{

    [SerializeField]
    private Text highscore;
    [SerializeField]
    private int maxHighscore = 0;


    // Start is called before the first frame update
    void Start()
    {
        highscore.text = "";


        for (int i = 0; i < maxHighscore; i++)
        {
            highscore.text += i.ToString() + ". " + Highscore.GetHighScoreNames().GetValue(i) + "  " + Highscore.GetHighScores().GetValue(i).ToString() + "\n"; 
        }
    }

}
