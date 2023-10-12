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
    public string questDesc;
    
    [TextArea(3, 8)]
    public string shortQuestDesc;

    public bool questStarted;
    public bool questCompleted;
    public bool questFailed;

    [TextArea(5, 20)]
    public string questFailedDesc;

    public List<Quest> questSteps;

    public List<QuestVariables> questVariables;

    public List<Quest> prerequisiteQuests;

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
            var trim = questVariable.variableName.Trim();

            questVariable.variableName = trim;
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

        questCompleted = true;

        StopListeningToQuestEvents();

        OnQuestCompleted?.Invoke(this);
    }

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

    public void RestartQuest()
    {
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
}
