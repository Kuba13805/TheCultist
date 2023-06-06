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
    [SerializeField] protected QuestId questId;

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
        
        if (questVariables == null) return;

            foreach (var questVariable in questVariables)
        {
            var trim = questVariable.variableName.Trim();

            questVariable.variableName = trim;
        }
    }

    private void MarkQuestAsCompletedFromDialogue(QuestId questIdFromEvent)
    {
        CompleteQuest(questIdFromEvent);
    }

    protected void CompleteQuest(QuestId questIdFromEvent)
    {
        if (questIdFromEvent.ToString() != questId.ToString())
        {
            Debug.Log(questIdFromEvent + ":" + questId);
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
        if (questIdFromEvent != questId)
        {
            return;
        }

        if (questStarted)
        {
            return;
        }

        if (prerequisiteQuests.Any(requiredQuest => !requiredQuest.questCompleted))
        {
            return;
        }

        questStarted = true;

        if (questVariables != null)
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
