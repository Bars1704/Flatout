using Gamebase.Miscellaneous;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Flatout/Static/LevelSettings")]
    public class LevelSettings : SerializedScriptableObject
    {
        [SerializeField]
        List<int> expToUnlockNewLevel;
        [ShowInInspector, OdinSerialize]
        private Dictionary<int, CarTier> LevelForOpeningCar = new Dictionary<int, CarTier>();
        //TODO: сделать возможность добавлять разные типы вознаграждений за уровень

        public int CheckLevel(int XpAmount)
        {
            for (int i = 1; i < expToUnlockNewLevel.Count; i++)
            {
                if (expToUnlockNewLevel[i] > XpAmount)
                    return expToUnlockNewLevel[i - 1];
            }
            return expToUnlockNewLevel[expToUnlockNewLevel.Count - 1];
        }
        public CarTier GetActualCar(int playerLevel)
        {
            CarTier resultCar;
            while (!LevelForOpeningCar.TryGetValue(playerLevel, out resultCar) && playerLevel >= 0)
                playerLevel--;
            return resultCar;
        }
    }
}