using Doozy.Engine.UI;
using UnityEngine;

namespace Flatout
{
    /// <summary>
    /// Кнопка, отвечающая за ускорение машинки игрока
    /// </summary>
    public class BoosterButton : MonoBehaviour
    {

        CarControlBase _carControl;
        private CarControlBase carControl
        {
            get
            {
                if (_carControl == null)
                    _carControl = PlayerCar.Instance.transform.GetComponent<CarControlBase>();
                return _carControl;
            }
            set => _carControl = value;
        }
        public void SetBooster(bool boosterStatus)
            => carControl.IsBoosted = boosterStatus;
        public void AddForcePuch()
        {
            carControl.DashBoost();
        }
    }
}
