using System;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Questlines.SingleQuests
{
    [CreateAssetMenu(fileName = "New Find Item Quest", menuName = "ScriptableObjects/Quests/Create new Find Item Quest")]
    public class QuestFindItem : Quest
    {
        [SerializeField] private BaseItem itemToFind;
        [SerializeField] private int quantityOfItemNeeded;
        
        [SerializeField] private int quantityOfItemInInventory;
        
        [TextArea(5, 20)]
        public string originalQuestDesc;
    
        [TextArea(3, 8)]
        public string originalShortQuestDesc;

        public static event Action<BaseItem> OnCheckForItemAtInventory;

        public static event Action<Quest> OnQuestUpdateStatus;

        protected override void StartQuest(QuestId questIdFromEvent)
        {
            base.StartQuest(questIdFromEvent);
            
            GameManager.OnReturnQuantityOfItems += UpdateItemQuantity;
            
            PlayerEvents.OnAddItemToInventory += CheckForItemInInventory;

            PlayerEvents.OnRemoveItemFromInventory += CheckForRemovedItem;

            questDesc = originalQuestDesc;

            shortQuestDesc = originalShortQuestDesc;

            OnCheckForItemAtInventory?.Invoke(itemToFind);
            
            questDesc = UpdateQuestDesc(originalQuestDesc);

            shortQuestDesc = UpdateQuestDesc(originalShortQuestDesc);
        }

        protected override void CompleteQuest(QuestId questIdFromEvent)
        {
            base.CompleteQuest(questIdFromEvent);

            questDesc = originalQuestDesc;

            shortQuestDesc = originalShortQuestDesc;
        }

        private void CheckForItemInInventory(BaseItem item)
        {
            if (item == itemToFind)
            {
                quantityOfItemInInventory += 1;
            }

            questDesc = UpdateQuestDesc(originalQuestDesc);

            shortQuestDesc = UpdateQuestDesc(originalShortQuestDesc);
            
            if (quantityOfItemInInventory >= quantityOfItemNeeded)
            {
                CompleteQuest(questId);
            }
            OnQuestUpdateStatus?.Invoke(this);
        }

        private void CheckForRemovedItem(BaseItem item)
        {
            if (item == itemToFind && quantityOfItemInInventory > 0)
            {
                quantityOfItemInInventory -= 1;
            }
            
            questDesc = UpdateQuestDesc(originalQuestDesc);

            shortQuestDesc = UpdateQuestDesc(originalShortQuestDesc);
        }

        private string UpdateQuestDesc(string desc)
        {
            var newDesc = desc + " Needed " + itemToFind.itemName.ToLower() + " " + quantityOfItemInInventory + "/" +
                          quantityOfItemNeeded + ".";
            
            return newDesc;
        }

        private void UpdateItemQuantity(int quantity)
        {
            quantityOfItemInInventory = quantity;
            
            questDesc = UpdateQuestDesc(originalQuestDesc);

            shortQuestDesc = UpdateQuestDesc(originalShortQuestDesc);
            
            if (quantityOfItemInInventory >= quantityOfItemNeeded)
            {
                CompleteQuest(questId);
            }
            
            OnQuestUpdateStatus?.Invoke(this);
        }
        protected override void StopListeningToQuestEvents()
        {
            base.StopListeningToQuestEvents();
            
            GameManager.OnReturnQuantityOfItems -= UpdateItemQuantity;
            
            PlayerEvents.OnAddItemToInventory -= CheckForItemInInventory;

            PlayerEvents.OnRemoveItemFromInventory -= CheckForRemovedItem;
        }
    }
}