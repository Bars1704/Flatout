using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flatout
{
    /// <summary>
    /// Скрипт кнопки загрузки игрового уровня
    /// </summary>
    public class StartLevel : MonoBehaviour
    {
        /// <summary>
        /// Загрузка игрового уровня
        /// </summary>
        public void LoadLevel()
        {
            //TODO: Добавить асинхронную загрузку + фейковый екран поиска игроков
            SceneManager.LoadScene(LevelList.Instance.GetRandomLevel());
        }
    }
}
