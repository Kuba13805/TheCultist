using System;
using System.Collections.Generic;
using System.Linq;
using Questlines.SingleQuests;
using UnityEngine;

namespace Questlines
{
    [CreateAssetMenu(fileName = "New QuestLog", menuName = "ScriptableObjects/Create new QuestLog")]
    public class QuestLog : ScriptableObject
    {
        [SerializeField] private List<Questline> activeQuestslines;

        [SerializeField] private List<Questline> completedQuestlines;

        #region Events

        public static event Action<List<QuestVariables>> OnQuestVariablesReturn;

        #endregion

        private void OnEnable()
        {
            Questline.OnQuestlineCompleted += AddOnQuestlineToCompleted;

            DialogueController.OnCallVariables += ReturnNeededVariables;

            DialogueController.OnSetNewVariables += SetNewVariablesValue;
        }

        private void OnDisable()
        {
            Questline.OnQuestlineCompleted -= AddOnQuestlineToCompleted;
            
            DialogueController.OnCallVariables -= ReturnNeededVariables;
            
            DialogueController.OnSetNewVariables -= SetNewVariablesValue;
        }

        private void AddOnQuestlineToCompleted(Questline questline)
        {
            completedQuestlines.Add(questline);
            activeQuestslines.Remove(questline);
        }
        private void ReturnNeededVariables(List<string> listOfNeededVariables)
        {
            var listOfActiveQuestVariables = (from t in listOfNeededVariables from questLine in activeQuestslines from quest 
                in questLine.questlineSteps from variable in quest.questVariables where variable.variableName == t select variable).ToList();
            
            var listOfCompletedQuestVariables = (from t in listOfNeededVariables from questLine in completedQuestlines from quest 
                in questLine.questlineSteps from variable in quest.questVariables where variable.variableName == t select variable).ToList();

            listOfActiveQuestVariables.AddRange(listOfCompletedQuestVariables);

            OnQuestVariablesReturn?.Invoke(listOfActiveQuestVariables);
        }
        
        private void SetNewVariablesValue(List<QuestVariables> listOfNewValues)
        {
            foreach (var newValue in listOfNewValues)
            {
                foreach (var variable in from questline in activeQuestslines from quest in questline.questlineSteps from variable in quest.questVariables where newValue.variableName == variable.variableName select variable)
                {
                    variable.conditionPassed = newValue.conditionPassed;
                }
            }
        }

        public List<Questline> ReturnActiveQuestlines()
        {
            return activeQuestslines;
        }

        public List<Questline> ReturnCompletedQuestlines()
        {
            return completedQuestlines;
        }
    }
}