using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;

namespace Flatout
{
    [CreateAssetMenu(fileName = "CarTier", menuName = "Flatout/CarTier")]
    public class CarTier : SerializedScriptableObject
    {
        [field: ShowInInspector]
        public Dictionary<Texture, Sprite> CarColors;
        public GameObject CarPrefab;
        public float MovingSpeed;
        public float RotationSpeed;
        public float BoosterAmount;
        public float BoosterForce;
        public int MaxHealth;
        public int Damage;
    }
}
