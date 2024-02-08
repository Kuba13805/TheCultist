using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DisplayQuestlineDetails : MonoBehaviour
{
    private Questline _questline;

    [SerializeField] private TextMeshProUGUI questlineNameLabel;

    [SerializeField] private Transform containerForQuests;

    [SerializeField] private GameObject questDetailsPrefab;

    private void Start()
    {
        DisplayedQuestline.OnQuestlineButtonClicked += ShowQuestLineDetails;
    }

    private void OnDestroy()
    {
        DisplayedQuestline.OnQuestlineButtonClicked -= ShowQuestLineDetails;
    }

    private void ShowQuestLineDetails(Questline questline)
    {
        _questline = questline;

        questlineNameLabel.text = _questline.questlineName;

        DisplayQuests();
    }

    private void DisplayQuests()
    {
        ClearQuests();
        
        foreach (var quest in _questline.questlineSteps)
        {
            if (!quest.questStarted) continue;
            
            var step = Instantiate(questDetailsPrefab, containerForQuests);

            step.GetComponentsInChildren<TextMeshProUGUI>()[0].text = quest.questName;
            step.GetComponentsInChildren<TextMeshProUGUI>()[1].text = quest.questDesc;

            if (!quest.questCompleted) continue;
            
            step.GetComponentsInChildren<TextMeshProUGUI>()[0].color = Color.gray;
            step.GetComponentsInChildren<TextMeshProUGUI>()[1].color = Color.gray;
        }
    }

    private void ClearQuests()
    {
        for (var i = 0; i < containerForQuests.childCount; i++)
        {
            Destroy(containerForQuests.GetChild(i).gameObject);
        }
    }
}
