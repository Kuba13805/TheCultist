using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIController : MonoBehaviour
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
        UI.SetActive(true);
    }
    private void CloseUIOnperformed(InputAction.CallbackContext obj)
    {
        UI.SetActive(false);
    }
}
