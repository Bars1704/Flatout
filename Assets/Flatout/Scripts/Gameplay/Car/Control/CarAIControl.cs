using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using System.Collections;

namespace Flatout
{
    /// <summary>
    /// Управление машинкой-ботом
    /// </summary>
    public class CarAIControl : CarControlBase
    {
        #region PrivateFields
        Transform _target;
        /// <summary>
        /// Настройки модуля
        /// </summary>
        CarAIConfig aiConfig;
        /// <summary>
        /// Событие, вызывающееся каждый <see cref="FixedUpdate"/>
        /// </summary>
        Action onFixedStep;
        /// <summary>
        /// Пустышка-обьект, нужен для отметки точки назначения в некоторых стратегиях
        /// </summary>
        Transform targetMarker;
        /// <summary>
        /// Список живых машинко
        /// </summary>
        List<CarBase> aliveCarsPool;
        /// <summary>
        /// true, если машинка делала рывок в погоне за этим игроком
        /// </summary>
        bool isDashed = false;

        /// <summary>
        /// Направление поворота при обьезде препядствий: 1 - направо, -1 налево
        /// </summary>
        int RotateDirection;
        #endregion

        #region PrivateFields
        /// <summary>
        /// Точка, которую преследует машинка
        /// </summary>
        Transform target
        {
            get => _target;
            set
            {
                CarBase carBaseComponent = null;
                if (_target?.TryGetComponent(out carBaseComponent) ?? false)
                {
                    carBaseComponent.OnDeath -= HandleTargetDeath;
                    carBase.OnGetDamaged -= HandleTargetGetDamaged;
                }

                _target = value;

                if (value.TryGetComponent(out carBaseComponent))
                {
                    carBaseComponent.OnDeath += HandleTargetDeath;
                    carBase.OnGetDamaged += HandleTargetGetDamaged;
                }
                isDashed = false;
            }
        }
        #endregion

        #region LifeCycle
        private void Start()
        {
            targetMarker = Instantiate(new GameObject()).GetComponent<Transform>();
            StartCoroutine(UpdateRandomValues());
            SetNewTarget();
        }
        private void OnDestroy()
        {
            StopAllCoroutines();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            MoveForvard();
            CheckDashBoost();
            CalculateRotation();

            onFixedStep?.Invoke();
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(targetMarker.position, 0.5f);
        }
        #endregion

        #region PrivateMethods

        /// <summary>
        /// Проверяет, можeт ли машинка попасть в эту точку
        /// </summary>
        /// <param name="point">Точка, которую нужно проверить</param>
        /// <returns>true если машинка может попасть в проверяемую точку</returns>
        bool IsValidPoint(Vector3 point)
        {
            var rayCastPoint = point;
            rayCastPoint.y += 10;
            return Physics.Raycast(rayCastPoint, Vector3.down, 10, 1 << 13);
        }

        /// <summary>
        /// Обработка смерти цели
        /// </summary>
        /// <param name="deadCar">Машинка, которая умерла</param>
        void HandleTargetDeath(CarBase deadCar) => SetNewTarget();

        /// <summary>
        /// Обработка получения целью урона
        /// </summary>
        /// <param name="attacker">Машинка, которая нанесла урон цели</param>
        void HandleTargetGetDamaged(CarBase carBase)
        {
            MoveToRandomPoint(aiConfig.AfterCollisionMoveDistance);
        }

        /// <summary>
        /// Проверка, достигла ли машинка поставленной точки
        /// Работает только с теми стратегиями, где цель не привязана к той или иной машинке,
        /// и задана через <see cref="targetMarker"/>
        /// </summary>
        void CheckTargetPointDistance()
        {
            if (DistanceToPoint(target) < 1)
            {
                SetNewTarget();
                targetMarker.position = transform.position;
                onFixedStep -= CheckTargetPointDistance;
            }
        }

