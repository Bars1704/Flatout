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
                Destroy(gameObject);
            }
        }
    }
}
