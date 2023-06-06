using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayActiveQuestsOnUI : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;
    private void Start()
    {
        DisplayedQuestline.OnQuestlineButtonClicked += UpdateDisplayedQuests;

        Quest.OnQuestStarted += AddNewQuestToDisplay;
    }

    private void UpdateDisplayedQuests(Questline activeQuestline)
    {
        DisplayQuests(activeQuestline);
    }

    private void DisplayQuests(Questline activeQuestline)
    {
        ClearQuests();
        
        foreach (var quest in activeQuestline.questlineSteps)
        {
            if (!quest.questStarted) continue;

            InstantiateSingleQuest(quest);
        }
    }

    private void AddNewQuestToDisplay(Quest quest)
    {
        InstantiateSingleQuest(quest);
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
}
