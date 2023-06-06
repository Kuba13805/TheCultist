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

        public override void OnEnable()
        {
            base.OnEnable();
            
            InventoryItemDragDrop.OnItemAddedToInventory += CheckForItemInInventory;

            InventoryItemDragDrop.OnItemRemovedFromInventory += CheckForRemovedItem;

            GameManager.OnReturnQuantityOfItems += UpdateItemQuantity;
        }

        protected override void StartQuest(QuestId questIdFromEvent)
        {
            base.StartQuest(questIdFromEvent);
            
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
            return desc + " Needed " + itemToFind.itemName.ToLower() + " " + quantityOfItemInInventory + "/" +
                   quantityOfItemNeeded + ".";
        }

        private void UpdateItemQuantity(int quantity)
        {
            Debug.Log(quantity);
            quantityOfItemInInventory = quantity;
        }
        protected override void StopListeningToQuestEvents()
        {
            base.StopListeningToQuestEvents();
            
            InventoryItemDragDrop.OnItemAddedToInventory += CheckForItemInInventory;

            InventoryItemDragDrop.OnItemRemovedFromInventory += CheckForRemovedItem;
        }

        private void OnDisable()
        {
            questDesc = savedDesc;
            shortQuestDesc = savedShortDesc;
        }
    }
}