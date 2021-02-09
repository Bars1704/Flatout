using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamebase.Miscellaneous;
using Doozy.Engine.Soundy;
namespace Flatout
{
    [CreateAssetMenu(fileName = "CarVFXSettings", menuName = "Flatout/Static/CarVFXSettings")]

    public class CarVFXSettings : StaticScriptableObject<CarVFXSettings>
    {
        public AudioClip music;
        public AudioClip CarRunSound;
        public AudioClip CarBoostSound;
        public AudioClip CarDriftSound;
        public AudioClip BoxCrashSound;
        public AudioClip CarCrashSound;

        [Header("CarWheels")]
        public float MaxRotationAngle = 45;
        public float RotationSpeed = 33;
        public float TorqueSpeed = 3;
        [Range(0,1)]
        public float RotateSmoothness = 0.1f;
    }
}
