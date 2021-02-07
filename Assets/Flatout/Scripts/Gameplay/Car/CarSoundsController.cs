using Doozy.Engine.Soundy;
using UnityEngine;

namespace Flatout
{
    [RequireComponent(typeof(AudioSource))]
    public class CarSoundsController : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        void PlayCarBoostSound()
        {
            ResetAudioSourceSettings();
            audioSource.clip = CarVFXManager.Instance.CarBoostSound;
            audioSource.Play();
        }
        void PlayBoxCrashedSound()
        {
            ResetAudioSourceSettings();
            audioSource.PlayOneShot(CarVFXManager.Instance.BoxCrashSound);
        }
        void PlayCarRotateSound(Vector3 rotatingVector)
        {
            ResetAudioSourceSettings();
            audioSource.clip = CarVFXManager.Instance.CarDriftSound;
            audioSource.pitch = (transform.rotation.eulerAngles - rotatingVector).magnitude;
            audioSource.Play();
        }
        void PlayCarRunSound(float speed)
        {
            ResetAudioSourceSettings();
            audioSource.clip = CarVFXManager.Instance.CarRunSound;
            audioSource.volume = speed;
            audioSource.Play();
        }
        void PlayCarCrashSound()
        {
            ResetAudioSourceSettings();
            audioSource.PlayOneShot(CarVFXManager.Instance.CarCrashSound);
        }
        void ResetAudioSourceSettings()
        {
            audioSource.pitch = 1;
            audioSource.volume = 1;
        }
        public void Start()
        {
            audioSource = GetComponent<AudioSource>();
            CarControlBase carControl = GetComponent<CarControlBase>();
            carControl.OnCarBoost += PlayCarBoostSound;
            carControl.OnCarRotate += PlayCarRotateSound;
            carControl.OnCarRun += PlayCarRunSound;
            GetComponent<CarBase>().OnCarCrashed += PlayCarCrashSound;
            GetComponent<CarBase>().OnBoxCrashed += PlayBoxCrashedSound;
        }
    }
}
