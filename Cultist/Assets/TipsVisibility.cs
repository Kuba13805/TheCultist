using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

public class TipsVisibility : MonoBehaviour
{
    [SerializeField] private GameObject tipsUI;

    private bool status;

    private void Start()
    {
        InputManager.Instance.PlayerInputActions.Player.ShowHideTips.performed += ShowHideTipsPanel;
    }
    private void ShowHideTipsPanel(InputAction.CallbackContext obj)
    {
        if (status)
        {
            tipsUI.SetActive(false);
            status = false;
        }
        else
        {
            tipsUI.SetActive(true);
            status = true;
        }
    }
}
