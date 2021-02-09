using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Flatout
{
    [RequireComponent(typeof(Button))]
    public class GoToMainMenuButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(GoToMainMenu);
        }

        void GoToMainMenu()
        {
            SceneManager.LoadScene(LevelList.Instance.MainMenuScene.ScenePath);
        }
    }
}
