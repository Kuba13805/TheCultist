using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayQuestStatus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questNameTextBox;

    [SerializeField] private GameObject questPanel;

    [SerializeField] private float timeToAppear;
    

    private void Start()
    {
        questNameTextBox.DOFade(0, 0);

        questPanel.GetComponent<Image>().DOFade(0, 0);
        
        Quest.OnQuestStarted += DisplayStartingQuestName;

        Quest.OnQuestCompleted += DisplayCompletedQuestName;
    }

    private void OnDisable()
    {
        Quest.OnQuestStarted -= DisplayStartingQuestName;

        Quest.OnQuestCompleted -= DisplayCompletedQuestName;
    }

    private void DisplayStartingQuestName(Quest quest)
    {
        if(!quest.questVisible) return;
        
        questNameTextBox.text = "Started: " + quest.questName;
        
        questNameTextBox.DOFade(255, 3);

        questPanel.GetComponent<Image>().DOFade(255, 3);

        StartCoroutine(WaitToDeactivate());
    }
    private void DisplayCompletedQuestName(Quest quest)
    {
        if(!quest.questVisible) return;
        
        questPanel.SetActive(true);
        
        questNameTextBox.text = "Completed: " + quest.questName;
        
        questNameTextBox.DOFade(255, 3);

        questPanel.GetComponent<Image>().DOFade(255, 3);
        
        StartCoroutine(WaitToDeactivate());
    }

    private IEnumerator WaitToDeactivate()
    {
        yield return new WaitForSeconds(timeToAppear);
        questPanel.SetActive(false);
    }
}
