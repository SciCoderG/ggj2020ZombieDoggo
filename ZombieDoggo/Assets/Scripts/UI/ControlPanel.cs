using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject controlPanel = null;


    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0.01f;

        StartCoroutine(startGame());
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(4f / 0.75f);

        controlPanel.SetActive(false);
        //Time.timeScale = 1;
    }
}
