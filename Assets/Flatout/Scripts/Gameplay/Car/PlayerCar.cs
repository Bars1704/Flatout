using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
    /// <summary>
    /// Машинка игрока
    /// </summary>
    public class PlayerCar : CarBase
    {
        protected override string GetCarNickName()
        => PlayerAvatar.Instance.Nickname;
    }
}
