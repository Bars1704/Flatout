using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Flatout
{
    [RequireComponent(typeof(Button))]
    public class HardnessLevelSetter : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI NameText;
        [SerializeField]
        private TextMeshProUGUI DescriptionText;
        private List<HardnessLevel> hardnessLevels;
        private void Start()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(ChangeHardnessLevel);

            NameText = GetComponentInChildren<TextMeshProUGUI>();

            hardnessLevels = GlobalSettings.Instance.hardnessLevels;
            SetDefaultHardnessLevel();
            NameText.text = PlayerAvatar.Instance.HardnessLevel.LevelName;
            DescriptionText.text = PlayerAvatar.Instance.HardnessLevel.Deskription;
        }
        void SetDefaultHardnessLevel()
        {
            if (PlayerPrefs.HasKey("HardnessLevel"))
            {
                var hardnessLevelName = PlayerPrefs.GetString("HardnessLevel");
                var hardnessLevel = hardnessLevels.FirstOrDefault(x => x.LevelName == hardnessLevelName);
                if (hardnessLevel == null) hardnessLevel = hardnessLevels.First();
                PlayerAvatar.Instance.HardnessLevel = hardnessLevel;
            }
            else
            {
                var defaultLevelName = hardnessLevels.First();
                PlayerAvatar.Instance.HardnessLevel = defaultLevelName;
                PlayerPrefs.SetString("HardnessLevel", defaultLevelName.LevelName);
            }
        }
        void ChangeHardnessLevel()
        {
            var currentHardnessLevels = PlayerAvatar.Instance.HardnessLevel;
            var nextLevelIndex = hardnessLevels.IndexOf(currentHardnessLevels) + 1;
            if (nextLevelIndex >= hardnessLevels.Count)
                nextLevelIndex -= hardnessLevels.Count;
            var newHardnessLevel = hardnessLevels[nextLevelIndex];
            PlayerAvatar.Instance.HardnessLevel = newHardnessLevel;
            NameText.text = newHardnessLevel.LevelName;
            DescriptionText.text = newHardnessLevel.Deskription;
            PlayerPrefs.SetString("HardnessLevel", newHardnessLevel.LevelName);

        }
    }

}
