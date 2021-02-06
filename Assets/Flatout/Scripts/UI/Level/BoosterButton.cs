using UnityEngine;

namespace Flatout
{
    public class BoosterButton : MonoBehaviour
    {
        //TODO: вынести в CarJoystickControl?
        [HideInInspector]
        public CarJoystickControl carControl;
        public void SetBooster(bool boosterStatus) 
            => carControl.IsBoosted = boosterStatus;
        public void AddForcePuch()
        {
            carControl.DashBoost();
        }
    }
}
