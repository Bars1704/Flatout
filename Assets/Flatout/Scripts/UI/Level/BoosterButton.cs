using UnityEngine;

namespace Flatout
{
    public class BoosterButton : MonoBehaviour
    {
        public CarManualControl carControl;

        bool isBoosterEnabled;
        public void SetBooster(bool boosterStatus) 
            => isBoosterEnabled = boosterStatus;
        public void AddForcePuch()
        {
            carControl.Boost();
            carControl.Boost();
            carControl.Boost();
            carControl.Boost();
            carControl.Boost();
            carControl.Boost();
            carControl.Boost();
            carControl.Boost();
            carControl.Boost();
            carControl.Boost();
            carControl.Boost();
            carControl.Boost(); 
            carControl.Boost();
            carControl.Boost();
            carControl.Boost();
            carControl.Boost();
            carControl.Boost();
            carControl.Boost();
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
