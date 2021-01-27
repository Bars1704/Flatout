using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Gamebase.Miscellaneous;
using UnityEngine.SceneManagement;

namespace Flatout
{
    [CreateAssetMenu(fileName = "Level List", menuName = "Flatout/Static/Leve List")]
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