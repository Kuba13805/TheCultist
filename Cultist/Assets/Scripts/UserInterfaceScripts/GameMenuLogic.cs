using System;
using Events;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameMenuLogic : MonoBehaviour
{
    [SerializeField] private GameObject gameMenuPanel;
    
    private void Start()
    {
        InputManager.Instance.PlayerInputActions.Player.OpenGameMenu.performed += OpenGameMenu;
    }

    private void OnDisable()
    {
        InputManager.Instance.PlayerInputActions.Player.OpenGameMenu.performed -= OpenGameMenu;
        
    }

    private void CloseGameMenu(InputAction.CallbackContext obj)
    {
        Resume();
    }

    private void OpenGameMenu(InputAction.CallbackContext obj)
    {
        CallGameManagerEvents.StopGame(true);
        
        InputManager.Instance.PlayerInputActions.Player.OpenGameMenu.performed -= OpenGameMenu;
        
        InputManager.Instance.PlayerInputActions.UI.CloseUI.performed += CloseGameMenu;
        
        CallPlayerInputChange.SetAllInput(false);
        
        CallPlayerInputChange.SetUiActions(true);
        
        gameMenuPanel.SetActive(true);
    }

    public void Resume()
    {
        InputManager.Instance.PlayerInputActions.Player.OpenGameMenu.performed += OpenGameMenu;
        
        InputManager.Instance.PlayerInputActions.UI.CloseUI.performed -= CloseGameMenu;
        
        CallPlayerInputChange.SetAllInput(false);
        
        CallPlayerInputChange.SetPlayerActions(true);
        
        CallPlayerInputChange.SetCameraActions(true);
        
        CallGameManagerEvents.StopGame(false);
        
        gameMenuPanel.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        var locationEvent = new CallLocationChange();
        
        SceneManager.UnloadSceneAsync("PlayerAndUIScene");
        
        locationEvent.ChangeLocation("MainMenuScene", true);
        
        Resume();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
