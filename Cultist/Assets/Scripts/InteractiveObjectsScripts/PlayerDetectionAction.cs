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
        
        public bool oneTimeAction = true;

        public ActionType actionType;

        [ShowIf("actionType", ActionType.CallForNarrativeEvent)][AllowNesting]
        public NarrativeEvent narrativeEvent;
        
        [ShowIf("actionType", ActionType.CallForDialogue)][AllowNesting]
        public DialogueInteraction dialogue;
        
        [ShowIf("actionType", ActionType.CallForTimeline)][AllowNesting]
        public TimelineAsset timeline;
        
        [ShowIf("actionType", ActionType.CallForComment)][AllowNesting][TextArea(1, 5)]
        public string comment;
        [ShowIf("actionType", ActionType.CallForComment)][AllowNesting]
        public GameObject commentOrigin;
        
        [ShowIf("actionType", ActionType.CallForTest)][AllowNesting]
        public Stat skillToTest;
        
        [ShowIf("actionType", ActionType.CallForTest)][AllowNesting]
        public int testDifficulty;
        
        [ShowIf("actionType", ActionType.CallForTest)][AllowNesting]
        public GameObject testGameObject;
    }
}
    public enum ActionType
    {
        CallForNarrativeEvent,
        CallForDialogue,
        CallForTimeline,
        CallForComment,
        CallForTest
    }
