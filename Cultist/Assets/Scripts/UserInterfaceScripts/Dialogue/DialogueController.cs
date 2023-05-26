using System;
using System.Collections.Generic;
using System.Linq;
using Ink.Parsed;
using UnityEngine;
using Managers;
using Questlines;
using Questlines.SingleQuests;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Choice = Ink.Runtime.Choice;
using Story = Ink.Runtime.Story;

public class DialogueController : MonoBehaviour
{
    private DialogueInteraction _originConversationPoint;
    
    #region InkVariables

    private TextAsset _inkAsset;
    private Story _inkStory;
    private List<Choice> _listOfChoices;
    private List<string> _listOfCurrentTags;

    private List<QuestVariables> _listOfQuestVariables;
    
    #endregion
    
    #region GuiVariables
    private GameObject _dialoguePanel;

    private static GameObject _playerChoicesContainer;
    private GameObject _playerPortraitBox;
    private Sprite _playerPortraitSprite;
    
    
    private TextMeshProUGUI _npcTextBox;
    private GameObject _npcPortraitBox;
    private Sprite _npcPortraitSprite;
    
    private string _charName;
    

    #endregion


    #region Events

    public static event Action OnDialogueShown;
    
    public static event Action OnDialogueClosed;

    public static event Action<int, string> OnTestCheck;

    public static event Action<List<string>> OnCallVariables;

    public static event Action<List<QuestVariables>> OnSetNewVariables;

    public static event Action<QuestId> OnQuestStart;

    public static event Action<QuestId> OnQuestComplete; 

    #endregion

    private void Awake()
    {
        InputManager.Instance.PlayerInputActions.UI.SkipConversation.performed += NextDialogue;
        
        DialogueSendChoice.OnChoiceSubmitted += SubmitChoice;
        
        GameManager.OnPlayerTestCheck += ReturnTestResult;
        
        DialogueInteraction.OnDialogueCall += Initialize;

        QuestLog.OnQuestVariablesReturn += LoadQuestVariablesToDialogue;
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
        
        QuestLog.OnQuestVariablesReturn -= LoadQuestVariablesToDialogue;
    }

    private void Initialize(string objectName, DialogueInteraction dialogueInteraction)
    {

        _dialoguePanel = gameObject;
        
        InitializeStoryUIBoxes(objectName, dialogueInteraction);
        
        InitializeStory(dialogueInteraction);
        
        CallForVariable();

        StartDialogue();
    }

    private void InitializeStoryUIBoxes(string objectName, Component dialogueInteraction)
    {
        _playerChoicesContainer = FindPlayerChoiceContent();
        _playerPortraitBox = FindUiElement("PlayerPortrait");
        _playerPortraitSprite = GameManager.Instance.playerData.playerPortrait;

        _playerPortraitBox.GetComponentInChildren<Image>().sprite = _playerPortraitSprite;
        
        _charName = objectName;
        _npcTextBox = FindNpcTextBox();
        _npcPortraitBox = FindUiElement("NPCPortrait");
        _npcPortraitSprite = dialogueInteraction.GetComponent<InteractableCharacter>().characterPortrait;

        if (_npcPortraitSprite == null)
        {
            _npcPortraitBox.SetActive(false);
        }
        else
        {
            _npcPortraitBox.GetComponentInChildren<Image>().sprite = _npcPortraitSprite;
        }
    }

    #region ControllStory
    private void InitializeStory(DialogueInteraction dialogueInteraction)
    {
        _inkAsset = dialogueInteraction.dialogueAsset;
        _inkStory = new Story(_inkAsset.text);
        HandleInkError();
        
        _originConversationPoint = dialogueInteraction;
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
        }
        if (!_inkStory.canContinue && _inkStory.currentChoices.Count == 0 || _npcTextBox.text == "")
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
        if (!_inkStory.canContinue) return;
        
