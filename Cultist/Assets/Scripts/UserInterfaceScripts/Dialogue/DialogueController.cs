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
        npcTextBox.text = $"<color=yellow>{charName}</color>: " + inkStory.Continue();
    }
    private void NextDialogue(InputAction.CallbackContext context)
    {
        if (inkStory.canContinue && inkStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }
        
        if (inkStory.currentChoices.Count == 0 || playerChoicesContainer.transform.childCount != 0) return;
        if (_listOfChoices != null)
        {
            _listOfChoices.RemoveRange(0, _listOfChoices.Count - 1);
        }
        _listOfChoices = inkStory.currentChoices;
        DisplayChoices(inkStory, _listOfChoices);
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

    private static void DisplayChoices(Story story, List<Choice> list)
    {
        foreach (var choice in list)
        {
            var dialogueOption = DisplayDialogueOption();
            
            LoadDialogueOptionContent(dialogueOption, choice.index, choice.text);
            dialogueOption.GetComponent<DialogueSendChoice>().choice = choice;
            
            Debug.Log(choice.index);
        }
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
