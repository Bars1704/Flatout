using System;
using UnityEngine;

public class TriggerCollider : MonoBehaviour
{
    public Action<Collider> OnTriggered;
    public void OnTriggerEnter(Collider other)
    => OnTriggered?.Invoke(other);
}