        _npcTextBox.text = $"<color=yellow>{_charName}:</color> " + _inkStory.Continue();
        HandleQuestManagement();
        // if (_inkStory.currentTags != null)
        // {
        //     foreach (var VARIABLE in _inkStory.currentTags)
        //     {
        //         var strings = VARIABLE.Split(':');
        //         
        //         Debug.Log(strings[0]);
        //     }
        // }
    }

    private void SubmitChoice(Choice choice)
    {
        var wasPlayerTested = TestPlayerWithChoice(choice);

        _inkStory.ChooseChoiceIndex(_listOfChoices.IndexOf(choice));
        ClearChoices(_playerChoicesContainer.transform.GetComponentsInChildren<DialogueSendChoice>());
        if (_inkStory.canContinue)
        {
            ContinueStory();
            if (wasPlayerTested && _inkStory.canContinue)
            {
                ContinueStory();
            }
        }
        DisplayInfoToClick();
    }

    private void EndDialogue()
    {
        SendNewVariablesToDialogueLog();
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
    private void DisplayChoices(List<Choice> list)
    {
        foreach (var choice in list)
        {
            var dialogueOption = DisplayDialogueOption();

            var testToPassInfo = AddTestInfoToChoice(choice);
            
            LoadDialogueOptionContent(dialogueOption, choice.index, testToPassInfo + choice.text);
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
        var container = FindUiElement("PlayerChoices");
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
        var container = FindUiElement("PlayerChoices");
        if (container != null)
        {
            container.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }
    #endregion

    #region DialogueLogic

    private void HandleQuestManagement()
    {
        var currentTags = CheckForQuestTags(_inkStory.currentTags);
        var questIdToReturn = new QuestId();
        
        foreach (var tagContent in currentTags.Select(questTag => questTag.Split(':', '_')))
        {
            questIdToReturn.idPrefix = tagContent[1];
            questIdToReturn.questNumber = int.Parse(tagContent[2]);
            switch (tagContent[0])
            {
                case "questStart":
                    Debug.Log("Quest started: " + questIdToReturn);
                    OnQuestStart?.Invoke(questIdToReturn);
                    break;
                case "questComplete":
                    Debug.Log("Quest completed: " + questIdToReturn);
                    OnQuestComplete?.Invoke(questIdToReturn);
                    break;
            }
        }
    }

    private List<string> CheckForQuestTags(List<string> currentTagsAtContinue)
    {
        List<string> list = new List<string>();
        
        foreach (var questTag in currentTagsAtContinue)
        {
            if (questTag.Contains("quest")) list.Add(questTag);
        }

        return list;
    }
    private string CheckForTagsAtChoice(Choice choice)
    {
        _listOfCurrentTags = choice.tags;
        
        return _listOfCurrentTags == null ? "No tags" : _listOfCurrentTags[0];
    }

    private bool TestPlayerWithChoice(Choice choice)
    {
        var testTag = CheckForTagsAtChoice(choice);
        if (testTag == "No tags") return false;
        
        var array = testTag.Split(":");
        
        OnTestCheck?.Invoke(int.Parse(array[1]), array[0]);

        return true;
    }

    private string AddTestInfoToChoice(Choice choice)
    {
        var testTag = CheckForTagsAtChoice(choice);
        var array = testTag.Split(":");

        var stringToReturn = "[" + array[0] + "] ";

        if (array[0] == "No tags")
        {
            stringToReturn = "";
        }

        return stringToReturn;
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

    #region HandleQuestsInDialogue

    #region GetVariablesFromDialogueLog
    private void CallForVariable()
    {
        var listOfVariables = LoadNeededVariablesFromDialogue();
        
        OnCallVariables?.Invoke(listOfVariables);
    }

    private List<string> LoadNeededVariablesFromDialogue()
    {
        var listOfNeededVariables = new List<string>();
        
        foreach (var inkVariable in _inkStory.variablesState)
        {
            listOfNeededVariables.Add(inkVariable);
        }

        return listOfNeededVariables;
    }
    private void LoadQuestVariablesToDialogue(List<QuestVariables> listOfReturnQuestVariables)
    {
        _listOfQuestVariables = new List<QuestVariables>();
        
        _listOfQuestVariables = listOfReturnQuestVariables;
        
        foreach (var variable in _listOfQuestVariables)
        {
            var variableContent = variable.conditionPassed;

            _inkStory.variablesState[variable.variableName] = variableContent;
            
            Debug.Log("Variable with name: " + variable.variableName + " has been passed to story!");
        }
        SaveStoryState();
        LoadStoryState(_originConversationPoint.dialogueSaved);
    }
    #endregion

    #region SetNewVariables

    private void SendNewVariablesToDialogueLog()
    {
        var questVariablesList = new List<QuestVariables>();
        
        var listOfVariables = LoadNeededVariablesFromDialogue();

        foreach (var variable in listOfVariables)
        {
            var newVariableToSet = new QuestVariables
            {
                variableName = variable,
                conditionPassed = (bool)_inkStory.variablesState[variable]
            };

            questVariablesList.Add(newVariableToSet);
        }
        
        OnSetNewVariables?.Invoke(questVariablesList);
    }
    

    #endregion

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
    private GameObject FindUiElement(string elementToFind)
    {
        return _dialoguePanel.transform.Find(elementToFind).gameObject;
    }
    #endregion
}
