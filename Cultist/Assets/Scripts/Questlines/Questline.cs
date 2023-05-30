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

   private bool _questlineCompleted;

   [SerializeField] private int remainingQuests;

   #region Events

   public static event Action<Questline> OnQuestlineCompleted;

   #endregion

   private void OnEnable()
   {
      Quest.OnQuestCompleted += CheckForRemainingOnQuests;
   }

   private void OnDisable()
   {
      Quest.OnQuestCompleted -= CheckForRemainingOnQuests;
      
   }
   
   private void MarkQuestlineAsCompleted()
   {
      Debug.Log("Questline: " + questlineName + " has been completed!");
      OnQuestlineCompleted?.Invoke(this);
   }

   private void CheckForRemainingOnQuests(Quest completedQuest)
   {
      remainingQuests = questlineSteps.Count;
      var questIsInQuestline = false;
      
      foreach (var quest in questlineSteps.Where(quest => completedQuest == quest))
      {
         questIsInQuestline = true;
      }
      
      if (!questIsInQuestline)
      {
         return;
      }
      
      if (remainingQuests == 0)
      {
         MarkQuestlineAsCompleted();
         return;
      }
      
      remainingQuests -= 1;
   }
}
