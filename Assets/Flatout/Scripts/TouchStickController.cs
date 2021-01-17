using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
    public class TouchStickController : MonoBehaviour
    {
        private TouchStickControl movementStick;
        [SerializeField] private CarPhysicalControll carController;
        void Start()
        {
            movementStick = GetComponent<TouchStickControl>();
        }

        // Update is called once per frame
        void Update()
        {
            carController.MoveTorvards(movementStick.Value.y);
            carController.Rotate(movementStick.Value.x);
        }
    }
}
