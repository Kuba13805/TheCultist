using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerData PlayerData;
    public static GameManager Instance { get; private set; }
    

    public GameState State;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
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
}
