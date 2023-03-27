using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerData PlayerData;
    public InventoryManager InventoryManager;
    public static GameManager Instance { get; private set; }
    

    public GameState State;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
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
}
