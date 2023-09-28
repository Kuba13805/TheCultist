using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNarrativeEvent", menuName = "ScriptableObjects/Create New Narrative Event")]
public class NarrativeEvent : ScriptableObject
{
    public static event Action<NarrativeEvent> CallForEventToOpen;
    
    [SerializeField] private NarrativeEventId narrativeEventId;

    [SerializeField] private TextAsset narrativeEventText;

    [SerializeField] private List<NarrativeEventAction> actionsList;

    public void CallForEvent()
    {
        CallForEventToOpen?.Invoke(this);
    }
    private void DoAllActions()
    {
        foreach (var action in actionsList)
        {
            action.DoAction();
        }
    }
}
