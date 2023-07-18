using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToScene : MonoBehaviour
{
    public PlayerData PlayerData;

    public string travelPointId;
    public string sceneNameToGoBack;
    
    private string receivedLocationInfo;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SeparateSceneNameFromTravelPointId();
            SceneManager.LoadScene(sceneNameToGoBack);
        }
    }

    private void SeparateSceneNameFromTravelPointId()
    {
        string[] array = receivedLocationInfo.Split(' ');
        sceneNameToGoBack = array[1];
    }
}
