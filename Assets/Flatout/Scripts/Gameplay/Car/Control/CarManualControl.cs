﻿using InControl;
using UnityEngine;

namespace Flatout
{
    /// <summary>
    /// Управление машинкой
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class CarManualControl : MonoBehaviour
    {
        /// <summary>
        /// Скорость поворота
        /// </summary>
        [SerializeField, Tooltip("Скорость поворота")] private float rotateSpeed;
        /// <summary>
        /// Сила ускорения
        /// </summary>
        [SerializeField, Tooltip("Сила ускорения")] private float boostForce = 100;
        /// <summary>
        /// Скорость движения
        /// </summary>
        [SerializeField, Tooltip("Скорость движения")] private float MoveSpeed;
        /// <summary>
        ///  <see cref="Rigidbody"/> компонент управляемой машинки
        /// </summary>
        private Rigidbody carRigidbody;
        /// <summary>
        /// Джойстик управления
        /// </summary>
        private TouchStickControl runStick;

        /// <summary>
        /// Движется ли машинка в данный момент
        /// </summary>
        public bool IsMoving { get; private set; }

        /// <summary>
        /// Инициализация компонента
        /// </summary>
        /// <param name="car">Конфиг, хранящий в себе параметры машинки</param>
        public void Init(CarTier car)
        {
            rotateSpeed = car.RotationSpeed;
            boostForce = car.BoosterForce;
            MoveSpeed = car.MovingSpeed;
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

            carRigidbody.velocity = MoveSpeed * runDirection;
            carRigidbody.rotation =
                Quaternion.Lerp(carRigidbody.rotation, Quaternion.LookRotation(runDirection),
                    rotateSpeed);
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

