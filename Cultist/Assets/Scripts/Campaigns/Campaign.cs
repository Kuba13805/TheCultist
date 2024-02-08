using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Unity.Services.Analytics;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCampaign", menuName = "ScriptableObjects/Create New Campaign")]
public class Campaign : ScriptableObject
{
    public int campaignId;

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

    public static event Action<Campaign> OnForcedCampaignComplete; 
    
    private void OnEnable()
    {
        NewGameManager.OnNewGameStart += HandleNewGameStart;
        
        CurrentLocationManager.OnSceneLoaded += CallForFirstNarrativeEvent;

        Questline.OnQuestlineCompleted += CompleteCampaign;
    }

    private void HandleNewGameStart(Campaign startCampaign)
    {
        ResetCampaign();
        
        StartCampaign(startCampaign);
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

        SendStartedCampaignData(campaignId.ToString(), campaignName);
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
        Debug.Log(campaignName + " completed!");
        
        SendCompletedCampaignData(campaignId.ToString(), campaignName);
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
        CurrentLocationManager.OnSceneLoaded += CallForFirstNarrativeEvent;

        Questline.OnQuestlineCompleted += CompleteCampaign;
        
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

        OnForcedCampaignComplete?.Invoke(this);

        foreach (var questline in campaignQuestlines)
        {
            questline.ForceCompleteQuestline();
        }
        
        SendCompletedCampaignData(campaignId.ToString(), campaignName);
    }
    
    private static void SendStartedCampaignData(string idToPass, string nameToPass)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "campaignStoryId", idToPass },
                { "questName", nameToPass }
            };
    
            AnalyticsService.Instance.CustomData("campaignStarted", parameters);
        }
    
        private static void SendCompletedCampaignData(string idToPass, string nameToPass)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "campaignStoryId", idToPass },
                { "questName", nameToPass }
            };
    
            AnalyticsService.Instance.CustomData("campaignCompleted", parameters);
        }
}
