using System;
using System.Collections.Generic;
using Questlines.SingleQuests;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObjects/Quests/Create new Quest")]
public class Quest : ScriptableObject
{
    
    public string questName;

    [TextArea(15, 20)]
    public string questDesc;
        
    [SerializeField] private bool _questCompleted;

    public List<QuestVariables> questVariables;

    #region Events

    public static event Action<Quest> QuestCompleted; 

    #endregion

    private void OnEnable()
    {
        foreach (var questVariable in questVariables)
        {
            var trim = questVariable.variableName.Trim();

            questVariable.variableName = trim;
        }
    }

    private void MarkQuestAsCompleted()
    {
        _questCompleted = true;
            
        QuestCompleted?.Invoke(this);
    }
}
