using System;
using UnityEngine;

namespace Flatout
{
    public class TriggerCollider : MonoBehaviour
    {
        public Action<Collider> OnTriggered;
        public void OnTriggerEnter(Collider other)
        => OnTriggered?.Invoke(other);
    }
}
