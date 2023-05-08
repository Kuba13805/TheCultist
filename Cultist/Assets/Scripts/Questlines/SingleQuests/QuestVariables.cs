using UnityEngine;

namespace Questlines.SingleQuests
{
    [System.Serializable]
    public class QuestVariables
    {
        public string variableName;
        [TextArea(4, 6)]
        public string conditionDesc;
        public bool conditionPassed;
    }
}