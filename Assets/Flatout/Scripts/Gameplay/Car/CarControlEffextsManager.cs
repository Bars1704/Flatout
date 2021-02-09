using System.Collections;
using UnityEngine;

namespace Flatout
{
    public class CarControlEffextsManager : MonoBehaviour
    {
        CarVFXSettings settings;
        [SerializeField] ParticleSystem BoosterParticles;
        [SerializeField] ParticleSystem BoosterBlastParticles;
        [SerializeField] Transform[] allWheels;
        [SerializeField] Transform[] rotatedWheels;
        Animator carAnimator;
        void Torque(float speed)
        {
            foreach (var wheel in allWheels)
                wheel.Rotate(0, 0, settings.TorqueSpeed * speed);
        }

        void RotateCar(Vector3 angle)
        {
            var YAngle = (angle.y - transform.rotation.eulerAngles.y);
            foreach (var wheel in rotatedWheels)
            {
                var rot = wheel.rotation.eulerAngles;
                var rotationAngle = Mathf.Lerp(0, YAngle, settings.RotateSmoothness) * settings.RotationSpeed;
                rot.y = Mathf.Clamp(rotationAngle, -settings.MaxRotationAngle, settings.MaxRotationAngle);
                wheel.localEulerAngles = rot;
            }
        }
        void CarDeath()
        {
            carAnimator.enabled = true;
            carAnimator.SetTrigger("Death");
        }
        private void Start()
        {
            settings = CarVFXSettings.Instance;

            carAnimator = GetComponentInChildren<Animator>(true);
            carAnimator.enabled = false;

            BoosterParticles.Stop();

            var controller = GetComponent<CarControlBase>();
            controller.OnCarBoostEnd += BoosterParticles.Stop;
            controller.OnCarBoostBegin += BoosterParticles.Play;
            controller.OnCarBoostDash += BoosterBlastParticles.Play;
            controller.OnCarRotate += RotateCar;
            controller.OnCarRun += Torque;
            GetComponent<CarBase>().OnDeath += x => CarDeath();
        }
    }
}
