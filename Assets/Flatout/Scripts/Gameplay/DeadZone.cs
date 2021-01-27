using UnityEngine;

namespace Flatout
{
    /// <summary>
    /// Зона, при попадании в которую игрок умирает
    /// </summary>
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