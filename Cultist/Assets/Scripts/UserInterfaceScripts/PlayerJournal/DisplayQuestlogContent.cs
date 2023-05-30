using System;
using System.Collections;
using System.Collections.Generic;
using Questlines;
using UnityEngine;

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
        
        LoadQuestlines(active, activeQuestlines);
        LoadQuestlines(completed, completedQuestlines);
    }

    private void LoadQuestlines(List<Questline> questlinesToLoad, Transform containerForQuestlines)
    {

        foreach (var questline in questlinesToLoad)
        {
            var quest = Instantiate(questlineDisplayPrefab, containerForQuestlines);
            quest.GetComponent<DisplayedQuestline>().questlineToDisplay = questline;
        }
    }

    private void ClearQuestLogContent()
    {
        foreach (var prompt in activeQuestlines.GetComponentsInChildren<DisplayedQuestline>())
        {
            Destroy(prompt.gameObject);
        }
        foreach (var prompt in completedQuestlines.GetComponentsInChildren<DisplayedQuestline>())
        {
            Destroy(prompt.gameObject);
        }
    }
}
