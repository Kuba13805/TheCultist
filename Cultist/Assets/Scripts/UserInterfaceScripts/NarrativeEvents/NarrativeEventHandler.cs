using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Events;
using Ink.Runtime;
using Managers;
using Questlines.SingleQuests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeEventHandler : MonoBehaviour
{
    [SerializeField] private GameObject narrativeEventPanel;
    
    [SerializeField] private GameObject eventImageBox;
    
    [SerializeField] private GameObject eventTextBox;
    
    [SerializeField] private GameObject eventChoicesBox;

    [SerializeField] private GameObject choicePrompt;

    private List<BaseItem> _currentEventItems;

    private TextAsset _currentEventTextAsset;

    private Story _currentStory;

    #region Events

    public static event Action OnNarrativeEventClose;

    #endregion

    private void Start()
    {
        QuestManager.OnNarrativeEventPass += ShowEvent;

        NarrativeEventChoice.OnChoiceChoosen += ChooseChoice;
    }

    private void OnDisable()
    {
        QuestManager.OnNarrativeEventPass -= ShowEvent;

        NarrativeEventChoice.OnChoiceChoosen -= ChooseChoice;
    }

    #region EventDisplaying
    private void ShowEvent(NarrativeEvent newEvent)
    {
        narrativeEventPanel.SetActive(true);
        
        CallGameManagerEvents.StopGame(true);
        
        CallPlayerInputChange.SetAllInput(false);
        
        CallPlayerInputChange.SetUiActions(true);
        
        
        _currentEventTextAsset = newEvent.narrativeEventText;

        _currentEventItems = newEvent.itemsInEvent;
        
        InitializeEventStory();

        if (newEvent.eventImages.Count > 0)
        {
            UpdateImage(newEvent.eventImages[0]);
        }
    }

    private void UpdateImage(Sprite newImage)
    {
        eventImageBox.GetComponent<Image>().sprite = newImage;
    }

    private void InitializeEventStory()
    {
        _currentStory = new Story(_currentEventTextAsset.text);
        
        DisplayText();
    }

    private void DisplayText()
    {
        if (!_currentStory.canContinue) return;
        
        eventTextBox.GetComponent<TextMeshProUGUI>().text = _currentStory.Continue();
        
        SearchForCommand();
        
        DisplayChoices();

        if (eventTextBox.GetComponent<TextMeshProUGUI>().text == "")
        {
            CloseEvent();
        }
    }

    private void DisplayChoices()
    {
        if (_currentStory.currentChoices.Count <= 0) return;
        
        foreach (var choice in _currentStory.currentChoices)
        {
            SearchForChoiceFlag(choice);
        }
    }
    private void CreateChoicePrompt(Choice choice)
    {
        
        var newChoicePrompt = Instantiate(choicePrompt, eventChoicesBox.transform);

        newChoicePrompt.GetComponent<NarrativeEventChoice>().InitializeChoice(choice);
    }

    private void ChooseChoice(int index)
    {
        _currentStory.ChooseChoiceIndex(index);

        ClearChoices();
            
        DisplayText();
    }

    private void ClearChoices()
    {
        foreach (var prompt in eventChoicesBox.GetComponentsInChildren<Image>())
        {
            Destroy(prompt.gameObject);
        }
    }

    private void CloseEvent()
    {
        narrativeEventPanel.SetActive(false);
        
        OnNarrativeEventClose?.Invoke();
        
        CallPlayerInputChange.SetPlayerActions(true);
        
        CallPlayerInputChange.SetCameraActions(true);
        
        CallGameManagerEvents.StopGame(false);
    }
    #endregion

    #region EventFunctionsLogic
    

    #region ChoiceFlags
    private void SearchForChoiceFlag(Choice choice)
    {
        var conditionsPassed = new List<bool>();

        if (choice.tags == null)
        {
            CreateChoicePrompt(choice);
            return;
        }
        foreach (var flag in choice.tags)
        {
            var array = flag.Split(":");
            
            if(!array[0].Contains("flag")) continue;
            
            var choiceCanBeSeen = false;
            
            switch (array[1])
            {
                case "hasItem":
                    StartCoroutine(CheckForItemInInventory(ReturnItemFromList(int.Parse(array[2])),
                        result => { choiceCanBeSeen = result;}));
                    break;
                    
                case "statValue":
                    StartCoroutine(CheckForStatValue(array[2], int.Parse(array[3]), 
                        result => { choiceCanBeSeen = result; }));
                    break;
                    
                case "hasTag":
                    break;
                
                case "questStatus":
                    break;
                
                case "questVariableStatus":
                    break;
                    
            }
            conditionsPassed.Add(choiceCanBeSeen);
        }

        if (conditionsPassed.Any(condition => condition == false))
        {
            return;
        }
        
        CreateChoicePrompt(choice);
    }
    private IEnumerator CheckForQuestVariable()
    {
        throw new NotImplementedException();
    }

    private IEnumerator CheckForQuestStatus()
    {
        throw new NotImplementedException();
    }

    private IEnumerator CheckForTag()
    {
        throw new NotImplementedException();
    }

    private IEnumerator CheckForStatValue(string statToCheck, int neededStatValue, Action<bool> onResult)
    {
        var statOnNeededValue = false;
        var isProcessing = true;

        var playerEvent = new PlayerEvents();

        GameManager.OnReturnStatValue += ReturnStatValue;
        
        playerEvent.CheckForStatValue(ParseStringToStat(statToCheck));

        while (isProcessing)
        {
            yield return null;
        }
        
        GameManager.OnReturnStatValue -= ReturnStatValue;

        onResult(statOnNeededValue);
        yield break;
        
        void ReturnStatValue(int statValue)
        {
            statOnNeededValue = statValue >= neededStatValue;

            isProcessing = false;
        }

        Stat ParseStringToStat(string statName)
        {
            Enum.TryParse(statName, out Stat statToFind);

            return statToFind;
        }
    }

    private IEnumerator CheckForItemInInventory(BaseItem itemToCheck, Action<bool> onResult)
    {
        var itemInInventory = false;
        var isProcessing = true;

        var playerEvent = new PlayerEvents();

        GameManager.OnReturnQuantityOfItems += ReturnItemStatus;

        playerEvent.CheckForItem(itemToCheck);
        
        while (isProcessing)
        {
            yield return null;
        }

        GameManager.OnReturnQuantityOfItems -= ReturnItemStatus;
        
        onResult(itemInInventory);
        yield break;

        void ReturnItemStatus(int quantity)
        {
            itemInInventory = quantity > 0;

            Debug.Log("Item: " + itemToCheck.itemName + ". Is in inventory: " + itemInInventory);
            
            isProcessing = false;
        }
    }
    
    private BaseItem ReturnItemFromList(int itemId)
    {
        foreach (var item in _currentEventItems.Where(item => item.itemId == itemId))
        {
            return item;
        }

        return null;
    }
    #endregion

    #region ChoiceCommands
    private void SearchForCommand()
    {
        if (_currentStory.currentTags == null) return;
        
        var commandList = _currentStory.currentTags;

        foreach (var array in commandList.Select(command => command.Split(':')))
        {
            if (!array[0].Contains("command") || array[0] == null) return;
            
            switch (array[1])
            {
                case "loadLevel":
                    LoadLevelFromNarrativeEvent(array[2]);
                    break;
                
                case "addItem":
                    AddItemFromNarrativeEvent(int.Parse(array[2]), int.Parse(array[3]));
                    break;
                
                case "removeItem":
                    RemoveItemFromNarrativeEvent(int.Parse(array[2]), int.Parse(array[3]));
                    break;
                case "startQuest":
                    StartQuest(array[2], int.Parse(array[3]));
                    break;
                
                case "completeQuest":
                    CompleteQuest(array[2], int.Parse(array[3]));
                    break;
                
                case "failQuest":
                    FailQuest(array[2], int.Parse(array[3]));
                    break;
                
                case "CompleteCampaign":
                    CompleteCampaign();
                    break;
                
                case "startTimeline":
                    StartTimelineFromNarrativeEvent();
                    break;
                
                case "gameOver":
                    FinishGameFromNarrativeEvent();
                    break;
                
                case "addClue":
                    AddClueFromNarrativeEvent();
                    break;
                
                case "addEntryToCompendium":
                    AddEntryToCompendiumFromNarrativeEvent();
                    break;
                
                default:
                    throw new Exception("Command not found or wrong syntax");
            }
        }
    }
    private void LoadLevelFromNarrativeEvent(string sceneName)
    {
        var locationChange = new CallLocationChange();
        
        locationChange.ChangeLocation(sceneName, true);
    }
    private void AddItemFromNarrativeEvent(int itemId, int quantity)
    {
        foreach (var item in _currentEventItems)
        {
            if (item.itemId != itemId) continue;
            
            var playerEvent = new PlayerEvents();

            for (int i = 0; i < quantity; i++)
            {
                playerEvent.AddItem(item);
            }
        }
    }
    private void RemoveItemFromNarrativeEvent(int itemId, int quantity)
    {
        foreach (var item in _currentEventItems)
        {
            if (item.itemId != itemId) continue;
            
            var playerEvent = new PlayerEvents();
        
            for (int i = 0; i < quantity; i++)
            {
                playerEvent.RemoveItem(item);
            }
        }
    }

    private void StartQuest(string questlineName, int questNumber)
    {
        var questEvent = new CallQuestEvents();
        
        questEvent.StartQuest(ReturnQuestId(questlineName, questNumber));
    }

    private void CompleteQuest(string questlineName, int questNumber)
    {
        var questEvent = new CallQuestEvents();
        
        questEvent.CompleteQuest(ReturnQuestId(questlineName, questNumber));
    }
    private void FailQuest(string questlineName, int questNumber)
    {
        var questEvent = new CallQuestEvents();
        
        questEvent.FailQuest(ReturnQuestId(questlineName, questNumber));
    }

    private QuestId ReturnQuestId(string questlineName, int questNumber)
    {
        var questIdToReturn = new QuestId();

        questIdToReturn.idPrefix = questlineName;
        questIdToReturn.questNumber = questNumber;

        return questIdToReturn;
    }

    private void CompleteCampaign()
    {
        
    }
    // uruchom timeline
    private void StartTimelineFromNarrativeEvent()
    {
        
    }
    // gameover
    private void FinishGameFromNarrativeEvent()
    {
        PlayerEvents.EndGame();
    }
    // dodaj wskazówkę
    private void AddClueFromNarrativeEvent()
    {
        
    }
    // dodaj wpis do kompedium
    private void AddEntryToCompendiumFromNarrativeEvent()
    {
        
    }

    

    #endregion
    #endregion
}
