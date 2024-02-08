using System;
using System.Collections;
using System.Collections.Generic;
using Questlines.SingleQuests;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Return Item Quest", menuName = "ScriptableObjects/Quests/Create new Return Item Quest")]
public class QuestReturnItem : Quest
{
    [SerializeField] private BaseItem itemToReturn;
    
    [SerializeField] private int numberOfItemsToRemove;
    
    public static event Action<BaseItem, int> OnQuestItemRemove;
    protected override void CompleteQuest(QuestId questIdFromEvent)
    {
        base.CompleteQuest(questIdFromEvent);
        
        OnQuestItemRemove?.Invoke(itemToReturn, numberOfItemsToRemove);
    }
}
