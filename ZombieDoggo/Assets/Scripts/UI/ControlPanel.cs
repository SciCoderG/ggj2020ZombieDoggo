using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject controlPanel = null;

    [SerializeField]
    private GameObject hudPanel = null;

    private float pauseEndTime;

    // Start is called before the first frame update
    void Start()
    {
        hudPanel.SetActive(false);
        Time.timeScale = 0.01f;

        StartCoroutine(startGame());
    }
    
    IEnumerator startGame()
    {
        yield return new WaitForSeconds(Time.timeScale * 2);

        controlPanel.SetActive(false);
        hudPanel.SetActive(true);
        Time.timeScale = 1;
    }
}
