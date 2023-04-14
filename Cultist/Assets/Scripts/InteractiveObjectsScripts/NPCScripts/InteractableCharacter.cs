using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class InteractableCharacter : BaseInteractableObject
{
    public CharacterClass characterClass;

    private GameObject dialogueCanvas;
    public override void Start()
    {
        base.Start();
        objectName ??= characterClass.className;

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
    public override void Interact()
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
        GameObject loadedPanel = Resources.Load("DialoguePanel").GameObject();
        return loadedPanel;
    }
}
