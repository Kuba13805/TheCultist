using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Managers;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    private GameObject dialoguePanel;
    private static GameObject dialogueCanvas;
    
    private TextAsset inkAsset;
    private Story inkStory;
    
    private TextMeshProUGUI npcTextBox;
    private static GameObject playerChoicesContainer;
    
    private string charName;
    
    #region Events

    public static event Action OnDialogueShown;
    
    public static event Action OnDialogueClosed;

    #endregion
    //
    // private void Awake()
    // {
    //     InputManager.Instance.PlayerInputActions.UI.SkipConversation.performed += NextDialogue;
    //     
    //     try
    //     {
    //         dialogueCanvas = GameObject.Find("DialogueCanvas");
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e + "Canvas cannot be found");
    //         throw;
    //     }
    // }

    private void OnDestroy()
    {
        OnDialogueClosed?.Invoke();
        
        InputManager.Instance.PlayerInputActions.UI.SkipConversation.performed -= NextDialogue;
    }

    public void Initialize(TextAsset newInkAsset, string objectName)
    {
        InputManager.Instance.PlayerInputActions.UI.SkipConversation.performed += NextDialogue;
        
        try
        {
            dialogueCanvas = GameObject.Find("DialogueCanvas");
        }
        catch (Exception e)
        {
            Console.WriteLine(e + "Canvas cannot be found");
            throw;
        }
        
        dialoguePanel = DisplayDialoguePanel();
        OnDialogueShown?.Invoke();

        inkAsset = newInkAsset;
        
        npcTextBox = FindNpcTextBox();
        playerChoicesContainer = FindPlayerChoiceContent();

        StartDialogue(objectName);
    }

    private void StartDialogue(string objectName)
    {
        inkStory = new Story(inkAsset.text);

        charName = objectName;
        npcTextBox.text = $"<color=yellow>{charName}</color>: " + inkStory.Continue();
    }
    private void NextDialogue(InputAction.CallbackContext context)
    {
        if (inkStory.canContinue)
        {
            npcTextBox.text = $"<color=yellow>{charName}</color>: " + inkStory.Continue();
        }

        if (inkStory.currentChoices.Count <= 0 || playerChoicesContainer.transform.childCount != 0) return;
        foreach (var choice in inkStory.currentChoices)
        {
            var dialogueOption = DisplayDialogueOption();
            
            LoadDialogueOptionContent(dialogueOption, choice.index, choice.text);
        }
    }

    private void EndDialogue()
    {
        
    }

    #region DisplayDialoguePanel
    private static void LoadDialogueOptionContent(GameObject dialogueOptionButton, int optionIndex, string optionText)
    {
        optionIndex += 1;
        dialogueOptionButton.GetComponentInChildren<TextMeshProUGUI>().text = optionIndex + ". " + optionText;
    }
    private static GameObject DisplayDialoguePanel()
    {
        var panelToDisplay = LoadPrefab("DialoguePanel");
        return Instantiate(panelToDisplay, dialogueCanvas.transform);
    }

    private static GameObject DisplayDialogueOption()
    {
        var buttonToLoad = LoadPrefab("DialogueOptionButton");
        return Instantiate(buttonToLoad, playerChoicesContainer.transform);
    }
    private static GameObject LoadPrefab(string nameOfPrefabToLoad)
    {
        var loadedPrefab = Resources.Load(nameOfPrefabToLoad) as GameObject;
        return loadedPrefab;
    }
    #endregion
    
    #region FindPanelElements
    private TextMeshProUGUI FindNpcTextBox()
    {
        return dialoguePanel.transform.Find("NPCText").GetComponentInChildren<TextMeshProUGUI>();
    }

    private GameObject FindPlayerChoiceContent()
    {
        return dialoguePanel.transform.Find("PlayerChoices").Find("Viewport").Find("PlayerChoicesContent").gameObject;
    }
    #endregion
}
