using System;

namespace Events
{
    public class CallLocationChange
    {
        #region Events

        public static event Action<string, bool> OnChangeLocation;
        
        public static event Action<string, int> OnChangeLocationOnTravelPoint;

        #endregion

        public void ChangeLocation(string newLocationName, bool setActive)
        {
            OnChangeLocation?.Invoke(newLocationName, setActive);
        }

        public void ChangeLocation(string newLocation, int travelPointId)
        {
            OnChangeLocationOnTravelPoint?.Invoke(newLocation, travelPointId);
        }
    }
}