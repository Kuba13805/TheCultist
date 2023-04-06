using UnityEngine;
using System;
using PlayerScripts;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public PlayerInputActions PlayerInputActions;
        public static InputManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            PlayerInputActions = new PlayerInputActions();
        }

        public void ChangeActionMapToPlayer()
        {
            PlayerInputActions.Disable();
            PlayerInputActions.Player.Enable();
        }

        public void ChangeActionMapToUI()
        {
            PlayerInputActions.Disable();
            PlayerInputActions.UI.Enable();
        }
    }
}
