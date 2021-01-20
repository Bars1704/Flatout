using UnityEngine;
using System.Linq;
using System.Collections.Generic;
namespace Flatout
{
    public class LevelCore : MonoBehaviour
    {
        [SerializeField] Transform SpawnpointsParent;
        PlayerCar PlayerCar;
        CarTier PlayerCarTier;
        List<BotCar> BotCars = new List<BotCar>();
        void Start()
        {
            SpawnCars();
        }

        public void SpawnCars()
        {
            var spawnpoints = SpawnpointsParent.GetComponentsInChildren<SpawnPoint>()
                .Select(x => x.transform)
                .Shuffle()
                .ToList();
            PlayerCarTier = GlobalSettings.Instance.LevelsSettings.GetActualCar();
            SpawnPlayer(spawnpoints.First());
            spawnpoints.RemoveAt(0);
            SpawnBots(spawnpoints);
        }

        void SpawnPlayer(Transform spawnPoint)
        {
            var playerCarGameObj = Instantiate(PlayerCarTier.CarPrefab, spawnPoint);
            PlayerCar = playerCarGameObj.AddComponent<PlayerCar>();
            PlayerCar.Init(PlayerCarTier, playerCarGameObj);
            PlayerCar.OnDeath += RegisterDeath;
            var carController = playerCarGameObj.AddComponent<CarManualControl>();
            carController.Init(PlayerCarTier);
        }

        void WinMatch() { Debug.Log("Win"); }
        void LoseMatch() { Debug.Log("Lose"); }

        void RegisterDeath(CarBase Car)
        {
            if (Car == PlayerCar)
                LoseMatch();
            else
            {
                BotCars.Remove(Car as BotCar);
                if (BotCars.Count == 0)
                    WinMatch();
            }
        }

        void SpawnBots(IEnumerable<Transform> spawnPoints)
        {
            foreach (var spawnpoint in spawnPoints)
            {
                var BotGO = Instantiate(PlayerCarTier.CarPrefab, spawnpoint);
                var BotBase = BotGO.AddComponent<BotCar>();
                BotBase.Init(PlayerCarTier, BotGO);
                BotCars.Add(BotBase);
            }
        }
    }
}