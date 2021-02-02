using InControl;
using UnityEngine;

namespace Flatout
{
    /// <summary>
    /// Управление машинкой
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class CarManualControl : MonoBehaviour
    {
        private CarTier controlCarTier;
        /// <summary>
        ///  <see cref="Rigidbody"/> компонент управляемой машинки
        /// </summary>
        private Rigidbody carRigidbody;
        /// <summary>
        /// Джойстик управления
        /// </summary>
        private TouchStickControl runStick;

        private CarBase car;
        /// <summary>
        /// Движется ли машинка в данный момент
        /// </summary>
        public bool IsMoving { get; private set; }

        private CarBase carBase;

        /// <summary>
        /// Инициализация компонента
        /// </summary>
        /// <param name="carTier">Конфиг, хранящий в себе параметры машинки</param>
        public void Init(CarTier carTier, CarBase carBase)
        {
            controlCarTier = carTier;
            this.carBase = carBase;
        }

        private void Awake()
        {
            carRigidbody = GetComponent<Rigidbody>();
            runStick = FindObjectOfType<TouchStickControl>();
        }
        /// <summary>
        /// Движение 
        /// </summary>
        private void Run()
        {
            if (!runStick.IsActive)
            {
                ResetVelocity();
                return;
            }

            Vector3 runDirection = new Vector3(runStick.Value.x, 0, runStick.Value.y);

            if (runDirection.magnitude <= runStick.lowerDeadZone)
            {
                ResetVelocity();
                return;
            }
            //var carVelocity = controlCarTier.MovingSpeed * runDirection;
            //carVelocity.y = carRigidbody.velocity.y;
            //carRigidbody.velocity = carVelocity;
            carRigidbody.AddForce(controlCarTier.MovingSpeed * transform.forward);
            carRigidbody.rotation =
                Quaternion.Lerp(carRigidbody.rotation, Quaternion.LookRotation(runDirection),
                    controlCarTier.RotationSpeed);
            IsMoving = true;
        }

        private void FixedUpdate()
        {
            Run();
        }
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Boost();
            }
        }
#endif
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
        /// "Откат" скорости машинки
        /// </summary>
        private void ResetVelocity()
        {
            IsMoving = false;
        }
    }
}


