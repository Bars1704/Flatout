using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
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
    }
}
