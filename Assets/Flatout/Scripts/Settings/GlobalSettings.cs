using Gamebase.Miscellaneous;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
    [CreateAssetMenu(fileName = "GlobalSettings", menuName = "Flatout/Static/GlobalSettings")]
    public class GlobalSettings : StaticScriptableObject<GlobalSettings>
    {
        public string DefaultNickName = "New Car";
        public LevelSettings LevelsSettings;
        public GameObject NickNamePrefab;
        public GameObject HealthBarPrefab;

        public Vector2 HealtBarOffset;
        public Vector2 NameBarOffset;

        public List<HardnessLevel> hardnessLevels;
    }
}