using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Management : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private string gameScene;

    void Start()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void openCreditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void quitApplication()
    {
        Application.Quit();
    }
}
