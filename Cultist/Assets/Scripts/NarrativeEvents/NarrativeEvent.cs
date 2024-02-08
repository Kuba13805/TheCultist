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

    [NonReorderable]
    public List<Sprite> eventImages;

    [NonReorderable]
    public List<BaseItem> itemsInEvent;

    public void CallForEvent()
    {
        CallForEventToOpen?.Invoke(this);
    }
}
