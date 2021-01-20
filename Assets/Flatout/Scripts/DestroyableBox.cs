using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBox : MonoBehaviour
{
    [SerializeField] float breakMinSpeed;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.sqrMagnitude >= breakMinSpeed)
        {
            Destroy(gameObject);
        }
    }
}
