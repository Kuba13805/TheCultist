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
            //PlayerInputActions.Camera.Enable();

            #region Events

            CallMenuEvents.OnMenuShown += ChangeActionMapToUI;

            CallMenuEvents.OnMenuClosed += ChangeActionMapToPlayer;

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
            CallMenuEvents.OnMenuShown -= ChangeActionMapToUI;

            CallMenuEvents.OnMenuClosed -= ChangeActionMapToPlayer;
            
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
                Debug.Log("Camera off");
                PlayerInputActions.Camera.Disable();
            }
            else
            {
                Debug.Log("Camera on");
                PlayerInputActions.Camera.Enable();
            }
        }
        #endregion
    }
}
