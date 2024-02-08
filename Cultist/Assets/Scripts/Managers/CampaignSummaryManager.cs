using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using ModestTree;
using NaughtyAttributes;
using PlayerScripts;
using Questlines.SingleQuests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CampaignSummaryManager : MonoBehaviour
{
    [SerializeField] private Campaign completedCampaign;

    [SerializeField] private List<Reward> earnedRewards;

    private List<Questline> _questlinesToDisplay;

    #region Elements

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
    private GameObject campaignAttributeRewardsBox;
    
    [SerializeField] [Foldout("UiElements")]
    private GameObject campaignItemRewardsBox;
    
    #endregion

    #region Prefabs

    [SerializeField] [Foldout("UiPrefabs")]
    private GameObject completedQuestlinePrefab;
    
    [SerializeField] [Foldout("UiPrefabs")]
    private GameObject majorDecisionPrefab;
    
    [SerializeField] [Foldout("RewardsPrefabs")]
    private GameObject attributeRewardPrefab;
    
    [SerializeField] [Foldout("RewardsPrefabs")]
    private GameObject itemRewardPrefab;
    
    #endregion

    private void OnEnable()
    {
        CurrentLocationManager.OnPassCampaignDataToSummary += UpdateCompletedCampaign;

        DisplayedQuestline.OnQuestlineButtonClicked += DisplayMajorDecisions;
    }

    private void Start()
    {
        CallPlayerInputChange.SetAllInput(false);
        
        CallPlayerInputChange.SetUiActions(true);

        MajorDecisionLogic.OnQuestVariableClick += UpdateDecisionDesc;
    }

    private void OnDisable()
    {
        CurrentLocationManager.OnPassCampaignDataToSummary -= UpdateCompletedCampaign;
        
        DisplayedQuestline.OnQuestlineButtonClicked -= DisplayMajorDecisions;
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
        
        DisplayCampaignRewards();
    }

    private void DisplayCompletedQuestlines()
    {
        _questlinesToDisplay = new List<Questline>();
        
        if (_questlinesToDisplay.Count > 0)
        {
            foreach (var questline in _questlinesToDisplay)
            {
                _questlinesToDisplay.Remove(questline);
            }
        }

        foreach (var questline in completedCampaign.campaignQuestlines)
        {
            _questlinesToDisplay.Add(questline);
        }
        
        foreach (var questline in _questlinesToDisplay)
        {
            if (!questline.questlineCompleted) continue;
            
            var promptInstance = Instantiate(completedQuestlinePrefab, completedQuestlinesBox.transform);

            promptInstance.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = questline.questlineName;

            promptInstance.GetComponent<DisplayedQuestline>().questlineToDisplay = questline;
        }
    }

    #region Decisions
    private void DisplayMajorDecisions(Questline selectedQuestline)
    {
        ClearDecisionBox();
        
        foreach (var quest in selectedQuestline.questlineSteps)
        {
            foreach (var questVariable in quest.questVariables)
            {
                if (!questVariable.isMajorDecision) continue;

                var promptInstance = Instantiate(majorDecisionPrefab, majorDecisionBox.transform);

                promptInstance.GetComponent<MajorDecisionLogic>().currentQuestVariable = questVariable;
            }
        }
    }

    private void ClearDecisionBox()
    {
        if (majorDecisionBox.transform.childCount <= 0) return;
        
        foreach (var prompt in majorDecisionBox.GetComponentsInChildren<MajorDecisionLogic>())
        {
            Destroy(prompt.gameObject);
        }
    }

    private void UpdateDecisionDesc(QuestVariables questVariable)
    {
        majorDecisionDescText.text = questVariable.conditionPassed ? questVariable.conditionPassedDesc : questVariable.conditionNotPassedDesc;
    }

    

    #endregion

    private void DisplayCampaignRewards()
    {
        foreach (var reward in completedCampaign.campaignRewards)
        {
            DisplaySingleReward(reward);
        }
    }

    private void DisplaySingleReward(Reward reward)
    {
        switch (reward.rewardType)
        {
            case RewardType.GiveItem:
                DisplayItemReward(reward);
                break;
            case RewardType.GiveClue:
                break;
            case RewardType.GiveMoney:
                break;
            case RewardType.GiveStatExp:
                break;
            case RewardType.GiveStatLevel:
                DisplayAttributeReward(reward);
                break;
            case RewardType.GiveAbility:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void DisplayItemReward(Reward reward)
    {
        var rewardPrompt = Instantiate(itemRewardPrefab, campaignItemRewardsBox.transform);

        rewardPrompt.GetComponentInChildren<DisplayItemDetails>().item = reward.rewardItem;
    }

    private void DisplayAttributeReward(Reward reward)
    {
        var rewardPrompt = Instantiate(attributeRewardPrefab, campaignAttributeRewardsBox.transform);
        
        rewardPrompt.GetComponent<AttributeRewardLogic>().InitializeReward(reward.statToModify.ToString(), reward.rewardStatLevel);
    }
}
