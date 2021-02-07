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
        public GameObject CarMenuViewPrefab;
        public float MovingSpeed;
        public float RotationSpeed;
        public float BoosterAmount;
        public float BoosterForce;
        public float BoosterTake;
        public int MaxHealth;
        public int Damage;
        public int BoostDashMultiplier = 10;
        public Sprite ButtonSprite;
    }
}
