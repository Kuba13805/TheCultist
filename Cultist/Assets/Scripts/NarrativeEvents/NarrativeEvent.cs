using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewNarrativeEvent", menuName = "ScriptableObjects/Create New Narrative Event")]
public class NarrativeEvent : ScriptableObject
{
    
    [SerializeField] private NarrativeEventId narrativeEventId;

    [SerializeField] private TextAsset narrativeEventText;

    [SerializeField] private List<NarrativeEventAction> actionsList;

}
