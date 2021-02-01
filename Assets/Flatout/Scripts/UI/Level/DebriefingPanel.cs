using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gamebase.Miscellaneous;

namespace Flatout
{
    public class DebriefingPanel : Singleton<DebriefingPanel>
    {
        [SerializeField] GameObject panel;
        [SerializeField] TextMeshProUGUI CarsCrashedText;
        [SerializeField] TextMeshProUGUI BoxCrashedText;
        [SerializeField] TextMeshProUGUI totalXPText;
        public override void Initialize() { panel.SetActive(false); }

        public void Show()
        {
            PlayerCar car = PlayerCar.Instance;
            CarsCrashedText.SetText(car.CarsCrashed.ToString());
            BoxCrashedText.SetText(car.BoxesCrashed.ToString());
            totalXPText.SetText(car.XP.ToString());
            panel.SetActive(true);
        }
    }
}
