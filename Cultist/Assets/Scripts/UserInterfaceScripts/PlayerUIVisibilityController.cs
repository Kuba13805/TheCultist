using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerUIVisibilityController : MonoBehaviour
{
    [SerializeField] private GameObject playerUICanvas;

    [SerializeField] private GameObject playerInventory;
    
    [SerializeField] private GameObject playerJournal;
    private void Start()
    {
        InputManager.Instance.PlayerInputActions.Player.OpenInventory.performed += OpenInventoryOnPerformed;
        
        InputManager.Instance.PlayerInputActions.UI.CloseUI.performed += CloseUIOnPerformed;

        InputManager.Instance.PlayerInputActions.Player.OpenJournal.performed += OpenJournalOnPerformed;
    }
    
    private void OnDestroy()
    {
        InputManager.Instance.PlayerInputActions.Player.OpenInventory.performed -= OpenInventoryOnPerformed;
        
        InputManager.Instance.PlayerInputActions.UI.CloseUI.performed -= CloseUIOnPerformed;
        
        InputManager.Instance.PlayerInputActions.Player.OpenJournal.performed -= OpenJournalOnPerformed;
    }
    private void OpenJournalOnPerformed(InputAction.CallbackContext obj)
    {
        playerJournal.SetActive(true);
        
        CallPlayerInputChange.SetAllInput(false);
        
        CallPlayerInputChange.SetUiActions(true);
    }

    private void OpenInventoryOnPerformed(InputAction.CallbackContext context)
    {
        playerInventory.SetActive(true);
        
        CallPlayerInputChange.SetAllInput(false);
        
        CallPlayerInputChange.SetUiActions(true);
    }
    private void CloseUIOnPerformed(InputAction.CallbackContext obj)
    {
        for (var i = 0; i < playerUICanvas.transform.childCount; i++)
        {
            var uiElement = playerUICanvas.transform.GetChild(i);
            uiElement.gameObject.SetActive(false);
        }
        
        CallPlayerInputChange.SetAllInput(false);
        
        CallPlayerInputChange.SetPlayerActions(true);
        
        CallPlayerInputChange.SetCameraActions(true);
    }
}
