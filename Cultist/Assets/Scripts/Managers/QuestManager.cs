using System;
using System.Collections.Generic;
using Questlines;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    #region Events

    public static event Action<List<Questline>, List<Questline>> OnQuestLogPass;

    public static event Action<NarrativeEvent> OnNarrativeEventPass; 
    #endregion
    [SerializeField] private QuestLog currentQuestLog;


    [SerializeField] private Campaign ongoingCampaign;

    [SerializeField] private NarrativeEvent currentNarrativeEvent;

    [SerializeField] private List<Reward> earnedRewards;
    
    private void OnEnable()
    {
        DisplayQuestlogContent.OnQuestLogRequest += PassQuestLog;

        Campaign.OnCampaignStart += UpdateCampaign;

        NarrativeEvent.CallForEventToOpen += UpdateCurrentNarrativeEvent;
    }

    private void OnDisable()
    {
        DisplayQuestlogContent.OnQuestLogRequest -= PassQuestLog;
        
        Campaign.OnCampaignStart -= UpdateCampaign;
        
        NarrativeEvent.CallForEventToOpen += UpdateCurrentNarrativeEvent;
    }

    private void UpdateCurrentNarrativeEvent(NarrativeEvent narrativeEvent)
    {
        currentNarrativeEvent = narrativeEvent;
        
        OnNarrativeEventPass?.Invoke(narrativeEvent);
    }

    private void UpdateCampaign(Campaign newCampaign)
    {
        ongoingCampaign = newCampaign;
    }

    private void PassQuestLog()
    {
        OnQuestLogPass?.Invoke(currentQuestLog.ReturnActiveQuestlines(), currentQuestLog.ReturnCompletedQuestlines());
    }
}
