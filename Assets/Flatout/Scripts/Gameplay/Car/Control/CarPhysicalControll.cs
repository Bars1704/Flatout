using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
    /// <summary>
    /// Модуль управления машинки, основанный на физическом взаимодействии - машинка едет за счет вращающихся колес
    /// </summary>
    public class CarPhysicalControll : MonoBehaviour
    {
        /// <summary>
        /// <see cref="Rigidbody"/> компонент машинки
        /// </summary>
        Rigidbody rb;
        /// <summary>
        /// Коллайдеры колес, отвечающие за движение
        /// </summary>
        [SerializeField] WheelCollider[] moveColliders;
        /// <summary>
        /// Коллайдеры колес, которые осуществляют поворот
        /// </summary>
        [SerializeField] WheelCollider[] rotateColliders;
        /// <summary>
        /// Скорость вращения колес
        /// </summary>
        public float torque = 200;
        /// <summary>
        /// Скорость торможения
        /// </summary>
        public float brakeTorque = 200;
        /// <summary>
        /// Максимальный угол поворота колес
        /// </summary>
        public float rotationMaxAngle = 200;
        /// <summary>
        /// Сила ускорения
        /// </summary>
        public float boostForce = 10000;
        /// <summary>
        /// Скорость поворота
        /// </summary>
        public float rotationSpeed;
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        /// <summary>
        /// Движение вперед-назад
        /// </summary>
        /// <param name="direction">Направление движения (1 - вперед, -1 - назад)</param>
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

        /// <summary>
        /// Ускорение машинки
        /// </summary>
        public void Boost()
        {
            rb.AddForce(transform.forward * boostForce);
        }
        /// <summary>
        /// Поворот машинки за счет поворота отдельных колес (<see cref="rotateColliders"/>)
        /// </summary>
        /// <param name="direction">Направление поворота (1 - вправо, -1 - влево)</param>
        public void Rotate(float direction)
        {
            if (direction == 0) return;
            direction = Mathf.Clamp(direction, -1, 1);
            for (int i = 0; i < rotateColliders.Length; i++)
            {
                var currentWheel = rotateColliders[i];
                if (Mathf.Abs(currentWheel.steerAngle) >= rotationMaxAngle && direction * currentWheel.steerAngle > 0) continue;
                currentWheel.steerAngle += rotationSpeed * direction;
                currentWheel.transform.Rotate(new Vector3(0, rotationSpeed, 0) * direction);
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
