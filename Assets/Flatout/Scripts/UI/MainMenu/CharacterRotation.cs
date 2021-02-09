using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Bomberman
{
    public class CharacterRotation : MonoBehaviour
    {
        private Touch touch;
        private Quaternion rotationY;
        [SerializeField]
        private float rotateSpeedModifie = 0.2f;

        private bool isRotating = false;
        private Coroutine coroutine;

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    isRotating = true;

                    if (coroutine != null)
                        StopCoroutine(coroutine);

                    rotationY = Quaternion.Euler(0f, -touch.deltaPosition.x * rotateSpeedModifie, 0f);

                    transform.rotation = rotationY * transform.rotation;
                }
            }
            else if (isRotating)
            {
                isRotating = false;
                coroutine = StartCoroutine(RotateToDefault());
            }

        }

        private IEnumerator RotateToDefault()
        {
            yield return new WaitForSeconds(2f);
            transform.DORotate(new Vector3(0f, -180f, 0f), 1f);
            yield return null;
        }
    }
}