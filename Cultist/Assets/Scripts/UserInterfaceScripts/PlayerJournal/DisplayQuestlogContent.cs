using System;
using System.Collections;
using System.Collections.Generic;
using Questlines;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayQuestlogContent : MonoBehaviour
{
    [SerializeField] private Transform activeQuestlines;
    
    [SerializeField] private Transform completedQuestlines;

    [SerializeField] private GameObject questlineDisplayPrefab;

    public static event Action OnQuestLogRequest;

    private void OnEnable()
    {
        QuestManager.OnQuestLogPass += LoadQuestLogContent;
        
        OnQuestLogRequest?.Invoke();
    }

    private void LoadQuestLogContent(List<Questline> active, List<Questline> completed)
    {
        ClearQuestLogContent();
        
        LoadQuestlines(active);
        LoadQuestBreak();
        LoadQuestlines(completed);
    }

    private void LoadQuestlines(List<Questline> questlinesToLoad)
    {

        foreach (var questline in questlinesToLoad)
        {
            var quest = Instantiate(questlineDisplayPrefab, transform);
            quest.GetComponent<DisplayedQuestline>().questlineToDisplay = questline;
        }
    }

    private void LoadQuestBreak()
    {
        var quest = Instantiate(questlineDisplayPrefab, transform);
        quest.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "";
        quest.GetComponentInChildren<Button>().interactable = false;
    }

    private void ClearQuestLogContent()
    {
        foreach (var prompt in transform.GetComponentsInChildren<DisplayedQuestline>())
        {
            Destroy(prompt.gameObject);
        }
    }
}
