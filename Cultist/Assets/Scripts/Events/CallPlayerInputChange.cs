using System;

namespace Events
{
    public class CallPlayerInputChange
    {
        public static event Action<bool> OnSetAllInput;
        
        public static event Action<bool> OnSetPlayerInput;
        
        public static event Action<bool> OnSetCameraInput;
        
        public static event Action<bool> OnSetUiInput;
        
        public static void SetAllInput(bool setActive)
        {
            OnSetAllInput?.Invoke(setActive);
        }

        public static void SetPlayerActions(bool setPlayerActionsActive)
        {
            OnSetPlayerInput?.Invoke(setPlayerActionsActive);
        }
        
        public static void SetCameraActions(bool setCameraActionsActive)
        {
            OnSetCameraInput?.Invoke(setCameraActionsActive);
        }
        
        public static void SetUiActions(bool setUiActionsActive)
        {
            OnSetUiInput?.Invoke(setUiActionsActive);
        }
    }
}