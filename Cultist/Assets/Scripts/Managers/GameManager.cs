using System;
using PlayerScripts;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public PlayerData PlayerData;
        public static GameManager Instance { get; private set; }
    

        public GameState State;

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

        public void UpdateGameState(GameState newState)
        {
            State = newState;

            switch (newState)
            {
                case GameState.FreeMovement:
                    break;
                case GameState.Dialogue:
                    break;
                case GameState.Narrative:
                    break;
                case GameState.MenuOpened:
                    break;;
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

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
        }
    }
}
