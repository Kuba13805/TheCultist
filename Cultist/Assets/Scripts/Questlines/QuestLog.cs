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
        [SerializeField] private List<Questline> activeQuestlines;

        [SerializeField] private List<Questline> completedQuestlines;

        #region Events

        public static event Action<List<QuestVariables>> OnQuestVariablesReturn;

        #endregion

        private void OnEnable()
        {
            Questline.OnQuestlineCompleted += AddQuestlineToCompleted;

            Questline.OnQuestlineStart += AddQuestlineToActive;

            DialogueController.OnCallVariables += ReturnNeededVariables;

            DialogueController.OnSetNewVariables += SetNewVariablesValue;

            NewGameManager.OnNewGameStart += RestartPlayerLog;
        }

        private void RestartPlayerLog(Campaign obj)
        {
            activeQuestlines.Clear();
            
            completedQuestlines.Clear();
        }

        private void OnDisable()
        {
            Questline.OnQuestlineCompleted -= AddQuestlineToCompleted;
            
            Questline.OnQuestlineStart -= AddQuestlineToActive;
            
            DialogueController.OnCallVariables -= ReturnNeededVariables;
            
            DialogueController.OnSetNewVariables -= SetNewVariablesValue;
            
            NewGameManager.OnNewGameStart -= RestartPlayerLog;
        }

        private void AddQuestlineToActive(Questline questline)
        {
            if (activeQuestlines.Any(activeQuestline => activeQuestline.questlineId == questline.questlineId))
            {
                return;
            }

            activeQuestlines.Add(questline);
        }
        private void AddQuestlineToCompleted(Questline questline)
        {
            if (activeQuestlines.Any(activeQuestline => activeQuestline == questline))
            {
                return;
            }

            activeQuestlines.Remove(questline);

            if (completedQuestlines.Any(completedQuestline => completedQuestline == questline))
            {
                return;
            }

            completedQuestlines.Add(questline);
        }
        private void ReturnNeededVariables(List<string> listOfNeededVariables)
        {
            var listOfActiveQuestVariables = (from t in listOfNeededVariables from questLine in activeQuestlines from quest 
                in questLine.questlineSteps from variable in quest.questVariables where variable.variableCodeName == t select variable).ToList();
            
            var listOfCompletedQuestVariables = (from t in listOfNeededVariables from questLine in completedQuestlines from quest 
                in questLine.questlineSteps from variable in quest.questVariables where variable.variableCodeName == t select variable).ToList();

            listOfActiveQuestVariables.AddRange(listOfCompletedQuestVariables);

            OnQuestVariablesReturn?.Invoke(listOfActiveQuestVariables);
        }
        
        private void SetNewVariablesValue(List<QuestVariables> listOfNewValues)
        {
            foreach (var newValue in listOfNewValues)
            {
                foreach (var variable in from questline in activeQuestlines from quest in questline.questlineSteps from variable in quest.questVariables where newValue.variableCodeName == variable.variableCodeName select variable)
                {
                    if (newValue.conditionPassed)
                        variable.MarkVariableComplete();
                }
            }
        }

        public List<Questline> ReturnActiveQuestlines()
        {
            return activeQuestlines;
        }

        public List<Questline> ReturnCompletedQuestlines()
        {
            return completedQuestlines;
        }

        private void ClearQuestLog()
        {
            activeQuestlines.Clear();
            
            completedQuestlines.Clear();
        }
    }
}