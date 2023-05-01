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

            ChangeActionMapToPlayer();

            #region Events
            PlayerInputActions.Player.OpenInventory.performed += ChangeActionMapToUIOnKey;
            
            PlayerInputActions.UI.CloseUI.performed += ChangeActionMapToPlayerOnKey;
            
            CallCollectableEvents.OnCollectableShown += ChangeActionMapToUI;
            
            CallCollectableEvents.OnCollectableClosed += ChangeActionMapToPlayer;

            DialogueController.OnDialogueShown += ChangeActionMapToUI;

            DialogueController.OnDialogueClosed += ChangeActionMapToPlayer;

            CallContainerEvents.OnContainerOpen += ChangeActionMapToUI;

            CallContainerEvents.OnContainerClosed += ChangeActionMapToPlayer;

            #endregion
        }

        private void OnDestroy()
        {
            #region Events
            PlayerInputActions.Player.OpenInventory.performed -= ChangeActionMapToUIOnKey;
            
            PlayerInputActions.UI.CloseUI.performed -= ChangeActionMapToPlayerOnKey;
            
            CallCollectableEvents.OnCollectableShown -= ChangeActionMapToUI;
            
            CallCollectableEvents.OnCollectableClosed -= ChangeActionMapToPlayer;

            DialogueController.OnDialogueShown -= ChangeActionMapToUI;
            
            DialogueController.OnDialogueClosed -= ChangeActionMapToPlayer;
            
            CallContainerEvents.OnContainerOpen -= ChangeActionMapToUI;

            CallContainerEvents.OnContainerClosed -= ChangeActionMapToPlayer;
            #endregion
        }

        #region HandleActionMap
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
            
            SwitchCameraActionMap(true);
            
        }
        private void ChangeActionMapToUI()
        {
            PlayerInputActions.Disable();
            
            PlayerInputActions.UI.Enable();
            
            SwitchCameraActionMap(false);
        }

        private void SwitchCameraActionMap(bool mode)
        {
            if (mode)
            {
                PlayerInputActions.Camera.Enable();
            }
            else
            {
                PlayerInputActions.Camera.Disable();
            }
        }
        #endregion
    }
}
