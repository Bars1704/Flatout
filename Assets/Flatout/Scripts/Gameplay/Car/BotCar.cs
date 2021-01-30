using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
    /// <summary>
    /// Машинка-бот
    /// </summary>
    public class BotCar : CarBase
    {
        protected override string GetCarNickName()
        {
            return FakeNicknamesManager.Instance.GetNickName();
        }
    }
}