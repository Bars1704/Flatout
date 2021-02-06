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
        [SerializeField] float explosionForce;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.relativeVelocity.sqrMagnitude >= breakMinSpeed)
            {
                CrashBox(collision);
            }
        }
        /// <summary>
        /// Взрыв коробки
        /// </summary>
        /// <param name="collision"></param>
        void CrashBox(Collision collision)
        {
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Collider>());
            foreach (var boxPartcollider in transform.GetComponentsInChildren<Collider>(true))
            {
                boxPartcollider.enabled = true;
            }
                foreach (var boxPart in transform.GetComponentsInChildren<Rigidbody>())
            {
                boxPart.mass = 0.3f;
                boxPart.isKinematic = false;
                boxPart.constraints = RigidbodyConstraints.None;
                boxPart.AddExplosionForce(explosionForce, transform.position,10);
            }
            CarBase car;
            if (collision.gameObject.TryGetComponent(out car))
                car.BoxCrashed();
        }
    }
}
