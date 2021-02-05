using Doozy.Engine.Soundy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
    public class CarControlBase : MonoBehaviour
    {
        protected CarTier controlCarTier;
        protected CarBase carBase;

        /// <summary>
        ///  <see cref="Rigidbody"/> компонент управляемой машинки
        /// </summary>
        protected Rigidbody carRigidbody;

        /// <summary>
        /// Ускорение машинки
        /// </summary>
        public void Boost()
        {
            float availableBooster = carBase.TakeBooster(controlCarTier.BoosterTake) / controlCarTier.BoosterTake;
            float boostForce = availableBooster * controlCarTier.BoosterForce;
            carRigidbody.AddForce(transform.forward * boostForce);
        }
        /// <summary>
        /// Инициализация компонента
        /// </summary>
        /// <param name="carTier">Конфиг, хранящий в себе параметры машинки</param>
        /// <param name="carBase">Обьект машинки</param>
        public virtual void Init(CarTier carTier, CarBase carBase)
        {
            controlCarTier = carTier;
            this.carBase = carBase;
            carRigidbody = GetComponent<Rigidbody>();
        }
        /// <summary>
        /// Двигает машинку вперед
        /// </summary>
        /// <param name="speed">значение от 0 до 1, характеризует скорость машинки</param>
        public void Run(float speed)
        {
            var direction = transform.forward;
            direction.y = 0;
            carRigidbody.AddForce(controlCarTier.MovingSpeed * direction * speed);
            SoundyManager.Play("Flatout", "CarMove", transform);
        }
        /// <summary>
        /// Поворачивает машинку
        /// </summary>
        /// <param name="direction">Направление поворота</param>
        public void Rotate(Vector3 direction)
        {
            carRigidbody.rotation =
    Quaternion.Lerp(carRigidbody.rotation, Quaternion.LookRotation(direction),
        controlCarTier.RotationSpeed);
        }
        /// <summary>
        /// Ускорение-рывок
        /// </summary>
        public void DashBoost()
        {
            for (int i = 0; i < controlCarTier.BoostDashMultiplier; i++)
                Boost();
        }
    }

}
