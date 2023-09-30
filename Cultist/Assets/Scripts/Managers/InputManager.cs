using UnityEngine;
using System;
using Events;
using PlayerScripts;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public  Texture2D interactableCursor;
        
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

            CallMenuEvents.OnMenuShown += ChangeActionMapToUI;

            CallMenuEvents.OnMenuClosed += ChangeActionMapToPlayer;

            CallCollectableEvents.OnCollectableShown += ChangeActionMapToUI;
            
            CallCollectableEvents.OnCollectableClosed += ChangeActionMapToPlayer;

            DialogueController.OnDialogueShown += ChangeActionMapToUI;

            DialogueController.OnDialogueClosed += ChangeActionMapToPlayer;

            CallContainerEvents.OnContainerOpen += ChangeActionMapToUI;

            CallContainerEvents.OnContainerClosed += ChangeActionMapToPlayer;

            CallPlayerInputChange.OnSetAllInput += SetAllActionMaps;

            CallPlayerInputChange.OnSetPlayerInput += SetPlayerActions;

            CallPlayerInputChange.OnSetCameraInput += SetCameraActions;
            
            CallPlayerInputChange.OnSetUiInput += SetUiCameraActions;

            #endregion
        }

        private void SetCameraActions(bool setActive)
        {
            if (setActive)
            {
                PlayerInputActions.Camera.Enable();
            }
            else
            {
                PlayerInputActions.Camera.Disable();
            }
        }

        private void SetPlayerActions(bool setActive)
        {
            if (setActive)
            {
                PlayerInputActions.Player.Enable();
            }
            else
            {
                PlayerInputActions.Player.Disable();
            }
        }
        
        private void SetUiCameraActions(bool setActive)
        {
            if (setActive)
            {
                PlayerInputActions.UI.Enable();
            }
            else
            {
                PlayerInputActions.UI.Disable();
            }
        }

        private void SetAllActionMaps(bool setActive)
        {
            if (setActive)
            {
                PlayerInputActions.Enable();
            }
            else
            {
                PlayerInputActions.Disable();
            }
        }

        private void OnDestroy()
        {
            #region Events
            CallMenuEvents.OnMenuShown -= ChangeActionMapToUI;

            CallMenuEvents.OnMenuClosed -= ChangeActionMapToPlayer;
            
            CallCollectableEvents.OnCollectableShown -= ChangeActionMapToUI;
            
            CallCollectableEvents.OnCollectableClosed -= ChangeActionMapToPlayer;

            DialogueController.OnDialogueShown -= ChangeActionMapToUI;
            
            DialogueController.OnDialogueClosed -= ChangeActionMapToPlayer;
            
            CallContainerEvents.OnContainerOpen -= ChangeActionMapToUI;

            CallContainerEvents.OnContainerClosed -= ChangeActionMapToPlayer;
            
            CallPlayerInputChange.OnSetAllInput -= SetAllActionMaps;

            CallPlayerInputChange.OnSetPlayerInput -= SetPlayerActions;

            CallPlayerInputChange.OnSetCameraInput -= SetCameraActions;

            CallPlayerInputChange.OnSetUiInput -= SetUiCameraActions;

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
            SwitchCameraActionMap(true);
            
            PlayerInputActions.Player.Enable();
            
            
        }
        private void ChangeActionMapToUI()
        {
            SwitchCameraActionMap(false);
            PlayerInputActions.Disable();
            
            PlayerInputActions.UI.Enable();
            
        }

        private void SwitchCameraActionMap(bool mode)
        {
            if (mode != true)
            {
                PlayerInputActions.Camera.Disable();
            }
            else
            {
                PlayerInputActions.Camera.Enable();
            }
        }
        #endregion
    }
}
