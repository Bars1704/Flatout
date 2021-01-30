using System.Collections.Generic;
using UnityEngine;
using Gamebase.Miscellaneous;

namespace Flatout
{
    [CreateAssetMenu(fileName = "Level List", menuName = "Flatout/Static/Level List")]
    public class LevelList : StaticScriptableObject<LevelList>
    {
        [SerializeField] List<SceneReference> Levels;
        public string GetRandomLevel()
        {
            var name = Levels[Random.Range(0, Levels.Count)].ScenePath;
            return name;
        }
    }
}