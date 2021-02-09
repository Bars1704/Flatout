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
        public override void Init(CarTier carTier, GameObject gameObj)
        {
            base.Init(carTier, gameObj);
            SetHardnessValues(PlayerAvatar.Instance.HardnessLevel);
        }
        void SetHardnessValues(HardnessLevel hardnessLevel)
        {
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