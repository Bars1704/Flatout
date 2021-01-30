﻿using UnityEngine;
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
        PlayerCar PlayerCar;
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
            PlayerCar = playerCarGameObj.AddComponent<PlayerCar>();
            PlayerCar.Init(PlayerCarTier, playerCarGameObj);
            PlayerCar.OnDeath += x => LoseMatch();
            var carController = playerCarGameObj.AddComponent<CarManualControl>();
            carController.Init(PlayerCarTier);
            PlayerCar.OnDeath += x => carController.enabled = false;
        }
        /// <summary>
        /// Победа игрока
        /// </summary>
        void WinMatch() { Debug.Log("Win"); }
        /// <summary>
        /// Поражение игрока
        /// </summary>
        void LoseMatch() { Debug.Log("Lose"); }
        /// <summary>
        /// Отметка смерти машинки
        /// </summary>
        /// <param name="Car">Управляющий компонент машинки, которая умерла</param>
        void RegisterBotDeath(CarBase Car)
        {
            if (Car == PlayerCar)
                LoseMatch();
            else
            {
                BotCars.Remove(BotCars.Find(x => x == Car));
                if (BotCars.Count == 0)
                    WinMatch();
            }
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
            }
            FakeNicknamesManager.Instance.FLushMemory();
        }
    }
}