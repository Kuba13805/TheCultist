using NaughtyAttributes;
using UnityEngine;

namespace Questlines.SingleQuests
{
    [System.Serializable]
    public class QuestVariables
    {
        public string variableName;
        
        [Label("Major decision")][AllowNesting] public bool isMajorDecision;
        
        [TextArea(3, 8)]
        public string conditionPassedDesc;
        
        [TextArea(3, 8)]
        public string conditionNotPassedDesc;

        
        [Label("Passed")][AllowNesting] public bool conditionPassed;
    }
}