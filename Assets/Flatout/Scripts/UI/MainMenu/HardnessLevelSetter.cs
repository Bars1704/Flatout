using System.Collections;
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
            NameText.text = PlayerAvatar.Instance.hardnessLevel.Name;
            DescriptionText.text = PlayerAvatar.Instance.hardnessLevel.Deskription;
        }
        void SetDefaultHardnessLevel()
        {
            if (PlayerPrefs.HasKey("HardnessLevel"))
            {
                var hardnessLevelName = PlayerPrefs.GetString("HardnessLevel");
                var hardnessLevel = hardnessLevels.FirstOrDefault(x => x.Name == hardnessLevelName);
                if (hardnessLevel == null) hardnessLevel = hardnessLevels.First();
                PlayerAvatar.Instance.hardnessLevel = hardnessLevel;
            }
            else
            {
                var defaultLevelName = hardnessLevels.First();
                PlayerAvatar.Instance.hardnessLevel = defaultLevelName;
                PlayerPrefs.SetString("HardnessLevel", defaultLevelName.Name);
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
            NameText.text = newHardnessLevel.Name;
            DescriptionText.text = newHardnessLevel.Deskription;
            PlayerPrefs.SetString("HardnessLevel", newHardnessLevel.Name);

        }
    }

}
