using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CampaignSummaryManager : MonoBehaviour
{
    [SerializeField] private Campaign completedCampaign;

    [SerializeField] private List<Reward> earnedRewards;

    [SerializeField] [Foldout("UiElements")]
    private TextMeshProUGUI campaignNameText;

    [SerializeField] [Foldout("UiElements")]
    private Image campaignImage;
    
    [SerializeField] [Foldout("UiElements")]
    private GameObject completedQuestlinesBox;
    
    [SerializeField] [Foldout("UiElements")]
    private GameObject majorDecisionBox;
    
    [SerializeField] [Foldout("UiElements")]
    private TextMeshProUGUI majorDecisionDescText;
    
    [SerializeField] [Foldout("UiElements")]
    private GameObject campaignRewardsBox;
    
    
    [SerializeField] [Foldout("UiPrefabs")]
    private GameObject completedQuestlinePrefab;
    
    [SerializeField] [Foldout("UiPrefabs")]
    private GameObject majorDecisionPrefab;
    
    [SerializeField] [Foldout("UiPrefabs")]
    private GameObject rewardPrefab;

    private void OnEnable()
    {
        CurrentLocationManager.OnPassCampaignDataToSummary += UpdateCompletedCampaign;
    }

    private void OnDisable()
    {
        CurrentLocationManager.OnPassCampaignDataToSummary -= UpdateCompletedCampaign;
    }

    private void UpdateCompletedCampaign(Campaign passedCampaign)
    {
        completedCampaign = passedCampaign;
        
        GetEarnedRewards();
        
        DisplayCompletedCampaign();
    }

    private void GetEarnedRewards()
    {
        earnedRewards = completedCampaign.ReturnCampaignRewards();
    }

    private void DisplayCompletedCampaign()
    {
        campaignNameText.text = completedCampaign.campaignName;
        
        DisplayCompletedQuestlines();
    }

    private void DisplayCompletedQuestlines()
    {
        foreach (var questline in completedCampaign.campaignQuestlines)
        {
            if (!questline.questlineCompleted) continue;
            
            var promptInstance = Instantiate(completedQuestlinePrefab, completedQuestlinesBox.transform);

            promptInstance.GetComponentInChildren<TextMeshProUGUI>().text = questline.questlineName;
        }
    }

    private void DisplayMajorDecisions(Questline selectedQuestline)
    {
        foreach (var quest in selectedQuestline.questlineSteps)
        {
            foreach (var questVariable in quest.questVariables)
            {
                if (!questVariable.isMajorDecision) continue;

                var promptInstance = Instantiate(majorDecisionPrefab, majorDecisionBox.transform);

                promptInstance.GetComponentInChildren<TextMeshProUGUI>().text = questVariable.variableName;
            }
        }
    }
}
