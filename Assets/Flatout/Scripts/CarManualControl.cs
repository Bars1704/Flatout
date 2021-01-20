using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarManualControl : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float boostForce = 100;
        [SerializeField] private float MoveSpeed;
        private Rigidbody carRigidbody;
        private TouchStickControl runStick;
        private TouchButtonControl button;

        public bool IsMoving { get; private set; }

        public void Init(CarTier car) {
            rotateSpeed = car.RotationSpeed;
            boostForce = car.BoosterForce;
            MoveSpeed = car.MovingSpeed;
        }

        private void Awake()
        {
            carRigidbody = GetComponent<Rigidbody>();
            runStick = FindObjectOfType<TouchStickControl>();
            button = FindObjectOfType<TouchButtonControl>();
        }
        private void Run()
        {
            if (!runStick.IsActive)
            {
                ResetVelocity();
                return;
            }

            Vector3 runDirection = new Vector3(runStick.Value.x, 0, runStick.Value.y);
            if (runDirection.magnitude > runStick.lowerDeadZone)
            {
                carRigidbody.velocity = MoveSpeed * runDirection;
                carRigidbody.rotation =
                    Quaternion.Lerp(carRigidbody.rotation, Quaternion.LookRotation(runDirection),
                        rotateSpeed);

                IsMoving = true;
            }
            else
            {
                ResetVelocity();
            }

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


        public void Boost()
        {
            carRigidbody.AddForce(transform.forward * boostForce);
        }

        private void ResetVelocity()
        {
            IsMoving = false;
        }
    }
}


