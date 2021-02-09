using UnityEngine;

namespace Flatout
{
    [RequireComponent(typeof(AudioSource))]
    public class CarSoundsController : MonoBehaviour
    {
        CarVFXSettings soundsBase;
        [SerializeField] AudioSource audioSource;
        void PlayCarBoostSound()
        {
            ResetAudioSourceSettings();
            audioSource.clip = soundsBase.CarBoostSound;
            audioSource.Play();
        }
        void PlayBoxCrashedSound()
        {
            ResetAudioSourceSettings();
            audioSource.PlayOneShot(soundsBase.BoxCrashSound);
        }
        void PlayCarRotateSound(Vector3 rotatingVector)
        {
            ResetAudioSourceSettings();
            audioSource.clip = soundsBase.CarDriftSound;
            audioSource.pitch = (transform.rotation.eulerAngles - rotatingVector).magnitude;
            audioSource.Play();
        }
        void PlayCarRunSound(float speed)
        {
            ResetAudioSourceSettings();
            audioSource.clip = soundsBase.CarRunSound;
            audioSource.volume = speed;
            audioSource.Play();
        }
        void PlayCarCrashSound()
        {
            ResetAudioSourceSettings();
            audioSource.PlayOneShot(soundsBase.CarCrashSound);
        }
        void ResetAudioSourceSettings()
        {
            audioSource.pitch = 1;
            audioSource.volume = 1;
        }
        public void Start()
        {
            soundsBase = CarVFXSettings.Instance;
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
