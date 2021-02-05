using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Flatout
{
    /// <summary>
    /// Машинка-бот
    /// </summary>
    public class BotCar : CarBase
    {
        //TODO: вынести реализацию инициализации отдельно в каждый из классов (убрать донастройку в старте)
        private void Start()
        {
            var hardnessLevel = PlayerAvatar.Instance.hardnessLevel;
            Health = (int)(Health * hardnessLevel.BotHealthModifier);
            MaxHealth = Health;
            collisionDamage = (int)(collisionDamage * hardnessLevel.BotDamageModifier);
        }
        public override Texture GetCarColor(CarTier carTier)
        {
            return carTier.CarColors.Shuffle().First().Key;
        }

        public override string GetCarNickName()
        {
            return FakeNicknamesManager.Instance.GetNickName();
        }
    }
}