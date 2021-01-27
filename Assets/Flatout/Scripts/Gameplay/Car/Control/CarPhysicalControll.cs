using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
    public class CarPhysicalControll : MonoBehaviour
    {
        Rigidbody rb;
        [SerializeField] WheelCollider[] moveColliders;
        [SerializeField] WheelCollider[] rotateColliders;
        public float torque = 200;
        public float brakeTorque = 200;
        public float rotationMaxAngle = 200;
        public float boostForce = 10000;
        public float rotationSpeed;
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        public void MoveTorvards(float direction)
        {
            direction = Mathf.Clamp(direction, -1, 1);
            var force = direction > 0 ? direction * torque : direction * brakeTorque;
            for (int i = 0; i < moveColliders.Length; i++)
            {
                moveColliders[i].motorTorque = force;
                moveColliders[i].transform.Rotate(0, 0, force);
            }
        }
        public void Boost()
        {
            rb.AddForce(transform.forward * boostForce);
        }
        public void Rotate(float direction)
        {
            if (direction == 0) return;
            direction = Mathf.Clamp(direction, -1, 1);
            for (int i = 0; i < rotateColliders.Length; i++)
            {
                var currentWheel = rotateColliders[i];
                if (Mathf.Abs(currentWheel.steerAngle) >= rotationMaxAngle && direction * currentWheel.steerAngle > 0) continue;
                currentWheel.steerAngle += 4 * direction;
                currentWheel.transform.Rotate(new Vector3(0, rotationSpeed, 0) * direction);
            }
        }
        public void SetWheelsRotation(float rotation)
        {
            var angle = Mathf.InverseLerp(0, rotationMaxAngle, Mathf.Abs(rotation));
            if (rotation < 0) angle *= -1;
            for (int i = 0; i < rotateColliders.Length; i++)
            {
                rotateColliders[i].steerAngle = angle;
                rotateColliders[i].transform.rotation = Quaternion.Euler(0, angle, 0);
            }

        }
        void Update()
        {
            MoveTorvards(Input.GetAxis("Vertical"));
            if (Input.GetAxis("Horizontal") != 0)
            {
                Rotate(Input.GetAxis("Horizontal"));
            }
            if (Input.GetKey(KeyCode.Space))
            {
                Boost();
            }
        }
    }

}
