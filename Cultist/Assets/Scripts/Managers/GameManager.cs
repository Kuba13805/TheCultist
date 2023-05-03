using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using PlayerScripts;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Events

        public static event Action<bool> OnPlayerTestCheck;

        public static event Action<GameState> OnGameStateChanged;

        #endregion
        
        [BoxGroup("General")][FormerlySerializedAs("PlayerData")] public PlayerData playerData;
        [BoxGroup("General")][FormerlySerializedAs("State")] public GameState state;
        
        public static GameManager Instance { get; private set; }

        [BoxGroup("InventoryHandle")][SerializeField] private int maxInventoryItemId;
        
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
            
            
            InventoryItemDragDrop.OnItemAddedToInventory += AddItemToInventory;
        
            InventoryItemDragDrop.OnItemRemovedFromInventory += RemoveItemFromInventory;
        
            InventoryItemDragDrop.OnItemEquipped += AddItemToCharacterEquipment;
            
            InventoryItemDragDrop.OnItemEquipped += ActivateItemEffects;
        
            InventoryItemDragDrop.OnItemStriped += RemoveItemFromCharacterEquipment;

            InventoryItemDragDrop.OnItemStriped += DeactivateItemEffects;
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

        public enum GameState
        {
            FreeMovement,
            Dialogue,
            Narrative,
            MenuOpened
        }

        private enum Ability
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
            Psychology
        }

        private void TestPlayerAbilities(int baseTestDifficulty, string abilityToCheck)
        {
            var basePlayerNumber = 0;

            var checkedAbility = (Ability)Enum.Parse(typeof(Ability), abilityToCheck);
            basePlayerNumber = checkedAbility switch
            {
                Ability.Strength => playerData.strength,
                Ability.Dexterity => playerData.dexterity,
                Ability.Power => playerData.power,
                Ability.Wisdom => playerData.wisdom,
                Ability.Condition => playerData.condition,
                Ability.Perception => playerData.perception,
                Ability.Occultism => playerData.occultism,
                Ability.Medicine => playerData.medicine,
                Ability.Electrics => playerData.electrics,
                Ability.History => playerData.history,
                Ability.Persuasion => playerData.persuasion,
                Ability.Intimidation => playerData.intimidation,
                Ability.Locksmithing => playerData.locksmithing,
                Ability.Mechanics => playerData.mechanics,
                Ability.Psychology => playerData.psychology,
                _ => basePlayerNumber
            };

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
                case ItemEffect.CharStatsToEffect.Perception:
                    playerDataSet.perception = CalculateStatValue(playerDataSet.perception, itemEffect, isEffectActive);
                    break;
                case ItemEffect.CharStatsToEffect.Occultism:
                    playerDataSet.occultism = CalculateStatValue(playerDataSet.occultism, itemEffect, isEffectActive);
                    break;
                case ItemEffect.CharStatsToEffect.Medicine:
                    playerDataSet.medicine = CalculateStatValue(playerDataSet.medicine, itemEffect, isEffectActive);
                    break;
                case ItemEffect.CharStatsToEffect.Electrics:
                    playerDataSet.electrics = CalculateStatValue(playerDataSet.electrics, itemEffect, isEffectActive);
                    break;
                case ItemEffect.CharStatsToEffect.History:
                    playerDataSet.history = CalculateStatValue(playerDataSet.history, itemEffect, isEffectActive);
                    break;
                case ItemEffect.CharStatsToEffect.Persuasion:
                    playerDataSet.persuasion = CalculateStatValue(playerDataSet.persuasion, itemEffect, isEffectActive);
                    break;
                case ItemEffect.CharStatsToEffect.Intimidation:
                    playerDataSet.intimidation = CalculateStatValue(playerDataSet.intimidation, itemEffect, isEffectActive);
                    break;
                case ItemEffect.CharStatsToEffect.Locksmithing:
                    playerDataSet.locksmithing = CalculateStatValue(playerDataSet.locksmithing, itemEffect, isEffectActive);
                    break;
                case ItemEffect.CharStatsToEffect.Mechanics:
                    playerDataSet.mechanics = CalculateStatValue(playerDataSet.mechanics, itemEffect, isEffectActive);
                    break;
                case ItemEffect.CharStatsToEffect.Psychology:
                    playerDataSet.psychology = CalculateStatValue(playerDataSet.psychology, itemEffect, isEffectActive);
                    break;
                case ItemEffect.CharStatsToEffect.Strength:
                    playerDataSet.strength = CalculateStatValue(playerDataSet.strength, itemEffect, isEffectActive);
                    break;
                case ItemEffect.CharStatsToEffect.Dexterity:
                    playerDataSet.dexterity = CalculateStatValue(playerDataSet.dexterity, itemEffect, isEffectActive);
                    break;
                case ItemEffect.CharStatsToEffect.Power:
                    playerDataSet.power = CalculateStatValue(playerDataSet.power, itemEffect, isEffectActive);
                    break;
                case ItemEffect.CharStatsToEffect.Wisdom:
                    playerDataSet.wisdom = CalculateStatValue(playerDataSet.wisdom, itemEffect, isEffectActive);
                    break;
                case ItemEffect.CharStatsToEffect.Condition:
                    playerDataSet.condition = CalculateStatValue(playerDataSet.condition, itemEffect, isEffectActive);
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
