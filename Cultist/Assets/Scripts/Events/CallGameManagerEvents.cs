using System;

namespace Events
{
    public class CallGameManagerEvents
    {
        public static event Action<bool> OnGamePause;

        public static void StopGame(bool setStop)
        {
            OnGamePause?.Invoke(setStop);
        }
    }
}