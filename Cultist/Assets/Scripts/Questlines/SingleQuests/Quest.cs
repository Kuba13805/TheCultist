using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Questlines.SingleQuests;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObjects/Quests/Create new Quest")]
public class Quest : ScriptableObject
{
    
    public string questName;
    public QuestId questId;

    [TextArea(15, 20)]
    public string questDesc;
    
    [TextArea(3, 8)]
    public string shortQuestDesc;

    public bool questStarted;
    public bool questCompleted;


    public List<Quest> questSteps;

    public List<QuestVariables> questVariables;

    public List<Quest> prerequisiteQuests;

    #region Events

    public static event Action<Quest> OnQuestCompleted;

    public static event Action<Quest> OnQuestStarted;

    #endregion

    public virtual void OnEnable()
    {
        DialogueController.OnQuestStart += StartQuestFromDialogue;

        DialogueController.OnQuestComplete += MarkQuestAsCompletedFromDialogue;

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

    private void MarkQuestAsCompletedFromDialogue(QuestId questIdFromEvent)
    {
        CompleteQuest(questIdFromEvent);
    }

    protected virtual void CompleteQuest(QuestId questIdFromEvent)
    {
        if (questIdFromEvent.ToString() != questId.ToString())
        {
            return;
        }

        if (questCompleted)
        {
            return;
        }

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

    private void StartQuestFromDialogue(QuestId questIdFromEvent)
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
        DialogueController.OnQuestStart -= StartQuestFromDialogue;

        DialogueController.OnQuestComplete -= MarkQuestAsCompletedFromDialogue;
    }
}
