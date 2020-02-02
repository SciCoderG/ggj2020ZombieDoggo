using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject controlPanel = null;

    [SerializeField]
    private GameObject hudPanel = null;

    // Start is called before the first frame update
    void Start()
    {
        hudPanel.SetActive(false);
        Time.timeScale = 0;
        StartCoroutine(startGame());

    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(2f);
        controlPanel.SetActive(false);
        hudPanel.SetActive(true);
        Time.timeScale = 1;

    }
}
