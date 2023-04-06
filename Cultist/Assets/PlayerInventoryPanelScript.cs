using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventoryPanelScript : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    private void Start()
    {
        InputManager.Instance.PlayerInputActions.Player.OpenInventory.performed += OpenInventoryOnperformed;
        InputManager.Instance.PlayerInputActions.UI.CloseUI.performed += CloseUIOnperformed;
    }


    private void OnDestroy()
    {
        InputManager.Instance.PlayerInputActions.Player.OpenInventory.performed -= OpenInventoryOnperformed;
        InputManager.Instance.PlayerInputActions.UI.CloseUI.performed -= CloseUIOnperformed;
    }

    private void OpenInventoryOnperformed(InputAction.CallbackContext context)
    {
        InputManager.Instance.PlayerInputActions.Disable();
        InputManager.Instance.PlayerInputActions.UI.Enable(); 
        
        UI.SetActive(true);
    }
    private void CloseUIOnperformed(InputAction.CallbackContext obj)
    {
        InputManager.Instance.PlayerInputActions.Disable();
        InputManager.Instance.PlayerInputActions.Player.Enable();
        
        UI.SetActive(false);
    }
}
