using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_ShowHidePanel : MonoBehaviour
{
    public KeyCode showPanel;
    public KeyCode hidePanel;
    public GameObject panel;
    bool isActive = false;

    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(showPanel) && !isActive)
        {
            panel.SetActive(true);
            isActive = true;
        }
        if (Input.GetKeyDown(hidePanel) && isActive == true)
        {
            panel.SetActive(false);
            isActive = false;
        }
    }
}
