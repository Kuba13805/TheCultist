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

        private string savedDesc;
        private string savedShortDesc;

        public static event Action<BaseItem> OnCheckForItemAtInventory;

        public static event Action<Quest> OnQuestUpdateStatus;

        protected override void StartQuest(QuestId questIdFromEvent)
        {
            base.StartQuest(questIdFromEvent);
            
            GameManager.OnReturnQuantityOfItems += UpdateItemQuantity;
            
            InventoryItemDragDrop.OnItemAddedToInventory += CheckForItemInInventory;

            InventoryItemDragDrop.OnItemRemovedFromInventory += CheckForRemovedItem;

            // if (questDesc != savedDesc && savedDesc != null)
            // {
            //     Debug.Log("Desc changed!");
            //     questDesc = savedDesc;
            // }
            
            savedDesc = questDesc;
            
            savedShortDesc = shortQuestDesc;

            OnCheckForItemAtInventory?.Invoke(itemToFind);
            
            questDesc = UpdateQuestDesc(savedDesc);

            shortQuestDesc = UpdateQuestDesc(savedShortDesc);
        }

        private void CheckForItemInInventory(BaseItem item)
        {
            if (item == itemToFind)
            {
                quantityOfItemInInventory += 1;
            }

            questDesc = UpdateQuestDesc(savedDesc);

            shortQuestDesc = UpdateQuestDesc(savedShortDesc);
            
            if (quantityOfItemInInventory == quantityOfItemNeeded)
            {
                CompleteQuest(questId);
            }
        }

        private void CheckForRemovedItem(BaseItem item)
        {
            if (item == itemToFind && quantityOfItemInInventory > 0)
            {
                quantityOfItemInInventory -= 1;
            }
            
            questDesc = UpdateQuestDesc(savedDesc);

            shortQuestDesc = UpdateQuestDesc(savedShortDesc);
        }

        private string UpdateQuestDesc(string desc)
        {
            OnQuestUpdateStatus?.Invoke(this);
            
            return desc + " Needed " + itemToFind.itemName.ToLower() + " " + quantityOfItemInInventory + "/" +
                   quantityOfItemNeeded + ".";
        }

        private void UpdateItemQuantity(int quantity)
        {
            quantityOfItemInInventory = quantity;
        }
        protected override void StopListeningToQuestEvents()
        {
            base.StopListeningToQuestEvents();
            
            GameManager.OnReturnQuantityOfItems -= UpdateItemQuantity;
            
            InventoryItemDragDrop.OnItemAddedToInventory -= CheckForItemInInventory;

            InventoryItemDragDrop.OnItemRemovedFromInventory -= CheckForRemovedItem;
        }

        private void OnDisable()
        {
            questDesc = savedDesc;

            shortQuestDesc = savedShortDesc;
        }
    }
}