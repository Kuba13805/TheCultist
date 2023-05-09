using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DisplayQuestStatus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questNameTextBox;

    [SerializeField] private GameObject questPanel;

    [SerializeField] private float timeToAppear;

    private void Start()
    {
        Quest.OnQuestStarted += DisplayStartingQuestName;

        Quest.OnQuestCompleted += DisplayCompletedQuestName;

        questNameTextBox.DOFade(0, 0);

        questPanel.GetComponent<Image>().DOFade(0, 0);
        
        questPanel.SetActive(false);

    }
    private void DisplayStartingQuestName(Quest quest)
    {
        questPanel.SetActive(true);
        
        questNameTextBox.text = "Started: " + quest.questName;
        
        questNameTextBox.DOFade(255, 3);

        questPanel.GetComponent<Image>().DOFade(255, 3);

        StartCoroutine(WaitToDeactivate());
    }
    private void DisplayCompletedQuestName(Quest quest)
    {
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
