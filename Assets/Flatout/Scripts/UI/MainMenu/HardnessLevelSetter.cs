using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace Flatout
{
    [RequireComponent(typeof(Button))]
    public class HardnessLevelSetter : MonoBehaviour
    {
        private Text text;
        private List<HardnessLevel> hardnessLevels;
        private void Start()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(ChangeHardnessLevel);

            text = GetComponentInChildren<Text>();

            hardnessLevels = GlobalSettings.Instance.hardnessLevels;
            SetDefaultHardnessLevel();
            text.text = PlayerAvatar.Instance.hardnessLevel.name;
        }
        void SetDefaultHardnessLevel()
        {
            if (PlayerPrefs.HasKey("HardnessLevel"))
            {
                var hardnessLevelName = PlayerPrefs.GetString("HardnessLevel");
                var hardnessLevel = hardnessLevels.FirstOrDefault(x => x.name == hardnessLevelName);
                if (hardnessLevel == null) hardnessLevel = hardnessLevels.First();
                PlayerAvatar.Instance.hardnessLevel = hardnessLevel;
            }
            else
            {
                var defaultLevelName = hardnessLevels.First();
                PlayerAvatar.Instance.hardnessLevel = defaultLevelName;
                PlayerPrefs.SetString("HardnessLevel", defaultLevelName.name);
            }
        }
        void ChangeHardnessLevel()
        {
            var currentHardnessLevels = PlayerAvatar.Instance.hardnessLevel;
            var nextLevelIndex = hardnessLevels.IndexOf(currentHardnessLevels) + 1;
            if (nextLevelIndex >= hardnessLevels.Count)
                nextLevelIndex -= hardnessLevels.Count;
            var newHardnessLevel = hardnessLevels[nextLevelIndex];
            PlayerAvatar.Instance.hardnessLevel = newHardnessLevel;
            text.text = newHardnessLevel.name;
            PlayerPrefs.SetString("HardnessLevel", newHardnessLevel.name);

        }
    }

}
