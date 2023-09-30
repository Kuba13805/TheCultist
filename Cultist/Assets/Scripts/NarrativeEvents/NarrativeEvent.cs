using System;
using System.Collections.Generic;
using Events;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNarrativeEvent", menuName = "ScriptableObjects/Create New Narrative Event")]
public class NarrativeEvent : ScriptableObject
{
    public static event Action<NarrativeEvent> CallForEventToOpen;
    
    public NarrativeEventId narrativeEventId;

    public TextAsset narrativeEventText;

    public List<Sprite> eventImages;

    public List<NarrativeEventAction> actionsList;

    public void CallForEvent()
    {
        CallForEventToOpen?.Invoke(this);
    }
    public void DoAllActions()
    {
        foreach (var action in actionsList)
        {
            action.DoAction();
        }
    }
}
