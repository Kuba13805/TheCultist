using System;
using System.Collections;
using System.Collections.Generic;
using Questlines;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayQuestlogContent : MonoBehaviour
{
    [SerializeField] private GameObject questlineDisplayPrefab;

    public static event Action OnQuestLogRequest;

    private void Awake()
    {
        Questline.OnQuestlineStart += SetNewDisplayedQuestlineOnUI;
        
        QuestManager.OnQuestLogPass += LoadQuestLogContent;
    }


    private void OnEnable()
    {
        OnQuestLogRequest?.Invoke();
    }
    private void SetNewDisplayedQuestlineOnUI(Questline questline)
    {
        OnQuestLogRequest?.Invoke();
        
        if (transform.childCount == 1)
        {
            transform.GetComponentInChildren<DisplayedQuestline>().InvokeQuestline();
        }
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
            if (!questline.questlineVisible || !questline.questlineStarted) continue;
            
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
