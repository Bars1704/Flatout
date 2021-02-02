using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Flatout
{
    /// <summary>
    /// Основной скрипт уровня
    /// </summary>
    public class LevelCore : MonoBehaviour
    {
        /// <summary>
        /// Точки спавна игроков
        /// </summary>
        [SerializeField, Tooltip("Точки спавна игроков")] Transform SpawnpointsParent;
        /// <summary>
        /// Компонент машинки игрока
        /// </summary>
        PlayerCar playerCar;
        /// <summary>
        /// Конфигурация машинки игрока
        /// </summary>
        CarTier PlayerCarTier;
        /// <summary>
        /// Список машинок-ботов
        /// </summary>
        List<BotCar> BotCars = new List<BotCar>();
        void Start()
        {
            SpawnCars();
        }
        /// <summary>
        /// Размещает ботов и игрока на заданных точках
        /// </summary>
        public void SpawnCars()
        {
            var spawnpoints = SpawnpointsParent.GetComponentsInChildren<SpawnPoint>()
                .Select(x => x.transform)
                .Shuffle()
                .ToList();
            PlayerCarTier = PlayerAvatar.Instance.ActualCar;
            SpawnPlayer(spawnpoints.First());
            spawnpoints.RemoveAt(0);
            SpawnBots(spawnpoints);
        }
        /// <summary>
        /// Спавнит игрока
        /// </summary>
        /// <param name="spawnPoint">Точка появления игрока</param>
        void SpawnPlayer(Transform spawnPoint)
        {
            var playerCarGameObj = Instantiate(PlayerCarTier.CarPrefab, spawnPoint);
            playerCar = playerCarGameObj.AddComponent<PlayerCar>();
            playerCar.Init(PlayerCarTier, playerCarGameObj);
            playerCar.OnDeath += x => LoseMatch();
            var carController = playerCarGameObj.AddComponent<CarManualControl>();
            carController.Init(PlayerCarTier, playerCar);
            playerCar.OnDeath += x => carController.enabled = false;
            SpawnFloatingNickName(playerCar);
            SpawnHealthBar(playerCar);
            FindObjectOfType<BoosterButton>().carControl = carController;
            var BoosterBar = FindObjectOfType<BoosterBar>();
            playerCar.OnBoosterChanged += BoosterBar.ShowBooster;
        }
        /// <summary>
        /// Победа игрока
        /// </summary>
        void WinMatch()
        {
            DebriefingPanel.Instance.Show();
            playerCar.AddXP(PlayerAvatar.Instance.hardnessLevel.XPForWin);
            Debug.Log("Win");
        }
        /// <summary>
        /// Поражение игрока
        /// </summary>
        void LoseMatch()
        {
            DebriefingPanel.Instance.Show();
            Debug.Log("Lose");
        }
        /// <summary>
        /// Отметка смерти машинки
        /// </summary>
        /// <param name="Car">Управляющий компонент машинки, которая умерла</param>
        /// 
        void RegisterBotDeath(CarBase Car)
        {
            if (Car == playerCar)
                LoseMatch();
            else
            {
                BotCars.Remove(BotCars.Find(x => x == Car));
                if (BotCars.Count == 0)
                    WinMatch();
            }
        }


        /// <summary>
        /// Спавнит UI-обьект с ником
        /// </summary>
        private void SpawnFloatingNickName(CarBase car)
        {
            var nickNameComponent = Instantiate(GlobalSettings.Instance.NickNamePrefab).GetComponent<NameBar>();
            nickNameComponent.Target = car.transform;
            nickNameComponent.NickName = car.GetCarNickName();
            car.OnDeath += (x) => Destroy(nickNameComponent.gameObject);

        }
        /// <summary>
        /// Спавнит UI-хелсбар
        /// </summary>
        private void SpawnHealthBar(CarBase car)
        {
            var healtBar = Instantiate(GlobalSettings.Instance.HealthBarPrefab).GetComponent<HealhBar>();
            healtBar.Target = car.transform;
            car.OnHealthChanged += healtBar.ShowHealth;
            car.OnDeath += (x) => Destroy(healtBar.gameObject);
        }

        /// <summary>
        /// Спавнит машинок-ботов
        /// </summary>
        /// <param name="spawnPoints">Точки спавна ботов</param>
        void SpawnBots(IEnumerable<Transform> spawnPoints)
        {
            foreach (var spawnpoint in spawnPoints)
            {
                var BotGO = Instantiate(PlayerCarTier.CarPrefab, spawnpoint);
                var BotBase = BotGO.AddComponent<BotCar>();
                BotBase.Init(PlayerCarTier, BotGO);
                BotBase.OnDeath += RegisterBotDeath;
                BotCars.Add(BotBase);
                SpawnFloatingNickName(BotBase);
                SpawnHealthBar(BotBase);
            }
            FakeNicknamesManager.Instance.FLushMemory();
        }
    }
}