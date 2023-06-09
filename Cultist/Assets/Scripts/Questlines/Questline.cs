using System;
using System.Collections.Generic;
using System.Linq;
using Questlines.SingleQuests;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Questline", menuName = "ScriptableObjects/Quests/Create new Questline", order = 1)]
public class Questline : ScriptableObject
{
   public string questlineName;
   public List<Quest> questlineSteps;

   public bool questlineVisible;

   public bool questlineStarted;

   public bool questlineCompleted;

   [SerializeField] private int remainingQuests;

   #region Events

   public static event Action<Questline> OnQuestlineCompleted;

   #endregion

   private void OnEnable()
   {
      Quest.OnQuestStarted += StartQuestline;
      
      Quest.OnQuestCompleted += CheckForRemainingOnQuests;
   }

   private void StartQuestline(Quest startedQuest)
   {
      remainingQuests = questlineSteps.Count;
      
      foreach (var quest in questlineSteps)
      {
         if (quest == startedQuest && !questlineStarted)
         {
            questlineStarted = true;
         }
      }
   }

   private void MarkQuestlineAsCompleted()
   {
      Debug.Log("Questline: " + questlineName + " has been completed!");

      questlineCompleted = true;
      
      OnQuestlineCompleted?.Invoke(this);
      
      Quest.OnQuestCompleted -= CheckForRemainingOnQuests;
      
      Quest.OnQuestStarted -= StartQuestline;
   }

   private void CheckForRemainingOnQuests(Quest completedQuest)
   {
      var questIsInQuestline = true;
      
      foreach (var quest in questlineSteps.Where(quest => completedQuest == quest))
      {
         if (quest.questId.idPrefix != completedQuest.questId.idPrefix && quest.questId.questNumber != completedQuest.questId.questNumber)
         {
            questIsInQuestline = false;
         }
      }
      Debug.Log(questIsInQuestline);
      if (!questIsInQuestline)
      {
         return;
      }
      
      remainingQuests -= 1;
      
      if (remainingQuests == 0)
      {
         Debug.Log("Mark complete!");
         MarkQuestlineAsCompleted();
      }
   }
}
