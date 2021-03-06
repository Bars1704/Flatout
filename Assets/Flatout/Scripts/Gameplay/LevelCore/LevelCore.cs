﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        /// <summary>
        /// Все машинки в сумме;
        /// </summary>
        List<CarBase> AllCars = new List<CarBase>();
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
            AllCars.Add(playerCar);

            spawnpoints.RemoveAt(0);
            SpawnBots(spawnpoints);
            AllCars.AddRange(BotCars);
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
            var carController = playerCarGameObj.AddComponent<CarJoystickControl>();
            carController.Init(PlayerCarTier, playerCar);
            playerCar.OnDeath += x => carController.enabled = false;
            SpawnFloatingNickName(playerCar);
            SpawnHealthBar(playerCar);
            var BoosterBar = FindObjectOfType<BoosterBar>();
            playerCar.OnBoosterChanged += BoosterBar.ShowBooster;
        }
        /// <summary>
        /// Победа игрока
        /// </summary>
        void WinMatch()
        {
            DebriefingPanel.Instance.Show();
            playerCar.AddXP(PlayerAvatar.Instance.HardnessLevel.XPForWin);
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
            BotCars.Remove(BotCars.Find(x => x == Car));
            AllCars.Remove(Car);
            if (BotCars.Count == 0)
                WinMatch();
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
                int playerLevel = PlayerAvatar.Instance.Level;
                var botLevelBase = PlayerAvatar.Instance.HardnessLevel.GetBotCar(playerLevel);
                var botGameObject = Instantiate(botLevelBase.CarPrefab, spawnpoint);
                var botBaseComponennt = botGameObject.AddComponent<BotCar>();
                var botAIComponent = botGameObject.AddComponent<CarAIControl>();
                botBaseComponennt.OnDeath += x => botAIComponent.enabled = false;
                botAIComponent.Init(PlayerCarTier, botBaseComponennt, AllCars);
                botBaseComponennt.Init(PlayerCarTier, botGameObject);
                botBaseComponennt.OnDeath += RegisterBotDeath;
                BotCars.Add(botBaseComponennt);
                SpawnFloatingNickName(botBaseComponennt);
                SpawnHealthBar(botBaseComponennt);
            }
            FakeNicknamesManager.Instance.FLushMemory();
        }
    }
}