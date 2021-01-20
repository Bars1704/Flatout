using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartLevel : MonoBehaviour
{
    public void LoadLevel()
    {
        //TODO: Добавить асинхронную загрузку + фейковый екран поиска игроков
        SceneManager.LoadScene(LevelList.Instance.GetRandomLevel());
    }
}
