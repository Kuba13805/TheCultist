using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
public class NarrativeEventAction
{
    private enum EventAction
    {
        LoadScene,
        StartQuest,
        CompleteQuest,
        AddItem,
        RemoveItem
    }

    [SerializeField] [AllowNesting] private EventAction currentAction;
    
    [SerializeField][Scene][AllowNesting][ShowIf("currentAction", EventAction.LoadScene)]
    private string sceneToLoad;

    [SerializeField][AllowNesting][ShowIf("currentAction", EventAction.CompleteQuest)]
    private Quest questToComplete;
    
    [SerializeField][AllowNesting][ShowIf("currentAction", EventAction.StartQuest)]
    private Quest questToStart;
    
    [SerializeField][AllowNesting][ShowIf("currentAction", EventAction.AddItem)]
    private BaseItem itemToAdd;
    
    [SerializeField][AllowNesting][ShowIf("currentAction", EventAction.RemoveItem)]
    private BaseItem itemToRemove;
}
