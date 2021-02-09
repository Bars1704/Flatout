using Doozy.Engine.UI;
using UnityEngine;

namespace Flatout
{
    public class BoosterButton : MonoBehaviour
    {
        private void Start()
        {
            carControl = PlayerCar.Instance.transform.GetComponent<CarControlBase>();
        }
        private CarControlBase carControl;
        public void SetBooster(bool boosterStatus) 
            => carControl.IsBoosted = boosterStatus;
        public void AddForcePuch()
        {
            UIButton uI;
            carControl.DashBoost();
        }
    }
}
