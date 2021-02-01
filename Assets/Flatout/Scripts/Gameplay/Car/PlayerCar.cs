using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

namespace Flatout
{
    /// <summary>
    /// Машинка игрока
    /// </summary>
    public class PlayerCar : CarBase
    {

        private void Start()
        {
            var cineMachine = FindObjectOfType<CinemachineVirtualCamera>();
            cineMachine.Follow = transform;
            cineMachine.LookAt = transform;
        }
        public override string GetCarNickName()
        => PlayerAvatar.Instance.Nickname;

        public override Texture GetCarColor(CarTier carTier)
        {
            var textureName = PlayerPrefs.GetString("CarColor");
            var actualTexture = carTier.CarColors.FirstOrDefault(x => x.Key.name == textureName);
            return actualTexture.Key ?? carTier.CarColors.First().Key;
        }
    }
}
