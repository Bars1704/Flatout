using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
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
}
