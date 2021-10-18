using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenuCanvas;
    [SerializeField] private bool isOptionsMenuOn = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchOptionsMenuCanvas();
        }
        
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SwitchOptionsMenuCanvas()
    {
        if (isOptionsMenuOn)
        {
            optionsMenuCanvas.SetActive(false);
            isOptionsMenuOn = false;
            Time.timeScale = 1;
        }
        else if (!isOptionsMenuOn)
        {
            optionsMenuCanvas.SetActive(true);
            isOptionsMenuOn = true;
            Time.timeScale = 0;
        }
    }
}
