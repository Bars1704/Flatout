using Doozy.Engine.Soundy;
using UnityEngine;

namespace Flatout
{
    public class CarSoundsController : MonoBehaviour
    {
        void PlayCarBoostSound()
      => SoundyManager.Play(CarVFXManager.Instance.CarBoostSound, null, transform.position, 1, 1, false, 1);

        void PlayBoxCrashedSound()
            => SoundyManager.Play(CarVFXManager.Instance.BoxCrashSound, null, transform.position, 1, 1, false, 1);


        void PlayCarRotateSound(Vector3 rotatingVector)
        => SoundyManager.Play(CarVFXManager.Instance.CarDriftSound, null, transform.position, 1, (transform.rotation.eulerAngles - rotatingVector).magnitude, false, 1);

        void PlayCarRunSound(float speed)
        => SoundyManager.Play(CarVFXManager.Instance.CarRunSound, null, transform.position, speed, 1, false, 1);
        void PlayCarCrashSound()
       => SoundyManager.Play(CarVFXManager.Instance.CarCrashSound, null, transform.position, 1, 1, false, 1);

        public void Start()
        {
            SoundyManager.StopAllSounds();
           /// SoundyManager.Play(CarVFXManager.Instance.music, null, Vector3.zero, 0.05f, 1, true, 0);
            CarControlBase carControl = GetComponent<CarControlBase>();
            carControl.OnCarBoost += PlayCarBoostSound;
            carControl.OnCarRotate += PlayCarRotateSound;
            carControl.OnCarRun += PlayCarRunSound;
            GetComponent<CarBase>().OnCarCrashed += PlayCarCrashSound;
            GetComponent<CarBase>().OnBoxCrashed += PlayBoxCrashedSound;
        }
    }
}