        /// <summary>
        /// Стратегия передвижения в случайную точку
        /// </summary>
        /// <param name="radius">Максимальное растояние до точки-цели</param>
        void MoveToRandomPoint(float radius)
        {
            Vector3 direction;
            int counter = 0;
            do
            {
                direction = (Random.insideUnitSphere * radius) + transform.position;
                direction.y = 0;
            } while (!IsValidPoint(direction) && counter++ < aiConfig.InCycleTries);

            if (!IsValidPoint(direction))
            {
                SetNewTarget();
                return;
            }

            targetMarker.position = direction;
            target = targetMarker;

            onFixedStep += CheckTargetPointDistance;
        }

        /// <summary>
        /// Расчитывает расстояние до точки
        /// </summary>
        /// <param name="point">точка, до которой считается расстояние</param>
        /// <returns>Квадрат расстояния</returns>
        /// <remarks>Так как везде внутри модуля расстояние считается одним и тем же образом, дорогой операцией вычисления квадратного корня можно пренебречь </remarks>
        float DistanceToPoint(Transform point)
        => Vector3.SqrMagnitude(point.position - transform.position);

        /// <summary>
        /// Производит поворот машинки с учетом обьезда препядствий
        /// </summary>
        void CalculateRotation()
        {
            var rotateVector = (target.transform.position - transform.position).normalized;
            RaycastHit hit;
            var isAvoidingObstacle = Physics.Raycast(transform.position, rotateVector, out hit, aiConfig.ObstacleDetectingDistance, 1 << 11);
            rotateVector = isAvoidingObstacle ? CalculateAvoidRotate(hit.distance) : rotateVector;
            Rotate(rotateVector);

            #region SubMethods
            /// <summary>
            /// Расчитывает поворот, необходимый, чтобы обогнуть препядствие
            /// </summary>
            /// <param name="distanceToObstacle">расстояние до препядствия</param>
            Vector3 CalculateAvoidRotate(float distanceToObstacle)
            {
                int rotateTries = 0;
                Vector3 currentTarget = Vector3.zero;
                do
                {
                    var rotateAngle = Mathf.Lerp(aiConfig.ObstacleAvoidingAngle, 0, distanceToObstacle / aiConfig.ObstacleDetectingDistance);
                    rotateAngle *= RotateDirection;
                    currentTarget = Quaternion.Euler(0, rotateAngle, 0) * transform.forward;
                } while (rotateTries++ <= aiConfig.InCycleTries &&
                    Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized, aiConfig.ObstacleDetectingDistance, 1 << 11));
                return currentTarget;
            }
            #endregion
        }

        /// <summary>
        /// Стартовая инициализация 
        /// </summary>
        /// <param name="carTier">Параметры машинки</param>
        /// <param name="carBase">Основной компонент машинки</param>
        /// <param name="allCars">Список всех живых машинок</param>
        public void Init(CarTier carTier, CarBase carBase, List<CarBase> allCars)
        {
            base.Init(carTier, carBase);
            aliveCarsPool = allCars;
            aiConfig = CarAIConfig.Instance;
            SetRandonRotateDirection();
        }

        /// <summary>
        /// Устанавливает случайное направление поворота
        /// </summary>
        void SetRandonRotateDirection() =>
    RotateDirection = Random.value > 0.5 ? 1 : -1;

