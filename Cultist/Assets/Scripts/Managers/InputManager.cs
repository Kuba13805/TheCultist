using UnityEngine;
using System;
using PlayerScripts;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public PlayerInputActions PlayerInputActions;
        public static InputManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            PlayerInputActions = new PlayerInputActions();
            
            PlayerInputActions.Player.Enable();

            PlayerInputActions.Player.OpenInventory.performed += ChangeActionMapToUI;
            PlayerInputActions.UI.CloseUI.performed += ChangeActionMapToPlayer;
        }

        private void ChangeActionMapToPlayer(InputAction.CallbackContext context)
        {
            PlayerInputActions.Disable();
            PlayerInputActions.Player.Enable();
        }

        private void ChangeActionMapToUI(InputAction.CallbackContext context)
        {
            PlayerInputActions.Disable();
            PlayerInputActions.UI.Enable();
        }
    }
}
