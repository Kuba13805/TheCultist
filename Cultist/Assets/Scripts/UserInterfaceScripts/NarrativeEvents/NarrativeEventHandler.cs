using System;
using System.Linq;
using Events;
using Ink.Runtime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeEventHandler : MonoBehaviour
{
    [SerializeField] private GameObject narrativeEventPanel;
    
    [SerializeField] private GameObject eventImageBox;
    
    [SerializeField] private GameObject eventTextBox;
    
    [SerializeField] private GameObject eventChoicesBox;

    [SerializeField] private GameObject choicePrompt;

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
            CreateChoicePrompt(choice);
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

    #region EventFunctionsCode

    // zczytaj komendę z tekstu
    private void SearchForCommand()
    {
        if (_currentStory.currentTags == null) return;
        
        var commandList = _currentStory.currentTags;

        foreach (var array in commandList.Select(command => command.Split(':')))
        {
            if (!array[0].Contains("command") || array[0] == null) return;
            
            if (array[1].Contains("loadLevel"))
            {
                LoadLevelFromNarrativeEvent(array[2]);
            }
            else if (array[1].Contains("addItem"))
            {
                AddItemFromNarrativeEvent(int.Parse(array[2]));
            }
            else if (array[1].Contains("removeItem"))
            {
                RemoveItemFromNarrativeEvent(int.Parse(array[2]));
            }
            else if (array[1].Contains("startTimeline"))
            {
                StartTimelineFromNarrativeEvent();
            }
            else if (array[1].Contains("gameOver"))
            {
                FinishGameFromNarrativeEvent();
            }
            else if (array[1].Contains("addClue"))
            {
                AddClueFromNarrativeEvent();
            }
            else if (array[1].Contains("addEntryToCompendium"))
            {
                AddEntryToCompendiumFromNarrativeEvent();
            }
        }
    }
    
    // ładowanie levelu
    private void LoadLevelFromNarrativeEvent(string sceneName)
    {
        var locationChange = new CallLocationChange();
        
        locationChange.ChangeLocation(sceneName, true);
    }
    // dodawanie przedmiotu
    private void AddItemFromNarrativeEvent(int itemId)
    {
        var playerEvent = new PlayerEvents();
        
        //playerEvent.AddItem();
    }
    // usuwanie przedmiotu
    private void RemoveItemFromNarrativeEvent(int itemId)
    {
        
    }
    // uruchom timeline
    private void StartTimelineFromNarrativeEvent()
    {
        
    }
    // gameover
    private void FinishGameFromNarrativeEvent()
    {
        
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
}
