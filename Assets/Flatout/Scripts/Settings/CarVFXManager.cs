using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamebase.Miscellaneous;
using Doozy.Engine.Soundy;
namespace Flatout
{
    [CreateAssetMenu(fileName = "CarVFXManage", menuName = "Flatout/Static/CarVFXManage")]

    public class CarVFXManager : StaticScriptableObject<CarVFXManager>
    {
        public AudioClip music;
        public AudioClip CarRunSound;
        public AudioClip CarBoostSound;
        public AudioClip CarDriftSound;
        public AudioClip BoxCrashSound;
        public AudioClip CarCrashSound;
    }
}
