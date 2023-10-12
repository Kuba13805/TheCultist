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
   [SerializeField] private Campaign parentCampaign;

   public bool questlineVisible;

   public bool questlineStarted;

   public bool questlineCompleted;

   #region Events

   public static event Action<Questline> OnQuestlineCompleted;

   public static event Action<Questline> OnQuestlineStart; 

   #endregion

   private void OnEnable()
   {
      Quest.OnQuestStarted += StartQuestline;
      
      Quest.OnQuestCompleted += CheckForRemainingOnQuests;

      Quest.OnQuestFailed += CheckForRemainingOnQuests;
   }

   private void StartQuestline(Quest startedQuest)
   {
      foreach (var quest in questlineSteps)
      {
         if (quest != startedQuest || questlineStarted) continue;
         
         questlineStarted = true;
            
         OnQuestlineStart?.Invoke(this);

         Campaign.OnCampaignComplete += ForceQuestCompletion;

         return;
      }
   }

   private void ForceQuestCompletion(Campaign campaign)
   {
      if (campaign.ToString() != parentCampaign.ToString() || !questlineStarted) return;
      
      MarkQuestlineAsCompleted();

      foreach (var quest in questlineSteps.Where(quest => quest.questStarted))
      {
         quest.questFailed = true;
      }
   }

   private void MarkQuestlineAsCompleted()
   {
      questlineCompleted = true;
      
      OnQuestlineCompleted?.Invoke(this);

      Quest.OnQuestCompleted -= CheckForRemainingOnQuests;
      
      Quest.OnQuestStarted -= StartQuestline;
      
      Quest.OnQuestFailed -= CheckForRemainingOnQuests;
   }

   private void CheckForRemainingOnQuests(Quest completedQuest)
   {
      var allQuestsCompleted = true;
      
      foreach (var quest in questlineSteps)
      {
         if (quest.questCompleted) continue;
         
         allQuestsCompleted = false;
            
         break;
      }

      if (allQuestsCompleted)
      {
         MarkQuestlineAsCompleted();
      }
   }

   public void RestartQuestline()
   {
      questlineStarted = false;

      questlineCompleted = false;

      foreach (var quest in questlineSteps)
      {
         quest.RestartQuest();
      }
   }
}
