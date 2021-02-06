using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace Flatout
{
    [CreateAssetMenu(fileName = "HardnessLevel", menuName = "Flatout/HardnessLevel")]
    public class HardnessLevel : SerializedScriptableObject
    {
        public string Name;
        public string Deskription;

        [InfoBox("Key - смещение уровня относительно уровня игрока, Value - шанс выпадения того или иного смещения")]
        public Dictionary<int, float> BotLevelOffsetChance;

        private Dictionary<int, float> BotLevelOffsetLineSegment;

        [Header("Награды")]
        public int XPForCarCrash = 10;
        public int XPForBoxCrash = 10;
        public int XPForWin = 10;

        [Header("Модификаторы ботов")]
        public float BotSpeedModifier;
        public float BotDamageModifier;
        public float BotHealthModifier;

        void InitLineSegment()
        {
            BotLevelOffsetLineSegment = new Dictionary<int, float>(BotLevelOffsetChance);
            BotLevelOffsetLineSegment.ConvertToRandomLineSegment();
        }

        public int GetCarLevel(int playerLevel)
        {
            if (BotLevelOffsetLineSegment == default)
                InitLineSegment();

            return BotLevelOffsetLineSegment.GetRandomFromLineSegment();
        }

        public CarTier GetBotCar(int playerLevel)
            => GlobalSettings.Instance.LevelsSettings.GetActualCar(GetCarLevel(playerLevel));
    }
}

