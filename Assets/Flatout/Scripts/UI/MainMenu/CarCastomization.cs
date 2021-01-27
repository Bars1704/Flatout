using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
    /// <summary>
    /// UI-компонент кастомизации машинки
    /// </summary>
    public class CarCastomization : MonoBehaviour
    {
        void Start()
        {
            var actualCar = PlayerAvatar.Instance.ActualCar;
            SpawnCar(actualCar.CarPrefab);
        }
        /// <summary>
        /// Спавнит на сцене "пустышку" машинки
        /// </summary>
        /// <param name="carPrefab">префаб машинки</param>
        void SpawnCar(GameObject carPrefab)
        {
            var carInstance = Instantiate(carPrefab, transform);
            carInstance.GetComponentInChildren<Animator>().enabled = false;
            carInstance.GetComponentInChildren<Rigidbody>().useGravity = false;
        }
    }
}