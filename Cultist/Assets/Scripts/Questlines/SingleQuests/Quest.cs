using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using ModestTree;
using NaughtyAttributes;
using Questlines.SingleQuests;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObjects/Quests/Create new Quest")]
public class Quest : ScriptableObject
{
    
    public string questName;
    public QuestId questId;
    
    [TextArea(5, 20)]
    public string originalQuestDesc;
    
    [TextArea(3, 8)]
    public string originalShortQuestDesc;


    public List<Reward> questRewards;

    [AllowNesting][Foldout("QuestStatus")]
    public bool questVisible = true;
    [Foldout("QuestStatus")][AllowNesting]
    public bool questStarted;
    [Foldout("QuestStatus")][AllowNesting]
    public bool questCompleted;
    [Foldout("QuestStatus")][AllowNesting]
    public bool questFailed;

    [TextArea(5, 20)][Foldout("QuestStatusDesc")][AllowNesting]
    public string questFailedDesc;
    
    [TextArea(5, 20)][Foldout("QuestStatusDesc")][AllowNesting]
    public string questCompletedDesc;

    public List<Quest> questSteps;

    public List<QuestVariables> questVariables;

    public List<Quest> prerequisiteQuests;
    
    [TextArea(5, 20)]
    public string questDesc;
    
    [TextArea(3, 8)]
    public string shortQuestDesc;

    #region Events

    public static event Action<Quest> OnQuestCompleted;

    public static event Action<Quest> OnQuestStarted;

    public static event Action<Quest> OnQuestFailed; 

    #endregion

    public virtual void OnEnable()
    {
        CallQuestEvents.OnQuestStart += StartQuestFromEvent;

        CallQuestEvents.OnQuestComplete += MarkQuestAsCompletedFromEvent;

        CallQuestEvents.OnQuestFail += MarkQuestAsFailedFromEvent;

        OnQuestCompleted += OnPrerequisiteQuestComplete;
        
        if (questVariables == null) return;

            foreach (var questVariable in questVariables)
        {
            var trim = questVariable.variableCodeName.Trim();

            questVariable.variableCodeName = trim;
        }
    }

    private void OnPrerequisiteQuestComplete(Quest completedQuest)
    {
        foreach (var prerequisiteQuest in prerequisiteQuests)
        {
            if (completedQuest != prerequisiteQuest) continue;
            
            StartQuest(questId);
        }
    }

    private void MarkQuestAsCompletedFromEvent(QuestId questIdFromEvent)
    {
        CompleteQuest(questIdFromEvent);
    }

    private void MarkQuestAsFailedFromEvent(QuestId questIdFromEvent)
    {
        FailQuest(questIdFromEvent);
    }

    protected virtual void FailQuest(QuestId questIdFromEvent)
    {
        if (questIdFromEvent.ToString() != questId.ToString())
        {
            return;
        }

        if (questFailed || !questStarted || questCompleted) return;

        if (prerequisiteQuests != null)
        {
            foreach (var quest in prerequisiteQuests.Where(quest => quest.questStarted))
            {
                quest.FailQuest(quest.questId);
            }
        }

        questDesc = questFailedDesc;
        
        questFailed = true;

        questCompleted = true;
        
        StopListeningToQuestEvents();
        
        OnQuestFailed?.Invoke(this);
    }

    protected virtual void CompleteQuest(QuestId questIdFromEvent)
    {
        if (questIdFromEvent.ToString() != questId.ToString())
        {
            return;
        }

        if (questCompleted || questFailed) return;

        if (prerequisiteQuests != null)
        {
            if (questSteps.Any(requiredQuest => !requiredQuest.questCompleted))
            {
                return;
            }
        }

        questDesc = questCompletedDesc;
        
        questCompleted = true;

        StopListeningToQuestEvents();
        
        RewardPlayer();

        OnQuestCompleted?.Invoke(this);
    }

    #region QuestRewardSystem

    private void RewardPlayer()
    {
        foreach (var reward in questRewards)
        {
            switch (reward.rewardType)
            {
                case RewardType.GiveItem:
                    GiveItemReward(reward.rewardItem);
                    break;
                case RewardType.GiveClue:
                    break;
                case RewardType.GiveMoney:
                    GiveMoneyReward(reward.rewardMoney);
                    break;
                case RewardType.GiveStatExp:
                    break;
                case RewardType.GiveStatLevel:
                    GiveStatLevelReward(reward.statToModify, reward.rewardStatLevel);
                    break;
                case RewardType.GiveAbility:
                    GiveAbilityReward(reward.rewardAbility);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private static void GiveItemReward(BaseItem rewardItem)
    {
        var playerEvent = new PlayerEvents();
        
        playerEvent.AddItem(rewardItem);
    }

    private void GiveClueReward()
    {
        
    }
    private static void GiveMoneyReward(float moneyAmount)
    {
        var playerEvent = new PlayerEvents();
        
        playerEvent.AddMoney(moneyAmount);
    }

    private void GiveStatExpReward()
    {
        
    }

    private static void GiveStatLevelReward(Stat statName, int statLevel)
    {
        var playerEvent = new PlayerEvents();
        
        playerEvent.AddStatLevel(statName, statLevel);
    }

    private static void GiveAbilityReward(Ability ability)
    {
        var playerEvent = new PlayerEvents();
        
        playerEvent.AddAbility(ability);
    }

    #endregion
    private void StartQuestFromEvent(QuestId questIdFromEvent)
    {
        StartQuest(questIdFromEvent);
    }

    protected virtual void StartQuest(QuestId questIdFromEvent)
    {
        if (questIdFromEvent.idPrefix != questId.idPrefix || questIdFromEvent.questNumber != questId.questNumber)
        {
            return;
        }
        
        if (questStarted)
        {
            return;
        }
        
        if (prerequisiteQuests != null && prerequisiteQuests.Any(requiredQuest => !requiredQuest.questCompleted))
        {
            return;
        }

        questDesc = originalQuestDesc;

        shortQuestDesc = originalShortQuestDesc;

        questStarted = true;

        if (questVariables.Count > 0)
        {
            questVariables[0].conditionPassed = true;
        }

        OnQuestStarted?.Invoke(this);
    }

    protected virtual void StopListeningToQuestEvents()
    {
        CallQuestEvents.OnQuestStart -= StartQuestFromEvent;

        CallQuestEvents.OnQuestComplete -= MarkQuestAsCompletedFromEvent;

        CallQuestEvents.OnQuestFail -= MarkQuestAsFailedFromEvent;

        OnQuestCompleted -= OnPrerequisiteQuestComplete;
    }

    public virtual void RestartQuest()
    {
        questDesc = originalQuestDesc;

        shortQuestDesc = originalShortQuestDesc;
        
        questStarted = false;
        
        questCompleted = false;
        
        questFailed = false;

        foreach (var variable in questVariables)
        {
            variable.conditionPassed = false;
        }
        
        if (questSteps.IsEmpty()) return;

        foreach (var step in questSteps)
        {
            RestartQuest();
        }
    }

    public virtual void ForceCompleteQuest()
    {
        questDesc = originalQuestDesc;

        shortQuestDesc = originalShortQuestDesc;
        
        questStarted = true;
        
        questCompleted = true;
        
        foreach (var variable in questVariables)
        {
            variable.conditionPassed = true;
        }
        
        foreach (var step in questSteps)
        {
            ForceCompleteQuest();
        }
    }
}
