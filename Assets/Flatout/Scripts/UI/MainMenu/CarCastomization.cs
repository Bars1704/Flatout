using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Doozy.Engine.Soundy;

namespace Flatout
{
    /// <summary>
    /// UI-компонент кастомизации машинки
    /// </summary>
    public class CarCastomization : MonoBehaviour
    {
        /// <summary>
        /// Префаб кнопки выбора цвета
        /// </summary>
        [SerializeField] GameObject carColorSelecterButtonPrefab;
        /// <summary>
        /// Панель, на которой будут размещены кнопки
        /// </summary>
        [SerializeField] Transform colorSelecterPanel;
        /// <summary>
        /// Точка появления пустышки-машины
        /// </summary>
        [SerializeField] Transform carSpawnPoint;
        GameObject carDummy;
        CarTier actualCar;
        void Start()
        {
            actualCar = PlayerAvatar.Instance.ActualCar;
            SpawnCar(actualCar.CarPrefab);
            SpawnColourButtons();
            SetCarDefaultColor();
            SoundyManager.Play(CarVFXManager.Instance.music, null, Vector3.zero, 0.5f, 1, true, 0);
        }
        /// <summary>
        /// Устанавливает превью-машинке цвет по умолчанию
        /// </summary>
        void SetCarDefaultColor()
        {
            var textureName = PlayerPrefs.GetString("CarColor");
            var actualTexture = actualCar.CarColors.FirstOrDefault(x => x.Key.name == textureName);
            SetColor(actualTexture.Key ?? actualCar.CarColors.First().Key);
        }
        /// <summary>
        /// Спавнит кнопки выбора цвета
        /// </summary>
        void SpawnColourButtons()
        {
            foreach (var color in actualCar.CarColors)
            {
                var carButton = Instantiate(carColorSelecterButtonPrefab, colorSelecterPanel);
                carButton.GetComponent<Image>().sprite = color.Value;
                carButton.GetComponent<Button>().onClick.AddListener(() => SetColor(color.Key));
            }
        }

        void SetColor(Texture texture)
        {
            foreach (var renderer in carDummy.GetComponentsInChildren<Renderer>())
            {
                if (renderer.gameObject.name.ToLower().Contains("wheel")) continue;
                renderer.material.SetTexture(1, texture);
            }
            PlayerPrefs.SetString("CarColor", texture.name);
        }

        /// <summary>
        /// Спавнит на сцене "пустышку" машинки
        /// </summary>
        /// <param name="carPrefab">префаб машинки</param>
        void SpawnCar(GameObject carPrefab)
        {
            carDummy = Instantiate(carPrefab, carSpawnPoint);

            var components = carDummy.GetComponents<Component>();
            foreach (var component in components)
            {
                if (component is Transform) continue;
                Destroy(component);
            }
        }
    }
}