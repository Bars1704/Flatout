using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Flatout
{
    public class CarSelectPanel : MonoBehaviour
    {
        [SerializeField] GameObject CarButtonSelectPrefab;
        CarCastomization carCastomization;
        void Start()
        {
            carCastomization = FindObjectOfType<CarCastomization>();
            foreach (var currentCarTier in
                GlobalSettings.Instance.LevelsSettings.LevelForOpeningCar.Select(x => x.Value))
            {
                var buttonGO = Instantiate(CarButtonSelectPrefab, transform);
                buttonGO.GetComponent<Image>().sprite = currentCarTier.ButtonSprite;
                buttonGO.GetComponent<Button>().onClick.AddListener(() => OnCarSelect(currentCarTier));
            }
        }

        void OnCarSelect(CarTier car)
        {
            carCastomization.SpawnCar(car.CarMenuViewPrefab);
            PlayerAvatar.Instance.SetSelectedCarTer(car);
        }
    }
}
