using System;
using UnityEngine;

namespace Flatout
{
    /// <summary>
    /// Триггер-коллайдер, на коллбек которого можно подписаться извне
    /// </summary>
    public class TriggerCollider : MonoBehaviour
    {
        /// <summary>
        /// Событие вхождения в триггер
        /// </summary>
        public Action<Collider> OnTriggered;
        public void OnTriggerEnter(Collider other)
        => OnTriggered?.Invoke(other);
    }
}
