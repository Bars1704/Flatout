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
        [InfoBox("Не забудьте нажать кнопку NormalizeRandomness после заполнения!")]
        public Dictionary<int, float> BotLevelOffsetChance;

        [Header("Награды")]
        public int XPForCarCrash = 10;
        public int XPForBoxCrash = 10;
        public int XPForWin = 10;

        [Header("Модификаторы ботов")]
        public float BotSpeedModifier;
        public float BotDamageModifier;
        public float BotHealthModifier;

        [Button]
        void NormalizeRandomness()
        {
            BotLevelOffsetChance.OrderBy(x => x.Key);
            float totalValue = BotLevelOffsetChance.Sum(x => x.Value);
            for(int i = 0; i < BotLevelOffsetChance.Count; i++)
            {
                var currElement = BotLevelOffsetChance.ElementAt(i);
                BotLevelOffsetChance[currElement.Key] = currElement.Value / totalValue;
            }
        }
    }
}

