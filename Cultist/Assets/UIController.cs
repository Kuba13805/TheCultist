using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    private GameObject CanvasUI;
    private PlayerInputSystem InputSystem;
    public PlayerInput Input;

    private void Start()
    {
        InputSystem = new PlayerInputSystem();
    }

    public void OpenUI()
    {
        InputSystem.UI.Enable();
        Input.SwitchCurrentActionMap("UI");
        InputSystem.Player.Disable();
    }

    public void CloseUI()
    {
        InputSystem.Player.Enable();
        Input.SwitchCurrentActionMap("Player");
        InputSystem.UI.Disable();
    }
}
