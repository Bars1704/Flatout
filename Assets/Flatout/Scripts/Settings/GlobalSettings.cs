using UnityEngine;
using Gamebase.Miscellaneous;
using Sirenix.OdinInspector;

namespace Flatout
{
    [CreateAssetMenu(fileName = "GlobalSettings", menuName = "Flatout/Static/GlobalSettings")]
    public class GlobalSettings : StaticScriptableObject<GlobalSettings>
    {
        [ShowInInspector]
        public string DefaultNickName = "New Car";
        public LevelSettings LevelsSettings;
        public GameObject NickNamePrefab;
    }
}