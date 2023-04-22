using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Managers;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueInteraction : MonoBehaviour
{
    private GameObject dialoguePanel;
    private GameObject dialogueCanvas;
    
    [SerializeField] private TextAsset inkAsset;
    private Story inkStory;

    private TextMeshProUGUI npcTextBox;

    #region Events

    public static event Action OnDialogueShown;
    

    #endregion

    private void Start()
    {
        InputManager.Instance.PlayerInputActions.UI.SkipConversation.performed += NextDialogue;
        
        inkStory = new Story(inkAsset.text);

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

    private void StartDialogue()
    {
        npcTextBox.text = inkStory.Continue();
    }
    private void NextDialogue(InputAction.CallbackContext context)
    {
        if (inkStory.canContinue)
        {
            npcTextBox.text = inkStory.Continue();
        }
    }

    private TextMeshProUGUI FindNpcTextBox()
    {
        return dialoguePanel.transform.Find("NPCText").GetComponentInChildren<TextMeshProUGUI>();
    }
    #region DisplayDialoguePanel
    public void InteractWithObject()
    {
        DisplayDialoguePanel();
        OnDialogueShown?.Invoke();
        
        npcTextBox = FindNpcTextBox();
        
        StartDialogue();
    }
    private void DisplayDialoguePanel()
    {
        var panelToDisplay = LoadPanelFromResources();
        dialoguePanel = Instantiate(panelToDisplay, dialogueCanvas.transform);
        
    }
    private static GameObject LoadPanelFromResources()
    {
        var loadedPanel = Resources.Load("DialoguePanel") as GameObject;
        return loadedPanel;
    }
    #endregion

    private void OnDestroy()
    {
        InputManager.Instance.PlayerInputActions.UI.SkipConversation.performed -= NextDialogue;
    }
}
