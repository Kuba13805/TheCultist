using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Questlines;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayQuestlogContent : MonoBehaviour
{
    [SerializeField] private GameObject questlineDisplayPrefab;
    
    //[SerializeField] private GameObject 

    public static event Action OnQuestLogRequest;

    private void Awake()
    {
        Questline.OnQuestlineStart += SetNewDisplayedQuestlineOnUI;
        
        QuestManager.OnQuestLogPass += LoadQuestLogContent;
    }

    // private void Start()
    // {
    //     Questline.OnQuestlineStart += SetNewDisplayedQuestlineOnUI;
    //     
    //     QuestManager.OnQuestLogPass += LoadQuestLogContent;
    //     
    // }

    private void OnEnable()
    {
        if (transform.childCount > 0) ClearLog();
        
        OnQuestLogRequest?.Invoke();
    }

    private void SetNewDisplayedQuestlineOnUI(Questline questline)
    {
        if (transform.childCount == 1)
        {
            transform.GetComponentInChildren<DisplayedQuestline>().InvokeQuestline();
        }
    }

    private void LoadQuestLogContent(List<Questline> active, List<Questline> completed)
    {
        LoadQuestlines(active);
        LoadQuestBreak();
        LoadQuestlines(completed);
    }

    private void LoadQuestlines(List<Questline> questlinesToLoad)
    {
        
        foreach (var questline in questlinesToLoad.Where(questline => questline.questlineVisible && questline.questlineStarted && questline != null))
        {
            if (questlineDisplayPrefab == null) return;
            
            var quest = Instantiate(questlineDisplayPrefab, transform);
            
            if (quest == null) continue;
            
            quest.GetComponent<DisplayedQuestline>().questlineToDisplay = questline;
        }
    }

    private void LoadQuestBreak()
    {
        var quest = Instantiate(questlineDisplayPrefab, transform);
        quest.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "";
        quest.GetComponentInChildren<Button>().interactable = false;
    }

    private void ClearLog()
    {
        foreach (var prompt in transform.GetComponentsInChildren<DisplayedQuestline>())
        {
            Destroy(prompt.gameObject);
        }
    }

    private void OnDisable()
    {
        if (transform.childCount > 0) ClearLog();
    }
}
