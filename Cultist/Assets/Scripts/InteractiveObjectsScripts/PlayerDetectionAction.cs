using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

namespace InteractiveObjectsScripts
{
    [Serializable]
    public class PlayerDetectionAction
    {
        
        public bool notOneTimeAction;

        public ActionType actionType;

        [ShowIf("actionType", ActionType.CallForNarrativeEvent)]
        public NarrativeEvent narrativeEvent;
        
        [ShowIf("actionType", ActionType.CallForDialogue)]
        public DialogueInteraction dialogue;
        
        [ShowIf("actionType", ActionType.CallForTimeline)]
        public TimelineAsset timeline;
        
        [ShowIf("actionType", ActionType.CallForComment)]
        public string comment;
        [ShowIf("actionType", ActionType.CallForComment)]
        public GameObject commentOrigin;
        
        [ShowIf("actionType", ActionType.CallForScript)]
        public GameObject gameObjectWithScript;
    }
}
    public enum ActionType
    {
        CallForNarrativeEvent,
        CallForDialogue,
        CallForTimeline,
        CallForComment,
        CallForScript,
        CallForTest
    }
