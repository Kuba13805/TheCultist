using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
public class NarrativeEventAction
{
    #region Events

    public static event Action<string> OnSceneLoad;
    
    public static event Action<Quest> OnQuestComplete; 
    
    public static event Action<Quest> OnQuestStart;
    
    public static event Action<BaseItem> OnAddItem; 
    
    public static event Action<BaseItem> OnRemoveItem; 

    #endregion
    
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

    public void DoAction()
    {
        switch (currentAction)
        {
            case EventAction.LoadScene:
                OnSceneLoad?.Invoke(sceneToLoad);
                break;
            case EventAction.CompleteQuest:
                OnQuestComplete?.Invoke(questToComplete);
                break;
            case EventAction.StartQuest:
                OnQuestStart?.Invoke(questToStart);
                break;
            case EventAction.AddItem:
                OnAddItem?.Invoke(itemToAdd);
                break;
            case EventAction.RemoveItem:
                OnRemoveItem?.Invoke(itemToRemove);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
