using System;
using System.Collections;
using System.Collections.Generic;
using Questlines.SingleQuests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayActiveQuestsOnUI : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;

    [SerializeField] private Questline currentQuestline;

    private void Awake()
    {
        DisplayedQuestline.OnQuestlineButtonClicked += UpdateDisplayedQuests;

        Quest.OnQuestStarted += AddNewQuestToDisplay;

        QuestFindItem.OnQuestUpdateStatus += UpdateQuest;
        
        Questline.OnQuestlineStart += DisplayQuestsFromNewQuestline;
    }

    private void DisplayQuestsFromNewQuestline(Questline questline)
    {
        currentQuestline = questline;
        
        DisplayQuests(currentQuestline);
    }

    private void UpdateQuest(Quest quest)
    {
        DisplayQuests(currentQuestline);
    }

    private void UpdateDisplayedQuests(Questline activeQuestline)
    {
        currentQuestline = activeQuestline;
        
        DisplayQuests(activeQuestline);
    }

    private void DisplayQuests(Questline activeQuestline)
    {
        ClearQuests();

        if (currentQuestline == null) return;
        
        foreach (var quest in activeQuestline.questlineSteps)
        {
            if (!quest.questStarted || quest.questCompleted) continue;

            InstantiateSingleQuest(quest);
        }

    }

    private void AddNewQuestToDisplay(Quest quest)
    {
        if (currentQuestline != null)
        {
            InstantiateSingleQuest(quest);
        }
    }
    private void InstantiateSingleQuest(Quest quest)
    {
        var questInstance = Instantiate(questPrefab, transform);

        questInstance.GetComponentsInChildren<TextMeshProUGUI>()[0].text = quest.questName;
        questInstance.GetComponentsInChildren<TextMeshProUGUI>()[1].text = quest.shortQuestDesc;

        questInstance.GetComponentInChildren<SingleQuestPanelOnUIData>().currentQuest = quest;
    }

    private void ClearQuests()
    {
        if (transform.childCount <= 0) return;

        for (var i = 0; i < transform.childCount; i++)
        {
            Destroy(GetComponentsInChildren<SingleQuestPanelOnUIData>()[i].gameObject);
        }
    }

    private void OnDestroy()
    {
        DisplayedQuestline.OnQuestlineButtonClicked -= UpdateDisplayedQuests;

        Quest.OnQuestStarted -= AddNewQuestToDisplay;
        
        QuestFindItem.OnQuestUpdateStatus -= UpdateQuest;
        
        Questline.OnQuestlineStart -= DisplayQuestsFromNewQuestline;
    }
}
