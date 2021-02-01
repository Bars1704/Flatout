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