using InControl;
using UnityEngine;

namespace Flatout
{
    /// <summary>
    /// Управление машинкой с помощью джойстика
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class CarJoystickControl : CarControlBase
    {
        /// <summary>
        /// Джойстик управления
        /// </summary>
        private TouchStickControl runStick;

        private void Awake()
        {
            runStick = FindObjectOfType<TouchStickControl>();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (runStick.IsActive)
            {
                var speed = runStick.Value.magnitude;
                if (speed >= runStick.lowerDeadZone)
                {
                    Run(speed);
                    Rotate(new Vector3(runStick.Value.x, 0, runStick.Value.y));
                }
            }
        }


#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DashBoost();
            }
        }
#endif

    }
}


