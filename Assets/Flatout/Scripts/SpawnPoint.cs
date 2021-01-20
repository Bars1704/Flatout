using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 cubePosition = transform.position;
        cubePosition.y = 0.5f;
        Gizmos.DrawCube(cubePosition, Vector3.one);
    }
#endif
}
