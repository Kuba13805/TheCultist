using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Questlines.SingleQuests
{
    [System.Serializable]
    public class QuestVariables
    {
        [FormerlySerializedAs("variableName")] public string variableCodeName;

        public string variableName;
        public Quest parentQuest;
        
        [Label("Major decision")][AllowNesting] public bool isMajorDecision;
        
        [TextArea(3, 8)]
        public string conditionPassedDesc;
        
        [TextArea(3, 8)]
        public string conditionNotPassedDesc;
        
        [ShowIf("isMajorDecision")]
        public Sprite variableIcon;
        
        [Label("Passed")][AllowNesting] public bool conditionPassed;

        public void MarkVariableComplete()
        {
            SendVariableData(variableName, parentQuest.questId.ToString());
            
            conditionPassed = true;
        }

        private static void SendVariableData(string variableName, string parentQuest)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "decisionName", variableName },
                { "decisionQuest", parentQuest }
            };

            AnalyticsService.Instance.CustomData("playerDecision", parameters);
        }
    }
}