using Gamebase.Miscellaneous;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
    [CreateAssetMenu(fileName = "Level List", menuName = "Flatout/Static/Level List")]
    public class LevelList : StaticScriptableObject<LevelList>
    {
        public SceneReference MainMenuScene;

        [SerializeField] List<SceneReference> Levels;
        public string GetRandomLevel()
        {
            var name = Levels[Random.Range(0, Levels.Count)].ScenePath;
            return name;
        }
    }
}