using Gamebase.Miscellaneous;
using UnityEngine;

namespace Flatout
{
    [RequireComponent(typeof(Canvas))]
    public class UIFollowersCanvas : Singleton<UIFollowersCanvas>
    {
        public Canvas FollowersCancas { get; private set; }
        public override void Initialize()
        {
            FollowersCancas = GetComponent<Canvas>();
        }
    }
}