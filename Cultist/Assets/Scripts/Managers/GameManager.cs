using System;
using System.Collections.Generic;
using Events;
using NaughtyAttributes;
using PlayerScripts;
using Questlines.SingleQuests;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Events

        public static event Action<bool> OnPlayerTestCheck;

        public static event Action<int> OnReturnQuantityOfItems;

        public static event Action<GameState> OnGameStateChanged;

        #endregion
        
        [BoxGroup("General")][FormerlySerializedAs("PlayerData")] public PlayerData playerData;
        [BoxGroup("General")][FormerlySerializedAs("State")] public GameState state;
        
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            
            CallContainerEvents.OnContainerOpen += PauseGame;
        
            CallContainerEvents.OnContainerClosed += ResumeGame;
        
            CallCollectableEvents.OnCollectableShown += PauseGame;
        
            CallCollectableEvents.OnCollectableClosed += ResumeGame;
            
            
            DialogueController.OnTestCheck += TestPlayerAbilities;

            DialogueController.OnDialogueShown += ChangeGameStateToDialogue;
            
            
            InventoryItemDragDrop.OnItemEquipped += AddItemToCharacterEquipment;
            
            InventoryItemDragDrop.OnItemEquipped += ActivateItemEffects;
        
            InventoryItemDragDrop.OnItemStriped += RemoveItemFromCharacterEquipment;

            InventoryItemDragDrop.OnItemStriped += DeactivateItemEffects;
            

            QuestFindItem.OnCheckForItemAtInventory += CheckForItemInInventory;
            

            QuestReturnItem.OnQuestItemRemove += RemoveQuestItem;

            CallGameManagerEvents.OnGamePause += HandleGamePause;
            

            PlayerEvents.OnAddItemToInventory += AddItemToInventory;

            PlayerEvents.OnRemoveItemFromInventory += RemoveItemFromInventory;
        }

        private static void HandleGamePause(bool boolean)
        {
            if (boolean)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        private void UpdateGameState(GameState newState)
        {
            state = newState;

            switch (newState)
            {
                case GameState.FreeMovement:
                    OnGameStateChanged?.Invoke(GameState.FreeMovement);
                    break;
                case GameState.Dialogue:
                    OnGameStateChanged?.Invoke(GameState.Dialogue);
                    break;
                case GameState.Narrative:
                    OnGameStateChanged?.Invoke(GameState.Narrative);
                    break;
                case GameState.MenuOpened:
                    OnGameStateChanged?.Invoke(GameState.MenuOpened);
                    break;
                default:
                    throw new ArgumentException("Wrong gamestate");
            }
        }
        

        private void TestPlayerAbilities(int baseTestDifficulty, string abilityToCheck)
        {
            var basePlayerNumber = 0;

            var checkedAbility = (ModifiedStat)Enum.Parse(typeof(ModifiedStat), abilityToCheck);
            switch (checkedAbility)
            {
                case ModifiedStat.Strength:
                    basePlayerNumber = playerData.strength.statValue;
                    break;
                case ModifiedStat.Dexterity:
                    basePlayerNumber = playerData.dexterity.statValue;
                    break;
                case ModifiedStat.Power:
                    basePlayerNumber = playerData.power.statValue;
                    break;
                case ModifiedStat.Wisdom:
                    basePlayerNumber = playerData.wisdom.statValue;
                    break;
                case ModifiedStat.Condition:
                    basePlayerNumber = playerData.condition.statValue;
                    break;
                case ModifiedStat.Perception:
                    basePlayerNumber = playerData.perception.statValue;
                    break;
                case ModifiedStat.Occultism:
                    basePlayerNumber = playerData.occultism.statValue;
                    break;
                case ModifiedStat.Medicine:
                    basePlayerNumber = playerData.medicine.statValue;
                    break;
                case ModifiedStat.Electrics:
                    basePlayerNumber = playerData.electrics.statValue;
                    break;
                case ModifiedStat.History:
                    basePlayerNumber = playerData.history.statValue;
                    break;
                case ModifiedStat.Persuasion:
                    basePlayerNumber = playerData.persuasion.statValue;
                    break;
                case ModifiedStat.Intimidation:
                    basePlayerNumber = playerData.intimidation.statValue;
                    break;
                case ModifiedStat.Locksmithing:
                    basePlayerNumber = playerData.locksmithing.statValue;
                    break;
                case ModifiedStat.Mechanics:
                    basePlayerNumber = playerData.mechanics.statValue;
                    break;
                case ModifiedStat.Acrobatics:
                    basePlayerNumber = playerData.acrobatics.statValue;
                    break;
                case ModifiedStat.Forensics:
                    basePlayerNumber = playerData.forensics.statValue;
                    break;
                case ModifiedStat.Acting:
                    basePlayerNumber = playerData.acting.statValue;
                    break;
                case ModifiedStat.Alchemy:
                    basePlayerNumber = playerData.alchemy.statValue;
                    break;
                case ModifiedStat.Astrology:
                    basePlayerNumber = playerData.astrology.statValue;
                    break;
                case ModifiedStat.Thievery:
                    basePlayerNumber = playerData.thievery.statValue;
                    break;
                case ModifiedStat.RangedCombat:
                    basePlayerNumber = playerData.rangedCombat.statValue;
                    break;
                case ModifiedStat.HandToHandCombat:
                    basePlayerNumber = playerData.handToHandCombat.statValue;
                    break;
                case ModifiedStat.Etiquette:
                    basePlayerNumber = playerData.etiquette.statValue;
                    break;
                case ModifiedStat.Animism:
                    basePlayerNumber = playerData.animism.statValue;
                    break;
                case ModifiedStat.Empathy:
                    basePlayerNumber = playerData.empathy.statValue;
                    break;
                case ModifiedStat.Demonology:
                    basePlayerNumber = playerData.demonology.statValue;
                    break;
                case ModifiedStat.Stealth:
                    basePlayerNumber = playerData.stealth.statValue;
                    break;
                case ModifiedStat.Necromancy:
                    basePlayerNumber = playerData.necromancy.statValue;
                    break;
                default:
                    basePlayerNumber = basePlayerNumber;
                    break;
            }

            var testPlayer = new TestPlayer();

            var testResult = TestPlayer.TestAbility(basePlayerNumber, baseTestDifficulty);
            
            OnPlayerTestCheck?.Invoke(testResult);
        }
        #region ControllGameFlow
        private static void PauseGame()
        {
            Time.timeScale = 0;
        }

        private static void ResumeGame()
        {
            Time.timeScale = 1;
        }
        #endregion

        #region ChangeGameStateOptions

        private void ChangeGameStateToDialogue()
        {
            UpdateGameState(GameState.Dialogue);
        }

        private void ChangeGameStateToNarrative()
        {
            UpdateGameState(GameState.Narrative);
        }

        private void ChangeGameStateToMenu()
        {
            UpdateGameState(GameState.MenuOpened);
        }

        private void ChangeGameStateToFreeMovement()
        {
            UpdateGameState(GameState.FreeMovement);
        }
        #endregion

        #region HandlePlayerInventory

        private void AddItemToInventory(BaseItem item)
        {
            AddItemToList(playerData.playerInventoryItems, item);
        }

        private void RemoveItemFromInventory(BaseItem item)
        {
            RemoveItemFromList(playerData.playerInventoryItems, item);
        }

        private void AddItemToCharacterEquipment(BaseItem item)
        {
            AddItemToList(playerData.characterEquipment, item);
            RemoveItemFromList(playerData.playerInventoryItems, item);
        }

        private void RemoveItemFromCharacterEquipment(BaseItem item)
        {
            AddItemToList(playerData.playerInventoryItems, item);
            RemoveItemFromList(playerData.characterEquipment, item);
        }

        private static void AddItemToList(ICollection<BaseItem> listOfItems, BaseItem itemToAdd)
        {
            listOfItems.Add(itemToAdd);
        }

        private static void RemoveItemFromList(IList<BaseItem> listOfItems, BaseItem itemToRemove)
        {
            for (var i = 0; i < listOfItems.Count; i++)
            {
                if (listOfItems[i] != itemToRemove) continue;
                listOfItems.Remove(listOfItems[i]);
                break;
            }
        }

        private void CheckForItemInInventory(BaseItem itemToFind)
        {
            var quantityOfItemsFound = 0;
            
            foreach (var item in playerData.playerInventoryItems)
            {
                if (item == itemToFind)
                {
                    quantityOfItemsFound += 1;
                }
            }
            OnReturnQuantityOfItems?.Invoke(quantityOfItemsFound);
        }
        
        private void RemoveQuestItem(BaseItem itemToRemove, int quantityOfItemToRemove)
        {
            for (var i = 0; i < quantityOfItemToRemove; i++)
            {
                for (var j = 0; j < playerData.playerInventoryItems.Count; j++)
                {
                    if (playerData.playerInventoryItems[j] != itemToRemove) continue;
                    
                    playerData.playerInventoryItems.Remove(playerData.playerInventoryItems[j]);
                        
                    break;
                }
            }
        }
        #endregion

        #region HandlePlayerStats

        private void ActivateItemEffects(BaseItem item)
        {
            foreach (var multipleEffect in item.effectsOnItem)
            {
                foreach (var effect in multipleEffect.listOfAdditionalEffects)
                {
                    AffectStat(effect, playerData, true);
                }
            }
        }

        private void DeactivateItemEffects(BaseItem item)
        {
            foreach (var multipleEffect in item.effectsOnItem)
            {
                foreach (var effect in multipleEffect.listOfAdditionalEffects)
                {
                    AffectStat(effect, playerData, false);
                }
            }
        }

        private static void AffectStat(ItemEffect itemEffect, PlayerData playerDataSet, bool isEffectActive)
        {
            switch (itemEffect.statToEffect)
            {
                case ModifiedStat.Strength:
                    playerDataSet.strength.statValue = CalculateStatValue(playerDataSet.strength.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Dexterity:
                    playerDataSet.dexterity.statValue = CalculateStatValue(playerDataSet.dexterity.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Power:
                    playerDataSet.power.statValue = CalculateStatValue(playerDataSet.power.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Wisdom:
                    playerDataSet.wisdom.statValue = CalculateStatValue(playerDataSet.wisdom.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Condition:
                    playerDataSet.condition.statValue = CalculateStatValue(playerDataSet.condition.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Perception:
                    playerDataSet.perception.statValue = CalculateStatValue(playerDataSet.perception.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Occultism:
                    playerDataSet.occultism.statValue = CalculateStatValue(playerDataSet.occultism.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Medicine:
                    playerDataSet.medicine.statValue = CalculateStatValue(playerDataSet.medicine.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Electrics:
                    playerDataSet.electrics.statValue = CalculateStatValue(playerDataSet.electrics.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.History:
                    playerDataSet.history.statValue = CalculateStatValue(playerDataSet.history.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Persuasion:
                    playerDataSet.persuasion.statValue = CalculateStatValue(playerDataSet.persuasion.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Intimidation:
                    playerDataSet.intimidation.statValue = CalculateStatValue(playerDataSet.intimidation.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Locksmithing:
                    playerDataSet.locksmithing.statValue = CalculateStatValue(playerDataSet.locksmithing.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Mechanics:
                    playerDataSet.mechanics.statValue = CalculateStatValue(playerDataSet.mechanics.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Acrobatics:
                    playerDataSet.acrobatics.statValue = CalculateStatValue(playerDataSet.acrobatics.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Forensics:
                    playerDataSet.forensics.statValue = CalculateStatValue(playerDataSet.forensics.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Acting:
                    playerDataSet.acting.statValue = CalculateStatValue(playerDataSet.acting.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Alchemy:
                    playerDataSet.alchemy.statValue = CalculateStatValue(playerDataSet.alchemy.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Astrology:
                    playerDataSet.astrology.statValue = CalculateStatValue(playerDataSet.astrology.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Thievery:
                    playerDataSet.thievery.statValue = CalculateStatValue(playerDataSet.thievery.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.RangedCombat:
                    playerDataSet.rangedCombat.statValue = CalculateStatValue(playerDataSet.rangedCombat.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.HandToHandCombat:
                    playerDataSet.handToHandCombat.statValue = CalculateStatValue(playerDataSet.handToHandCombat.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Etiquette:
                    playerDataSet.etiquette.statValue = CalculateStatValue(playerDataSet.etiquette.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Animism:
                    playerDataSet.animism.statValue = CalculateStatValue(playerDataSet.animism.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Empathy:
                    playerDataSet.empathy.statValue = CalculateStatValue(playerDataSet.empathy.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Demonology:
                    playerDataSet.demonology.statValue = CalculateStatValue(playerDataSet.demonology.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Stealth:
                    playerDataSet.stealth.statValue = CalculateStatValue(playerDataSet.stealth.statValue, itemEffect, isEffectActive);
                    break;
                case ModifiedStat.Necromancy:
                    playerDataSet.necromancy.statValue = CalculateStatValue(playerDataSet.necromancy.statValue, itemEffect, isEffectActive);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static int CalculateStatValue(int playerDataStat, ItemEffect effect, bool isEffectActive)
        {
            var updatedStat = playerDataStat;
            if (isEffectActive)
            {
                if (effect.typeOfInfluence == ItemEffect.TypesOfInfluenceOnStat.IncreaseStat)
                {
                    updatedStat += effect.pointsAffecting;
                }
                else
                {
                    updatedStat -= effect.pointsAffecting;
                }
            }
            else
            {
                if (effect.typeOfInfluence == ItemEffect.TypesOfInfluenceOnStat.IncreaseStat)
                {
                    updatedStat -= effect.pointsAffecting;
                }
                else
                {
                    updatedStat += effect.pointsAffecting;
                }
            }
            return updatedStat;
        }

        #endregion
    }
}
public enum GameState
{
    FreeMovement,
    Dialogue,
    Narrative,
    MenuOpened
}

public enum ModifiedStat
{
    Strength,
    Dexterity,
    Power,
    Wisdom,
    Condition,
    Perception,
    Occultism,
    Medicine,
    Electrics,
    History,
    Persuasion,
    Intimidation,
    Locksmithing,
    Mechanics,
    Acrobatics,
    Forensics,
    Acting,
    Alchemy,
    Astrology,
    Thievery,
    RangedCombat,
    HandToHandCombat,
    Etiquette,
    Animism,
    Empathy,
    Demonology,
    Stealth,
    Necromancy
}
