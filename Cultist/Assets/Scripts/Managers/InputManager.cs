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

            PlayerInputActions.Player.OpenInventory.performed += ChangeActionMapToUIOnKey;
            
            PlayerInputActions.UI.CloseUI.performed += ChangeActionMapToPlayerOnKey;
            
            CollectableObject.OnCollectableShown += ChangeActionMapToUI;
            
            CollectableObject.OnCollectableClosed += ChangeActionMapToPlayer;
        }

        private void OnDestroy()
        {
            PlayerInputActions.Player.OpenInventory.performed -= ChangeActionMapToUIOnKey;
            
            PlayerInputActions.UI.CloseUI.performed -= ChangeActionMapToPlayerOnKey;
            
            CollectableObject.OnCollectableShown -= ChangeActionMapToUI;
            
            CollectableObject.OnCollectableClosed -= ChangeActionMapToPlayer;
        }

        private void ChangeActionMapToPlayerOnKey(InputAction.CallbackContext context)
        {
            ChangeActionMapToPlayer();
        }

        private void ChangeActionMapToUIOnKey(InputAction.CallbackContext context)
        {
            ChangeActionMapToUI();
        }

        private void ChangeActionMapToPlayer()
        {
            PlayerInputActions.Disable();
            
            PlayerInputActions.Player.Enable();
        }
        private void ChangeActionMapToUI()
        {
            PlayerInputActions.Disable();
            
            PlayerInputActions.UI.Enable();
        }
    }
}