        /// <summary>
        /// Случайным образом выбирает ту или иную стратегию поведения
        /// </summary>
        void SetNewTarget()
        {
            //TODO: вынести выбор цели в отдельный метод, в управлении оставить только преследование точки
            Dictionary<Action, float> probabilities = new Dictionary<Action, float>();
            probabilities.Add(FollowCarInRange, aiConfig.RandomCarInRangeTargetChance);
            probabilities.Add(() => MoveToRandomPoint(aiConfig.RandomPointTargetMaxDistance), aiConfig.RandomPointTargetChance);
            probabilities.Add(FollowTheNearestCar, aiConfig.NearestCarTargetChance);
            probabilities.Add(FollowRandomCar, aiConfig.RandomCarTargetChance);

            probabilities.GetRandomWithProbabilities().Invoke();

            #region SubMethods

            /// <summary>
            /// Стратегия преследования случайной машинки из заданного радиуса
            /// </summary>
            void FollowCarInRange()
            {
                var carsInRange = aliveCarsPool
                    .Where(x => x.gameObject != gameObject)
                    .Where(x => DistanceToPoint(x.transform) < aiConfig.AgroRange);

                if (carsInRange.Count() == 0)
                    FollowTheNearestCar();
                else
                    target = carsInRange.GetRandomElement().transform;
            }

            /// <summary>
            /// Стратегия преследования ближайшей машинки
            /// </summary>
            void FollowTheNearestCar()
            {
                CarBase nearestCar = null;
                var nearestCarMagnitude = float.MaxValue;
                for (int i = 0; i < aliveCarsPool.Count; i++)
                {
                    if (aliveCarsPool[i].gameObject == gameObject)
                        continue;
                    var distance = DistanceToPoint(aliveCarsPool[i].transform);
                    if (distance < nearestCarMagnitude)
                    {
                        nearestCarMagnitude = distance;
                        nearestCar = aliveCarsPool[i];
                    }
                }
                target = nearestCar.transform;
            }

            /// <summary>
            /// Стратегия преследования случайно выбранной машинки
            /// </summary>
            void FollowRandomCar()
            {
                target = aliveCarsPool
                    .Where(x => x.gameObject != gameObject)
                    .GetRandomElement().transform;
            }
            #endregion
        }

        /// <summary>
        /// Движение вперед с расчитанной скоростью
        /// </summary>
        void MoveForvard()
        {
            Run(CalculateSpeedModifier());
            #region SubMethods
            /// <summary>
            /// Расчитывает скорость движения вперед на основе настройки сложности и расстояния до цели (замедление перед столкновением)
            /// </summary>
            float CalculateSpeedModifier()
            {
                var hardnessLevelMultiplier = PlayerAvatar.Instance.hardnessLevel.BotSpeedModifier;
                var closenessToTargetMultiplier = Mathf.InverseLerp(0, aiConfig.SlowDistanceEnable, DistanceToPoint(target));
                closenessToTargetMultiplier = Mathf.Max(closenessToTargetMultiplier, aiConfig.MinSpeed);
                return hardnessLevelMultiplier * closenessToTargetMultiplier;
            }
            #endregion
        }

        /// <summary>
        /// Проверяет, можно ли выполнить ускорение-рывок, и, если можно, делает его с некой вероятностью
        /// </summary>
        void CheckDashBoost()
        {
            if (isDashed) return;
            if (AngleToTarget() > aiConfig.DashBoostAngle) return;
            if (Random.value > aiConfig.DashBoostChance) return;

            DashBoost();
            isDashed = true;

            #region SubMethods
            /// <summary>
            /// Вычисляет угол между направлением езды машинки и целью
            /// </summary>
            float AngleToTarget()
            {
                var thisRotation = transform.rotation.eulerAngles;
                var targetLookRotation = target.position - transform.position;
                return Vector3.Angle(thisRotation, targetLookRotation);
            }
            #endregion 
        }
        #endregion

        #region Corutines
        /// <summary>
        /// Корутина, которая раз в заданное количество времени делает попытку обновить часть случайных значений
        /// </summary>
        ///<remarks>Делая проверку раз в определенное время, а не каждый кадр решаются сразу две проблемы: производительность и возможность задать в конфиге более "понятные" значения вероятности смены того или иного значения</remarks>
        IEnumerator UpdateRandomValues()
        {
            while (true)
            {
                if (Random.value <= aiConfig.ChangeTargetChance)
                    SetNewTarget();
                if (Random.value <= aiConfig.ChangeRotationChance)
                    SetRandonRotateDirection();
                yield return new WaitForSeconds(aiConfig.RandomValuesUpdateTime);
            }
        }
        #endregion
    }
}
