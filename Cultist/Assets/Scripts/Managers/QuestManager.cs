using System;
using System.Collections.Generic;
using Questlines;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private QuestLog currentQuestLog;

    public static event Action<List<Questline>, List<Questline>> OnQuestLogPass;
    
    private void OnEnable()
    {
        DisplayQuestlogContent.OnQuestLogRequest += PassQuestLog;
    }

    private void OnDisable()
    {
        DisplayQuestlogContent.OnQuestLogRequest -= PassQuestLog;
    }

    private void PassQuestLog()
    {
        OnQuestLogPass?.Invoke(currentQuestLog.ReturnActiveQuestlines(), currentQuestLog.ReturnCompletedQuestlines());
    }
}
