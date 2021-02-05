using UnityEngine;
using Gamebase.Miscellaneous;
using Sirenix.OdinInspector;

namespace Flatout
{
    /// <summary>
    /// Хранит в себе настройки ИИ ботов-машинок
    /// </summary>
    [CreateAssetMenu(fileName = "CarAIConfig", menuName = "Flatout/Static/CarAIConfig")]
    public class CarAIConfig : StaticScriptableObject<CarAIConfig>
    {
        [Header("Передвижение")]
        [Tooltip("Расстояние до цели, после которого машинка начнет притормаживать")]
        public float SlowDistanceEnable = 10f;
        [Tooltip("Минимальный коеффициент торможения машинки")]
        [Range(0, 1)]
        public float MinSpeed = 0.5f;
        [Tooltip("Угол, в котором должна находиться цель, чтобы машинка попробовала ускориться")]
        public float DashBoostAngle = 30;
        [Tooltip("Вероятность машинкой ускориться")]
        public float DashBoostChance = 0.01f;

        [Header("Поворот и обьезд препядствий")]
        [Tooltip("Скорость поворота при обьезде препядствий")]
        public float ObstacleAvoidingAngle = 10f;
        [Tooltip("Расстояние до препядствия, после которого машинка начнет пытаться обьехать его")]
        public float ObstacleDetectingDistance = 40f;
        [Tooltip("Вероятность изменить направление поворота на случайное")]
        [Range(0, 1)]
        public float ChangeRotationChance = 0.3f;

        [Header("Выбор цели")]
        [Tooltip("Вероятность сменить стратегию выбора цели")]
        [Range(0, 1)]
        public float ChangeTargetChance = 0.3f;
        [InfoBox("Значения вероятностей выбора стратегий могут быть любыми, не обязательно в промежутке 0-1 - система сама рассчитает вероятность выпадения каждого")]
        [Tooltip("Вероятность выбрать стратегию \"Ехать за случайной машинкой из радиуса вокруг\"")]
        public float RandomCarInRangeTargetChance = 0.3f;
        [Tooltip("Радиус, внутри которого выбирается машинка")]
        public float AgroRange = 50f;
        [Tooltip("Вероятность выбрать стратегию \"Ехать в случайную точку\"")]
        public float RandomPointTargetChance = 1f;
        [Tooltip("Максимальное расстояние случайной точки до машинки")]
        public float RandomPointTargetMaxDistance = 40f;
        [Tooltip("Вероятность выбрать стратегию \"Ехать за ближайшей машинкой\"")]
        public float NearestCarTargetChance = 0.1f;
        [Tooltip("Вероятность выбрать стратегию \"Ехать за случайной машинкой\"")]
        public float RandomCarTargetChance = 0.1f;

        [Header("Разное")]
        [Tooltip("Интервал, с  которым происходит попытка обновить случаные значения (Такие как угол поворота и цель преследования)")]
        public float RandomValuesUpdateTime = 2f;
        [Tooltip("Количество попыток сделать что-либо в цикле, прежде чем выйти из него")]
        public int InCycleTries = 100;
        [Tooltip("Максимальное расстояние, на которое отьедет машика после удара")]
        public float AfterCollisionMoveDistance = 20f;

    }
}

