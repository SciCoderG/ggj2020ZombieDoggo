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

    

    private int currentPosition = 0;
    private int startPosition = 0;
    private int counter = 0;


    private static string highScorePrefsName = "highscores";
    private static int numHighscores = 10;
    private static string HighscoreNamePrefs { get { return highScorePrefsName + "-names"; } }

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


    public static int[] GetHighScores()
    {
        int[] highScores = PlayerPrefsX.GetIntArray(highScorePrefsName, 0, numHighscores);
        return highScores;
    }

    public static string[] GetHighScoreNames()
    {
        string[] highScorenames = PlayerPrefsX.GetStringArray(HighscoreNamePrefs, "", numHighscores);
        return highScorenames;
    }

    public static void UpdateHighscore(int[] highscore, string[] highscoreNames)
    {
        PlayerPrefsX.SetIntArray(highScorePrefsName, highscore);
        PlayerPrefsX.SetStringArray(HighscoreNamePrefs, highscoreNames);
    }

    private void OnDestroy()
    {
        int[] highScores = GetHighScores();
        string[] highScorenames = GetHighScoreNames();

        string randomName = RandomNames.GetRandomZombieName();
        
        for(int hIndex = 0; hIndex < highScores.Length; ++hIndex)
        {
            if(highScores[hIndex]< counter)
            {
                highScores = PushNewValueIntoArray(counter, hIndex, highScores);
                highScorenames = PushNewValueIntoArray(randomName, hIndex, highScorenames);
                break;
            }
        }

        UpdateHighscore(highScores, highScorenames);
    }

    private T[] PushNewValueIntoArray<T>(T newValue, int newIndex, T[] original)
    {
        T[] newHighscore = new T[original.Length];
        int index = 0;
        while(index < newIndex)
        {
            newHighscore[index] = original[index];
            index = index + 1;
        }
        newHighscore[newIndex] = newValue;
        index = newIndex + 1;
        while(index < original.Length)
        {
            newHighscore[index] = original[index - 1];
            index = index + 1;
        }
        return newHighscore;
    }

}
