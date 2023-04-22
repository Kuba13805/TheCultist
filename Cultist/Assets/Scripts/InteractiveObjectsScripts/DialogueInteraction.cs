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
    private TextMeshProUGUI playerChoiceTextBox;

    private string charName;

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
        charName = GetComponent<InteractableCharacter>().objectName;
        npcTextBox.text = $"<color=yellow>{charName}</color>: " + inkStory.Continue();
    }
    private void NextDialogue(InputAction.CallbackContext context)
    {
        if (inkStory.canContinue && inkStory.currentChoices.Count <= 0)
        {
            npcTextBox.text = $"<color=yellow>{charName}</color>: " + inkStory.Continue();
        }

        if (inkStory.currentChoices.Count <= 0) return;
        playerChoiceTextBox.text = "";
        foreach (var choice in inkStory.currentChoices)
        {
            playerChoiceTextBox.text += (choice.index + 1 ) + ". "+ choice.text + "\n";
        }
    }

    #region DisplayDialoguePanel
    public void InteractWithObject()
    {
        DisplayDialoguePanel();
        OnDialogueShown?.Invoke();
        
        npcTextBox = FindNpcTextBox();
        playerChoiceTextBox = FindPlayerChoiceTexBox();
        
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

    #region FindPanelElements
    private TextMeshProUGUI FindNpcTextBox()
    {
        return dialoguePanel.transform.Find("NPCText").GetComponentInChildren<TextMeshProUGUI>();
    }

    private TextMeshProUGUI FindPlayerChoiceTexBox()
    {
        return dialoguePanel.transform.Find("PlayerChoices").GetComponentInChildren<TextMeshProUGUI>();
    }
    #endregion

    private void OnDestroy()
    {
        InputManager.Instance.PlayerInputActions.UI.SkipConversation.performed -= NextDialogue;
    }
}
