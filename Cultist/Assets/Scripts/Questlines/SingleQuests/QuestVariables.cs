using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Questlines.SingleQuests
{
    [System.Serializable]
    public class QuestVariables
    {
        [FormerlySerializedAs("variableName")] public string variableCodeName;

        public string variableName;
        
        [Label("Major decision")][AllowNesting] public bool isMajorDecision;
        
        [TextArea(3, 8)]
        public string conditionPassedDesc;
        
        [TextArea(3, 8)]
        public string conditionNotPassedDesc;
        
        [ShowIf("isMajorDecision")]
        public Sprite variableIcon;
        
        [Label("Passed")][AllowNesting] public bool conditionPassed;
    }
}