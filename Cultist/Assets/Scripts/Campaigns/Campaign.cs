using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCampaign", menuName = "ScriptableObjects/Create New Campaign")]
public class Campaign : ScriptableObject
{
    [SerializeField] private int campaignId;

    [TextArea(2, 4)]
    public string campaignName;

    public Sprite campaignImage;

    [TextArea(15, 20)]
    public string campaignDesc;

    [Scene]
    public string firstSceneToLoad;
    
    public NarrativeEvent startingEvent;

    [Label("Questlines In Campaign")]
    public List<Questline> campaignQuestlines;
    
    [SerializeField][Label("Questlines Required To Complete")] private List<Questline> requiredQuestlines;

    [SerializeField][Label("Campaigns Required To Start")] private List<Campaign> requiredCampaigns;

    public bool hasStarted;

    public bool isCompleted;

    public List<Reward> campaignRewards;

    public static event Action<Campaign> OnCampaignStart;

    public static event Action<Campaign> OnCampaignComplete;
    
    private void OnEnable()
    {
        NewGameManager.OnNewGameStart += StartCampaign;

        CurrentLocationManager.OnSceneLoaded += CallForFirstNarrativeEvent;

        Questline.OnQuestlineCompleted += CompleteCampaign;
    }

    private void StartCampaign(Campaign startCampaign)
    {
        if(startCampaign.campaignId != campaignId) return;

        if (requiredCampaigns.Any(campaign => !campaign.isCompleted))
        {
            return;
        }
        
        hasStarted = true;
        
        OnCampaignStart?.Invoke(this);
        
        NewGameManager.OnNewGameStart -= StartCampaign;
    }

    private void CallForFirstNarrativeEvent()
    {
        startingEvent.CallForEvent();
        
        CurrentLocationManager.OnSceneLoaded -= CallForFirstNarrativeEvent;
    }

    private void CompleteCampaign(Questline recentlyCompletedQuestline)
    {
        if (campaignQuestlines.Any(questline => questline.questlineName != recentlyCompletedQuestline.questlineName))
        {
            return;
        }
        
        if (requiredQuestlines.Any(questline => !questline.questlineCompleted) || !hasStarted)
        {
            return;
        }
        
        isCompleted = true;
        
        OnCampaignComplete?.Invoke(this);
    }

    public List<Reward> ReturnCampaignRewards()
    {
        var rewardsToReturn = new List<Reward>();

        foreach (var quest in campaignQuestlines.SelectMany(questline => questline.questlineSteps.Where(quest => quest.questRewards != null)))
        {
            rewardsToReturn.AddRange(quest.questRewards.Where(reward => quest.questCompleted));
        }

        return rewardsToReturn;
    }

    public void ResetCampaign()
    {
        hasStarted = false;
        isCompleted = false;

        foreach (var questline in campaignQuestlines)
        {
            questline.RestartQuestline();
        }
    }

    public void ForceCompleteCampaign()
    {
        hasStarted = true;
        isCompleted = true;

        foreach (var questline in requiredQuestlines)
        {
            questline.ForceCompleteQuestline();
        }
        
        OnCampaignComplete?.Invoke(this);
    }
}
