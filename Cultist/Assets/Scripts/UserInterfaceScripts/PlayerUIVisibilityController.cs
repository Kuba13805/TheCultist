using System;
using System.Collections;
using System.Collections.Generic;
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
        OpenPlayerUICanvas();
        
        playerJournal.SetActive(true);
    }

    private void OpenInventoryOnPerformed(InputAction.CallbackContext context)
    {
        OpenPlayerUICanvas();
        
        playerInventory.SetActive(true);
    }
    private void CloseUIOnPerformed(InputAction.CallbackContext obj)
    {
        for (var i = 0; i < playerUICanvas.transform.childCount; i++)
        {
            var uiElement = playerUICanvas.transform.GetChild(i);
            uiElement.gameObject.SetActive(false);
        }
        
        ClosePlayerUICanvas();
    }

    private void ClosePlayerUICanvas()
    {
        playerUICanvas.SetActive(false);
    }

    private void OpenPlayerUICanvas()
    {
        playerUICanvas.SetActive(true);
    }
}
