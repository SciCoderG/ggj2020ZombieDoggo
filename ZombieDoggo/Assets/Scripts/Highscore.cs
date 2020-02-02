using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    [SerializeField]
    private GameObject zombie = null;
    [SerializeField]
    private Text textMeters = null;
    [SerializeField]
    private string highScorePrefsName = "Highscores";
    [SerializeField]
    private int numHighscores = 10;

    private int currentPosition = 0;
    private int startPosition = 0;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = (int)zombie.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = (int)zombie.transform.position.z;

        counter = (currentPosition - startPosition)/3;
        textMeters.text = counter.ToString() + " m";
    }

    private void OnDestroy()
    {
        int[] highScores = PlayerPrefsX.GetIntArray(highScorePrefsName, 0, numHighscores);
        for(int i = 0; i < highScores.Length; ++i)
        {
            if(highScores[i]< counter)
            {

            }
        }
    }
}
