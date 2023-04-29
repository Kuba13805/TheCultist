using System;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using TMPro;
using UnityEngine.InputSystem;
using Choice = Ink.Runtime.Choice;
using Story = Ink.Runtime.Story;

public class DialogueController : MonoBehaviour
{
    private DialogueInteraction _originConversationPoint;

    private GameObject _dialoguePanel;

    private TextAsset _inkAsset;
    private Story _inkStory;
    private List<Choice> _listOfChoices;
    private List<string> _listOfCurrentTags;

    private TextMeshProUGUI _npcTextBox;
    private static GameObject _playerChoicesContainer;
    
    private string _charName;

    #region Events

    public static event Action OnDialogueShown;
    
    public static event Action OnDialogueClosed;

    public static event Action<int, string> OnTestCheck;

    #endregion

    private void Awake()
    {
        InputManager.Instance.PlayerInputActions.UI.SkipConversation.performed += NextDialogue;
        
        DialogueSendChoice.OnChoiceSubmitted += SubmitChoice;
        
        GameManager.OnPlayerTestCheck += ReturnTestResult;
        
        DialogueInteraction.OnDialogueCall += Initialize;
    }

    private void Start()
    {
        OnDialogueShown?.Invoke();
    }

    private void OnDestroy()
    {
        
        OnDialogueClosed?.Invoke();
        
        InputManager.Instance.PlayerInputActions.UI.SkipConversation.performed -= NextDialogue;
        
        DialogueSendChoice.OnChoiceSubmitted -= SubmitChoice;
        
        GameManager.OnPlayerTestCheck -= ReturnTestResult;
        
        DialogueInteraction.OnDialogueCall -= Initialize;
    }

    private void Initialize(string objectName, DialogueInteraction dialogueInteraction)
    {
        _charName = objectName;

        _dialoguePanel = gameObject;
        
        InitializeStory(dialogueInteraction);

        _npcTextBox = FindNpcTextBox();
        _playerChoicesContainer = FindPlayerChoiceContent();

        StartDialogue();
    }

    #region ControllStory
    private void InitializeStory(DialogueInteraction dialogueInteraction)
    {
        _inkAsset = dialogueInteraction.dialogueAsset;
        _inkStory = new Story(_inkAsset.text);
        HandleInkError();
        
        _originConversationPoint = dialogueInteraction;
        
        if (_originConversationPoint.oneTimeConversation) return;

        if (!string.IsNullOrEmpty(_originConversationPoint.dialogueSaved))
        {
            LoadStoryState(_originConversationPoint.dialogueSaved);
            Debug.Log("Loaded");
        }
        else
        {
            SaveStoryState();
            Debug.Log("Saved");
        }
    }

    private void StartDialogue()
    {
        ContinueStory();
        DisplayInfoToClick();
    }
    private void NextDialogue(InputAction.CallbackContext context)
    {
        if (_inkStory.canContinue && _inkStory.currentChoices.Count == 0)
        {
            ContinueStory();
            if (_npcTextBox.text == "")
            {
                ContinueStory();
            }
        }
        if (!_inkStory.canContinue && _inkStory.currentChoices.Count == 0)
        {
            EndDialogue();
        }
        if (_inkStory.currentChoices.Count == 0 || _playerChoicesContainer.transform.childCount != 0) return;

        _listOfChoices?.RemoveRange(0, _listOfChoices.Count);
        ClearInfoToClick();
        _listOfChoices = _inkStory.currentChoices;
        DisplayChoices(_listOfChoices);
    }

    private void ContinueStory()
    {
        if (_inkStory.canContinue)
        {
            _npcTextBox.text = $"<color=yellow>{_charName}:</color> " + _inkStory.Continue();
        }
    }
    private void SubmitChoice(Choice choice)
    {
        TestPlayerWithChoice(choice);
        
        _inkStory.ChooseChoiceIndex(_listOfChoices.IndexOf(choice));
        ClearChoices(_playerChoicesContainer.transform.GetComponentsInChildren<DialogueSendChoice>());
        if (_inkStory.canContinue)
        {
            ContinueStory();
        }
        DisplayInfoToClick();
    }

    private void EndDialogue()
    {
        SaveStoryState();
        Destroy(_dialoguePanel.gameObject);
    }

    private void LoadStoryState(string storyStateToLoad)
    {
        _inkStory.state.LoadJson(storyStateToLoad);
    }

    private void SaveStoryState()
    {
        _originConversationPoint.dialogueSaved = _inkStory.state.ToJson();
    }
    #endregion

    #region DisplayDialoguePanel
    private static void LoadDialogueOptionContent(GameObject dialogueOptionButton, int optionIndex, string optionText)
    {
        optionIndex += 1;
        dialogueOptionButton.GetComponentInChildren<TextMeshProUGUI>().text = optionIndex + ". " + optionText;
    }

    private static GameObject DisplayDialogueOption()
    {
        var buttonToLoad = LoadPrefab("DialogueOptionButton");
        return Instantiate(buttonToLoad, _playerChoicesContainer.transform);
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
    private static void ClearChoices(IEnumerable<DialogueSendChoice> buttonArray)
    {
        foreach (var option in buttonArray)
        {
            Destroy(option.gameObject);
        }
    }
    private void DisplayInfoToClick()
    {
        var container = FindPlayerChoicesGlobalContainer();
        var buttonToClick = InputManager.Instance.PlayerInputActions.UI.SkipConversation.bindings[0].path;

        var array =buttonToClick.Split('/');
        
        var nameOfButtonToClick = buttonToClick.Contains("<Keyboard>/") ? array[^1] : buttonToClick;

        if (container != null)
        {
            container.GetComponentInChildren<TextMeshProUGUI>().text = $"Click {nameOfButtonToClick} to continue.";
        }
    }

    private void ClearInfoToClick()
    {
        var container = FindPlayerChoicesGlobalContainer();
        if (container != null)
        {
            container.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }
    #endregion

    #region DialogueLogic
    private string CheckForTags(Choice choice)
    {
        _listOfCurrentTags = choice.tags;
        return _listOfCurrentTags == null ? "No tags" : _listOfCurrentTags[0];
    }

    private void TestPlayerWithChoice(Choice choice)
    {
        var testTag = CheckForTags(choice);
        if (testTag == "No tags") return;
        
        var array = testTag.Split(":");
        
        OnTestCheck?.Invoke(int.Parse(array[1]), array[0]);
    }

    private void ReturnTestResult(bool result)
    {
        _inkStory.variablesState["test_passed"] = result;
    }
    private void HandleInkError()
    {
        _inkStory.onError += (msg, type) =>
        {
            if (type == Ink.ErrorType.Warning)
                Debug.LogWarning(msg);
            else
                Debug.LogError(msg);
        };
    }
    #endregion
    
    #region FindPanelElements
    private TextMeshProUGUI FindNpcTextBox()
    {
        return _dialoguePanel.transform.Find("NPCText").GetComponentInChildren<TextMeshProUGUI>();
    }
    private GameObject FindPlayerChoiceContent()
    {
        return _dialoguePanel.transform.Find("PlayerChoices").Find("Viewport").Find("PlayerChoicesContent").gameObject;
    }

    private GameObject FindPlayerChoicesGlobalContainer()
    {
        return _dialoguePanel.transform.Find("PlayerChoices").gameObject;
    }
    #endregion
}
