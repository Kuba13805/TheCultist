using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewCampaign", menuName = "ScriptableObjects/Create New Campaign")]
public class Campaign : ScriptableObject
{
    [SerializeField] private int campaignId;

    [TextArea(2, 4)]
    public string campaignName;

    public Image campaignImage;

    [TextArea(15, 20)]
    public string campaignDesc;

    [Scene]
    public string firstSceneToLoad;
    
    [SerializeField][Label("Questlines Required To Complete")] private List<Questline> requiredQuestlines;

    public NarrativeEvent startingEvent { get; }

    [SerializeField][Label("Campaigns Required To Start")] private List<Campaign> requiredCampaigns;

    public bool hasStarted;

    public bool isCompleted;

    private void OnEnable()
    {
        NewGameManager.OnNewGameStart += StartCampaign;
    }

    private void StartCampaign(Campaign startCampaign)
    {
        if(startCampaign.campaignId != campaignId) return;

        if (requiredCampaigns.Any(campaign => !campaign.isCompleted))
        {
            return;
        }
        
        hasStarted = true;
    }

    private void CompleteCampaign()
    {
        if (requiredQuestlines.Any(questline => !questline.questlineCompleted) || !hasStarted)
        {
            return;
        }
        
        isCompleted = true;
    }
}
