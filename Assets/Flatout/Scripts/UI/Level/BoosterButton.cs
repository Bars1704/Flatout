using UnityEngine;

namespace Flatout
{
    public class BoosterButton : MonoBehaviour
    {
        //TODO: вынести в CarJoystickControl?
        [HideInInspector]
        public CarJoystickControl carControl;

        bool isBoosterEnabled;
        public void SetBooster(bool boosterStatus) 
            => isBoosterEnabled = boosterStatus;
        public void AddForcePuch()
        {
            carControl.DashBoost();
        }
        private void FixedUpdate()
        {
            if (isBoosterEnabled)
            {
                carControl.Boost();
            }
        }
    }
}
