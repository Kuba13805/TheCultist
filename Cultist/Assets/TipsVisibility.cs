using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

public class TipsVisibility : MonoBehaviour
{
    [SerializeField] private GameObject tipsUI;

    private void Start()
    {
        InputManager.Instance.PlayerInputActions.Player.ShowHideTips.performed += ShowHideTipsPanel;
    }

    private void ShowHideTipsPanel(InputAction.CallbackContext obj)
    {
        tipsUI.SetActive(!tipsUI.activeSelf);
        
    }
}
