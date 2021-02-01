using UnityEngine;

namespace Flatout
{
    /// <summary>
    /// Коробка, рушащаяся при столкновении с заданной скоростью
    /// </summary>
    public class DestroyableBox : MonoBehaviour
    {
        /// <summary>
        /// Минимальная скорость с которой нужно втараниться в ящик, чтобы он сломался
        /// </summary>
        [SerializeField] float breakMinSpeed;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.relativeVelocity.sqrMagnitude >= breakMinSpeed)
            {
                GetComponent<Animator>().SetTrigger("OnDestroy");
                GetComponent<Collider>().enabled = false;
                CarBase car;
                if(collision.gameObject.TryGetComponent<CarBase>(out car))
                {
                    car.BoxCrashed();
                }
            }
        }
    }
}
