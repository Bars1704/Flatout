using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
    public class DeadZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            CarBase car = other.gameObject.GetComponentInParent<CarBase>();
            if (car != null)
                car.KillImmediately();
        }
    }
}