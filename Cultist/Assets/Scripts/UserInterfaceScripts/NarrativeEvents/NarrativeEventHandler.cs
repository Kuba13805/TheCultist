using System;
using Events;
using Ink.Runtime;
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

        if (eventTextBox.GetComponent<TextMeshProUGUI>().text == "")
        {
            CloseEvent();
                
            return;
        }
            
        DisplayChoices();
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
}
