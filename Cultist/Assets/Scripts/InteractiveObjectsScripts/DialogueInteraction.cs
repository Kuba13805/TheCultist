using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    private GameObject dialogueCanvas;

    private void Start()
    {
        try
        {
            dialogueCanvas = GameObject.Find("DialogueCanvas");
        }
        catch (Exception e)
        {
            Console.WriteLine(e + "Canvas cannot be found");
            throw;
        }
    }
    public void InteractWithObject()
    {
        DisplayDialoguePanel();
    }
    private void DisplayDialoguePanel()
    {
        var panelToDisplay = LoadPanelFromResources();
        Instantiate(panelToDisplay, dialogueCanvas.transform);
    }
    private static GameObject LoadPanelFromResources()
    {
        var loadedPanel = Resources.Load("DialoguePanel") as GameObject;
        return loadedPanel;
    }
}
