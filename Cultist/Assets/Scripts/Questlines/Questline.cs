using System;
using System.Collections.Generic;
using System.Linq;
using Questlines.SingleQuests;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Questline", menuName = "ScriptableObjects/Quests/Create new Questline", order = 1)]
public class Questline : ScriptableObject
{
   public string questlineName;
   public int questlineId;
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
      
      Campaign.OnCampaignComplete += ForceQuestCompletion;
   }

   private void StartQuestline(Quest startedQuest)
   {
      if (!questlineSteps.Any(quest => quest == startedQuest && !questlineStarted)) return;
      
      questlineStarted = true;
            
      OnQuestlineStart?.Invoke(this);

      SendStartedQuestlineData(questlineId.ToString(), questlineName);
   }

   private void ForceQuestCompletion(Campaign campaign)
   {
      if (campaign.campaignId != parentCampaign.campaignId || !questlineStarted) return;
      
      Debug.Log(campaign.campaignId + ":" + parentCampaign.campaignId);
      
      CompleteQuestline();

      foreach (var quest in questlineSteps.Where(quest => quest.questStarted))
      {
         quest.questFailed = true;
      }
   }

   private void CompleteQuestline()
   {
      questlineCompleted = true;
      
      OnQuestlineCompleted?.Invoke(this);
      
      SendCompletedQuestlineData(questlineId.ToString(), questlineName);

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
         CompleteQuestline();
      }
   }

   public void RestartQuestline()
   {
      Quest.OnQuestStarted += StartQuestline;
      
      Quest.OnQuestCompleted += CheckForRemainingOnQuests;

      Quest.OnQuestFailed += CheckForRemainingOnQuests;
      
      questlineStarted = false;

      questlineCompleted = false;

      foreach (var quest in questlineSteps)
      {
         quest.RestartQuest();
      }
   }

   public void ForceCompleteQuestline()
   {
      questlineStarted = true;

      questlineCompleted = true;

      foreach (var quest in questlineSteps)
      {
         quest.ForceCompleteQuest();
      }
   }
   
   private static void SendStartedQuestlineData(string idToPass, string nameToPass)
   {
      var parameters = new Dictionary<string, object>()
      {
         { "questlineStoryId", idToPass },
         { "questName", nameToPass }
      };
    
      AnalyticsService.Instance.CustomData("questlineStarted", parameters);
   }
    
   private static void SendCompletedQuestlineData(string idToPass, string nameToPass)
   {
      var parameters = new Dictionary<string, object>()
      {
         { "questlineStoryId", idToPass },
         { "questName", nameToPass }
      };
    
      AnalyticsService.Instance.CustomData("questlineCompleted", parameters);
   }
}
