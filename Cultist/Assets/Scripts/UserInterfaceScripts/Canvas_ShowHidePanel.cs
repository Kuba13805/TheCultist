using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Canvas_ShowHidePanel : MonoBehaviour
{
    public KeyCode showPanel;
    public KeyCode hidePanel;
    public GameObject panel;
    public bool isActive = false;

    void Start()
    {
        panel.GameObject().SetActive(false);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(showPanel) && !isActive)
        {
            panel.GameObject().SetActive(true);
            isActive = true;
            PauseGame();
        }
        if (Input.GetKeyDown(hidePanel) && isActive == true)
        {
            panel.GameObject().SetActive(false);
            isActive = false;
            ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
