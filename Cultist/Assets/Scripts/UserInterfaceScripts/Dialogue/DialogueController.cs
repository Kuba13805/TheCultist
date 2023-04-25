using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Managers;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    private GameObject dialoguePanel;
    private static GameObject dialogueCanvas;
    
    private TextAsset inkAsset;
    private Story inkStory;
    private List<Choice> _listOfChoices;

    private TextMeshProUGUI npcTextBox;
    private static GameObject playerChoicesContainer;
    
    private string charName;
    
    #region Events

    public static event Action OnDialogueShown;
    
    public static event Action OnDialogueClosed;

    #endregion
    private void OnDestroy()
    {
        
        OnDialogueClosed?.Invoke();
        
        InputManager.Instance.PlayerInputActions.UI.SkipConversation.performed -= NextDialogue;
        
        DialogueSendChoice.OnChoiceSubmitted -= SubmitChoice;
    }

    public void Initialize(TextAsset newInkAsset, string objectName)
    {
        DialogueSendChoice.OnChoiceSubmitted += SubmitChoice;
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
        inkStory = new Story(inkAsset.text);
        charName = objectName;
        
        npcTextBox = FindNpcTextBox();
        playerChoicesContainer = FindPlayerChoiceContent();
        
        StartDialogue();
    }

    #region ControllStory
    private void StartDialogue()
    {
        npcTextBox.text = $"<color=yellow>{charName}:</color> " + inkStory.Continue();
        
        DisplayInfoToClick();
    }
    private void NextDialogue(InputAction.CallbackContext context)
    {
        if (inkStory.canContinue && inkStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }

        if (!inkStory.canContinue && inkStory.currentChoices.Count == 0)
        {
            EndDialogue();
        }
        
        if (inkStory.currentChoices.Count == 0 || playerChoicesContainer.transform.childCount != 0) return;
        
        if (_listOfChoices != null)
        {
            _listOfChoices.RemoveRange(0, _listOfChoices.Count - 1);
        }
        ClearInfoToClick();
        _listOfChoices = inkStory.currentChoices;
        DisplayChoices(_listOfChoices);
    }

    private void ContinueStory()
    {
        npcTextBox.text = $"<color=yellow>{charName}</color>: " + inkStory.Continue();
    }
    private void SubmitChoice(Choice choice)
    {
        inkStory.ChooseChoiceIndex(_listOfChoices.IndexOf(choice));
        ClearChoices(playerChoicesContainer.transform.GetComponentsInChildren<DialogueSendChoice>());
        ContinueStory();
        DisplayInfoToClick();
    }

    private void EndDialogue()
    {
        Destroy(dialoguePanel.gameObject);
    }

    private static void ClearChoices(IEnumerable<DialogueSendChoice> buttonArray)
    {
        foreach (var option in buttonArray)
        {
            Destroy(option.gameObject);
        }
    }
    #endregion

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
    private static void DisplayChoices(List<Choice> list)
    {
        foreach (var choice in list)
        {
            var dialogueOption = DisplayDialogueOption();
            
            LoadDialogueOptionContent(dialogueOption, choice.index, choice.text);
            dialogueOption.GetComponent<DialogueSendChoice>().choice = choice;
        }
    }

    private void DisplayInfoToClick()
    {
        var container = FindPlayerChoicesGlobalContainer();
        var buttonToClick = InputManager.Instance.PlayerInputActions.UI.SkipConversation.bindings[0].path;

        var array =buttonToClick.Split('/');
        
        var nameOfButtonToClick = buttonToClick.Contains("<Keyboard>/") ? array[^1] : buttonToClick;

        container.GetComponentInChildren<TextMeshProUGUI>().text = $"Click {nameOfButtonToClick} to continue.";
    }

    private void ClearInfoToClick()
    {
        var container = FindPlayerChoicesGlobalContainer();
        container.GetComponentInChildren<TextMeshProUGUI>().text = "";
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

    private GameObject FindPlayerChoicesGlobalContainer()
    {
        return dialoguePanel.transform.Find("PlayerChoices").gameObject;
    }
    #endregion
}
