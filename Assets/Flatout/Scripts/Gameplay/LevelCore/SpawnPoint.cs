using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
    public class SpawnPoint : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Vector3 cubePosition = transform.position;
            cubePosition.y = 0.5f;
            Gizmos.DrawCube(cubePosition, Vector3.one);
        }
    }
}
