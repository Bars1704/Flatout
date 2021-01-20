using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCastomization : MonoBehaviour
{
    void Start()
    {
        var actualCar = GlobalSettings.Instance.LevelsSettings.GetActualCar(PlayerAvatar.Instance.Level);
        SpawnCar(actualCar.CarPrefab);
    }

    void SpawnCar(GameObject carPrefab)
    {
        var carInstance = Instantiate(carPrefab, transform);
        carInstance.GetComponentInChildren<Animator>().enabled = false;
        carInstance.GetComponentInChildren<Rigidbody>().useGravity = false;
    }
}
