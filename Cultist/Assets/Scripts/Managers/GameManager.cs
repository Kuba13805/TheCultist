using System;
using PlayerScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Events

        public static event Action<bool> OnPlayerTestCheck;

        public static event Action<GameState> OnGameStateChanged;

        #endregion
        [FormerlySerializedAs("PlayerData")] public PlayerData playerData;
        public static GameManager Instance { get; private set; }
    

        [FormerlySerializedAs("State")] public GameState state;

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
            DialogueController.OnTestCheck += TestPlayerAbilities;
            
            CallContainerEvents.OnContainerOpen += PauseGame;
        
            CallContainerEvents.OnContainerClosed += ResumeGame;
        
            CallCollectableEvents.OnCollectableShown += PauseGame;
        
            CallCollectableEvents.OnCollectableClosed += ResumeGame;

            DialogueController.OnDialogueShown += ChangeGameStateToDialogue;
            
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
    }
}
