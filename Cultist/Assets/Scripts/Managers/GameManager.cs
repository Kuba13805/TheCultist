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

        public static event Action OnPlayerHealed;

        public static event Action OnPlayerDamaged;

        public static event Action<string> OnPlayerNicknameChanged; 

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
            
            PlayerEvents.OnTestPlayerStat += TestPlayerAbilities;

            PlayerEvents.OnChangePlayerNickname += ChangePlayerNickname;

            PlayerEvents.OnHealPlayer += HealPlayer;

            PlayerEvents.OnDamagePlayer += DamagePlayer;

            PlayerEvents.OnEndGame += EndGame;

            PlayerEvents.OnAddMoneyToPlayer += AddMoneyToPlayer;

            PlayerEvents.OnRemoveMoneyFromPlayer += RemoveMoneyFromPlayer;

            PlayerEvents.OnCheckForItem += CheckForItemInInventory;
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
        

        private void TestPlayerAbilities(Stat abilityToCheck, int baseTestDifficulty)
        {
            var basePlayerNumber = 0;
            
            switch (abilityToCheck)
            {
                case Stat.Strength:
                    basePlayerNumber = playerData.strength.statValue;
                    break;
                case Stat.Dexterity:
                    basePlayerNumber = playerData.dexterity.statValue;
                    break;
                case Stat.Power:
                    basePlayerNumber = playerData.power.statValue;
                    break;
                case Stat.Wisdom:
                    basePlayerNumber = playerData.wisdom.statValue;
                    break;
                case Stat.Condition:
                    basePlayerNumber = playerData.condition.statValue;
                    break;
                case Stat.Perception:
                    basePlayerNumber = playerData.perception.statValue;
                    break;
                case Stat.Occultism:
                    basePlayerNumber = playerData.occultism.statValue;
                    break;
                case Stat.Medicine:
                    basePlayerNumber = playerData.medicine.statValue;
                    break;
                case Stat.Electrics:
                    basePlayerNumber = playerData.electrics.statValue;
                    break;
                case Stat.History:
                    basePlayerNumber = playerData.history.statValue;
                    break;
                case Stat.Persuasion:
                    basePlayerNumber = playerData.persuasion.statValue;
                    break;
                case Stat.Intimidation:
                    basePlayerNumber = playerData.intimidation.statValue;
                    break;
                case Stat.Locksmithing:
                    basePlayerNumber = playerData.locksmithing.statValue;
                    break;
                case Stat.Mechanics:
                    basePlayerNumber = playerData.mechanics.statValue;
                    break;
                case Stat.Acrobatics:
                    basePlayerNumber = playerData.acrobatics.statValue;
                    break;
                case Stat.Forensics:
                    basePlayerNumber = playerData.forensics.statValue;
                    break;
                case Stat.Acting:
                    basePlayerNumber = playerData.acting.statValue;
                    break;
                case Stat.Alchemy:
                    basePlayerNumber = playerData.alchemy.statValue;
                    break;
                case Stat.Astrology:
                    basePlayerNumber = playerData.astrology.statValue;
                    break;
                case Stat.Thievery:
                    basePlayerNumber = playerData.thievery.statValue;
                    break;
                case Stat.RangedCombat:
                    basePlayerNumber = playerData.rangedCombat.statValue;
                    break;
                case Stat.HandToHandCombat:
                    basePlayerNumber = playerData.handToHandCombat.statValue;
                    break;
                case Stat.Etiquette:
                    basePlayerNumber = playerData.etiquette.statValue;
                    break;
                case Stat.Animism:
                    basePlayerNumber = playerData.animism.statValue;
                    break;
                case Stat.Empathy:
                    basePlayerNumber = playerData.empathy.statValue;
                    break;
                case Stat.Demonology:
                    basePlayerNumber = playerData.demonology.statValue;
                    break;
                case Stat.Stealth:
                    basePlayerNumber = playerData.stealth.statValue;
                    break;
                case Stat.Necromancy:
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
                case Stat.Strength:
                    playerDataSet.strength.statValue = CalculateStatValue(playerDataSet.strength.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Dexterity:
                    playerDataSet.dexterity.statValue = CalculateStatValue(playerDataSet.dexterity.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Power:
                    playerDataSet.power.statValue = CalculateStatValue(playerDataSet.power.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Wisdom:
                    playerDataSet.wisdom.statValue = CalculateStatValue(playerDataSet.wisdom.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Condition:
                    playerDataSet.condition.statValue = CalculateStatValue(playerDataSet.condition.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Perception:
                    playerDataSet.perception.statValue = CalculateStatValue(playerDataSet.perception.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Occultism:
                    playerDataSet.occultism.statValue = CalculateStatValue(playerDataSet.occultism.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Medicine:
                    playerDataSet.medicine.statValue = CalculateStatValue(playerDataSet.medicine.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Electrics:
                    playerDataSet.electrics.statValue = CalculateStatValue(playerDataSet.electrics.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.History:
                    playerDataSet.history.statValue = CalculateStatValue(playerDataSet.history.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Persuasion:
                    playerDataSet.persuasion.statValue = CalculateStatValue(playerDataSet.persuasion.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Intimidation:
                    playerDataSet.intimidation.statValue = CalculateStatValue(playerDataSet.intimidation.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Locksmithing:
                    playerDataSet.locksmithing.statValue = CalculateStatValue(playerDataSet.locksmithing.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Mechanics:
                    playerDataSet.mechanics.statValue = CalculateStatValue(playerDataSet.mechanics.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Acrobatics:
                    playerDataSet.acrobatics.statValue = CalculateStatValue(playerDataSet.acrobatics.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Forensics:
                    playerDataSet.forensics.statValue = CalculateStatValue(playerDataSet.forensics.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Acting:
                    playerDataSet.acting.statValue = CalculateStatValue(playerDataSet.acting.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Alchemy:
                    playerDataSet.alchemy.statValue = CalculateStatValue(playerDataSet.alchemy.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Astrology:
                    playerDataSet.astrology.statValue = CalculateStatValue(playerDataSet.astrology.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Thievery:
                    playerDataSet.thievery.statValue = CalculateStatValue(playerDataSet.thievery.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.RangedCombat:
                    playerDataSet.rangedCombat.statValue = CalculateStatValue(playerDataSet.rangedCombat.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.HandToHandCombat:
                    playerDataSet.handToHandCombat.statValue = CalculateStatValue(playerDataSet.handToHandCombat.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Etiquette:
                    playerDataSet.etiquette.statValue = CalculateStatValue(playerDataSet.etiquette.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Animism:
                    playerDataSet.animism.statValue = CalculateStatValue(playerDataSet.animism.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Empathy:
                    playerDataSet.empathy.statValue = CalculateStatValue(playerDataSet.empathy.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Demonology:
                    playerDataSet.demonology.statValue = CalculateStatValue(playerDataSet.demonology.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Stealth:
                    playerDataSet.stealth.statValue = CalculateStatValue(playerDataSet.stealth.statValue, itemEffect, isEffectActive);
                    break;
                case Stat.Necromancy:
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

        #region HandlePlayerAlterations

        private void ChangePlayerNickname(string newNickname)
        {
            playerData.nickname = newNickname;
            
            OnPlayerNicknameChanged?.Invoke(newNickname);
        }
        
        private void DamagePlayer(int points)
        {
            playerData.health -= points;
            
            OnPlayerDamaged?.Invoke();
        }

        private void HealPlayer(int points)
        {
            playerData.health += points;
            
            OnPlayerHealed?.Invoke();
        }

        private void AddMoneyToPlayer(float quantity)
        {
            
        }

        private void RemoveMoneyFromPlayer(float quantity)
        {
            
        }

        private void EndGame()
        {
            
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

public enum Stat
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
