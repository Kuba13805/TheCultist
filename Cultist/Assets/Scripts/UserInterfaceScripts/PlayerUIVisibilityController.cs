using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerUIVisibilityController : MonoBehaviour
{
    [FormerlySerializedAs("UI")] [SerializeField] private GameObject ui;
    private void Start()
    {
        InputManager.Instance.PlayerInputActions.Player.OpenInventory.performed += OpenInventoryOnPerformed;
        
        InputManager.Instance.PlayerInputActions.UI.CloseUI.performed += CloseUIOnPerformed;
    }


    private void OnDestroy()
    {
        InputManager.Instance.PlayerInputActions.Player.OpenInventory.performed -= OpenInventoryOnPerformed;
        
        InputManager.Instance.PlayerInputActions.UI.CloseUI.performed -= CloseUIOnPerformed;
    }

    private void OpenInventoryOnPerformed(InputAction.CallbackContext context)
    {
        ui.SetActive(true);
    }
    private void CloseUIOnPerformed(InputAction.CallbackContext obj)
    {
        ui.SetActive(false);
    }
}
