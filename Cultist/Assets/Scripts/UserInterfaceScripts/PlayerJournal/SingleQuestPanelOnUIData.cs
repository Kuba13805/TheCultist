using System;
using Questlines.SingleQuests;
using TMPro;
using UnityEngine;

public class SingleQuestPanelOnUIData : MonoBehaviour
{
    public Quest currentQuest;

    private void Start()
    {
        Quest.OnQuestStarted += AddQuest;
        
        Quest.OnQuestCompleted += ObserveCompletedQuests;
    }

    private void OnDestroy()
    {
        Quest.OnQuestStarted -= AddQuest;
        
        Quest.OnQuestCompleted -= ObserveCompletedQuests;
    }
    private void AddQuest(Quest quest)
    {
        currentQuest = quest;
    }

    private void ObserveCompletedQuests(Quest quest)
    {
        if (currentQuest == quest)
        {
            Destroy(gameObject);
        }
    }
}
