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
    [SerializeField] private QuestId questId;

    [TextArea(15, 20)]
    public string questDesc;

    public bool questStarted;
    public bool questCompleted;


    public List<Quest> questSteps;

    public List<QuestVariables> questVariables;

    public List<Quest> prerequisiteQuests;

    #region Events

    public static event Action<Quest> OnQuestCompleted;

    public static event Action<Quest> OnQuestStarted;

    #endregion

    private void OnEnable()
    {
        if (questVariables == null) return;

        foreach (var questVariable in questVariables)
        {
            var trim = questVariable.variableName.Trim();

            questVariable.variableName = trim;
        }
        DialogueController.OnQuestStart += StartQuest;

        DialogueController.OnQuestComplete += MarkQuestAsCompleted;
    }

    private void MarkQuestAsCompleted(QuestId questIdFromEvent)
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

    private void StartQuest(QuestId questIdFromEvent)
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

        questVariables[0].conditionPassed = true;
        
        OnQuestStarted?.Invoke(this);
    }

    private void StopListeningToQuestEvents()
    {
        DialogueController.OnQuestStart -= StartQuest;

        DialogueController.OnQuestComplete -= MarkQuestAsCompleted;
    }
}