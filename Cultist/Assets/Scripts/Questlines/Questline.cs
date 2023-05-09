using System;
using System.Collections.Generic;
using System.Linq;
using Questlines.SingleQuests;
using UnityEngine;

[CreateAssetMenu(fileName = "New Questline", menuName = "ScriptableObjects/Quests/Create new Questline", order = 1)]
public class Questline : ScriptableObject
{
   public string questlineName;
   public List<Quest> questlineSteps;

   private bool _questlineCompleted;

   private int _remainingQuests;

   #region Events

   public static event Action<Questline> OnQuestlineCompleted;

   #endregion

   private void OnEnable()
   {
      Quest.OnQuestCompleted += CheckForRemainingOnQuests;

      _remainingQuests = questlineSteps.Count;
   }

   private void OnDisable()
   {
      Quest.OnQuestCompleted -= CheckForRemainingOnQuests;
      
   }
   
   private void MarkQuestlineAsCompleted()
   {
      OnQuestlineCompleted?.Invoke(this);
   }

   private void CheckForRemainingOnQuests(Quest completedQuest)
   {
      var questIsInQuestline = false;
      
      foreach (var quest in questlineSteps.Where(quest => completedQuest == quest))
      {
         questIsInQuestline = true;
      }
      
      if (!questIsInQuestline)
      {
         return;
      }
      
      if (_remainingQuests == 0)
      {
         MarkQuestlineAsCompleted();
         return;
      }
      
      _remainingQuests -= 1;
   }
}
