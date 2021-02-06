using System.Collections;
using UnityEngine;

namespace Flatout
{
    public class CarControlEffextsManager : MonoBehaviour
    {
        [SerializeField] ParticleSystem BoosterParticles;
        [SerializeField] Transform[] allWheels;
        [SerializeField] Transform[] rotatedWheels;
        Animator carAnimator;
        void Torque(float speed)
        {
            foreach (var wheel in allWheels)
                wheel.Rotate(0, 0, speed);
        }

        void RotateCar(Vector3 angle)
        {
            angle *= 1.1f;
            angle.y += 90;

            foreach (var wheel in rotatedWheels)
                wheel.rotation = Quaternion.Euler(angle) ;
        }
        void CarDeath()
        {
            carAnimator.enabled = true;
            carAnimator.SetTrigger("Death");
        }
        private void Start()
        {
            carAnimator = GetComponentInChildren<Animator>(true);
            carAnimator.enabled = false;

            BoosterParticles.Stop();

            var controller = GetComponent<CarControlBase>();
            controller.OnCarBoostEnd += BoosterParticles.Stop;
            controller.OnCarBoostBegin += BoosterParticles.Play;
            controller.OnCarRotate += RotateCar;
            controller.OnCarRun += Torque;
            GetComponent<CarBase>().OnDeath += x => CarDeath();
        }
    }
}
