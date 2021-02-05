using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using System.Collections;

namespace Flatout
{
    public class CarAIControl : CarControlBase
    {
        const float SlowDistanceEnable = 10f;
        const float MinSpeed = 0.5f;
        const float AgroRange = 50f;
        const float DashBoostAngle = 30;
        const float DashBoostChance = 0.001f;
        const float ChangeTargetChance = 0.3f;
        const float ObstacleAvoidingAngle = 10f;
        const float ObstacleDetectingDistance = 40f;
        const float ChangeRotationChance = 0.3f;
        const float RandomCarInRangeTargetChance = 0.3f;
        const float RandomPointTargetChance = 0.3f;
        const float RandomPointTargetMaxDistance = 40f;
        const float AfterCollisionMoveDistance = 20f;
        const float NearestCarTargetChance = 0.2f;
        const float RandomCarTargetChance = 0.2f;
        const float RandomValuesUpdateTime = 3f;
        const int ObstacleAvoidRotateTries = 100;
        
        Transform _target;
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
                    carBase.OnGetDamaged -= RegistreTargetGetDamaged;
                }

                _target = value;

                if (value.TryGetComponent(out carBaseComponent))
                {
                    carBaseComponent.OnDeath += HandleTargetDeath;
                    carBase.OnGetDamaged += RegistreTargetGetDamaged;
                }
                isDashed = false;
            }
        }

        /// <summary>
        /// Проверяет, можно ли машинка попасть в эту точку
        /// </summary>
        /// <param name="point">Точка, которую нужно проверить</param>
        /// <returns>true если машинка может попасть в проверяемую точку</returns>
        bool IsValidPoint(Vector3 point)
        {
            var rayCastPoint = point;
            rayCastPoint.y += 10;
            return Physics.Raycast(rayCastPoint, Vector3.down, 1 << 0);
        }

        Action onFixedStep;

        Transform targetMarker;

        List<CarBase> aliveCarsPool;
        void HandleTargetDeath(CarBase x) => SetNewTarget();

        void MoveToRandomPoint(float radius)
        {
            Vector3 direction;
            int counter = 0;
            do
            {
                direction = (Random.insideUnitSphere * radius) + transform.position;
                direction.y = 0;
            } while (!IsValidPoint(direction) && counter++ < ObstacleAvoidRotateTries);

            if (!IsValidPoint(direction))
            {
                SetNewTarget();
                return;
            }

            targetMarker.position = direction;
            target = targetMarker;

            onFixedStep += CheckTargetPointDistance;

            void CheckTargetPointDistance()
            {
                if (DistanceToPoint(target) < 1)
                {
                    SetNewTarget();
                    targetMarker.position = transform.position;
                    onFixedStep -= CheckTargetPointDistance;
                }
            }
        }
        float DistanceToPoint(Transform car)
        => Vector3.SqrMagnitude(car.position - transform.position);

        bool isDashed = false;
        int RotateDirection;
        public void Init(CarTier carTier, CarBase carBase, List<CarBase> allCars)
        {
            base.Init(carTier, carBase);
            aliveCarsPool = allCars;
            SetRandonRotateDirection();
        }
        void SetRandonRotateDirection() =>
            RotateDirection = Random.value > 0.5 ? 1 : -1;

        void RegistreTargetGetDamaged(CarBase attacker)
        {
            if (DistanceToPoint(target) > 2) return;
            MoveToRandomPoint(AfterCollisionMoveDistance);
        }
        private void Start()
        {
            SetNewTarget();
            targetMarker = Instantiate(new GameObject()).GetComponent<Transform>();
            StartCoroutine(UpdateRandomValues());
        }
        private void OnDestroy()
        {
            StopAllCoroutines();
        }
        IEnumerator UpdateRandomValues()
        {
            while (true)
            {
                if (Random.value <= ChangeTargetChance)
                    SetNewTarget();
                if (Random.value <= ChangeRotationChance)
                    SetRandonRotateDirection();
                yield return new WaitForSeconds(RandomValuesUpdateTime);
            }
        }
        //TODO: вынести выбор цели в отдельный метод, в управлении оставить только преследование точки
        void SetNewTarget()
        {
            Dictionary<Action, float> probabilities = new Dictionary<Action, float>();
            probabilities.Add(FollowCarInRange, RandomCarInRangeTargetChance);
            probabilities.Add(() => MoveToRandomPoint(RandomPointTargetMaxDistance), RandomPointTargetChance);
            probabilities.Add(FollowTheNearestCar, NearestCarTargetChance);
            probabilities.Add(FollowRandomCar, RandomCarTargetChance);

            probabilities.GetRandomWithProbabilities().Invoke();

            #region SubMethods
            void FollowCarInRange()
            {
                var carsInRange = aliveCarsPool
                    .Where(x => x.gameObject != gameObject)
                    .Where(x => DistanceToPoint(x.transform) < AgroRange);

                if (carsInRange.Count() == 0)
                    FollowTheNearestCar();
                else
                    target = carsInRange.GetRandomElement().transform;
            }
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
            void FollowRandomCar()
            {
                target = aliveCarsPool
                    .Where(x => x.gameObject != gameObject)
                    .GetRandomElement().transform;
            }
            #endregion
        }
        void MoveForvard()
        {
            Run(CalculateSpeedModifier());
            #region SubMethods
            float CalculateSpeedModifier()
            {
                var hardnessLevelMultiplier = PlayerAvatar.Instance.hardnessLevel.BotSpeedModifier;
                var closenessToTargetMultiplier = Mathf.InverseLerp(0, SlowDistanceEnable, DistanceToPoint(target));
                closenessToTargetMultiplier = Mathf.Max(closenessToTargetMultiplier, MinSpeed);
                return hardnessLevelMultiplier * closenessToTargetMultiplier;
            }
            #endregion
        }
        void CheckDashBoost()
        {
            if (isDashed) return;
            if (AngleToTarget() > DashBoostAngle) return;
            if (Random.value > DashBoostChance) return;

            DashBoost();
            isDashed = true;

            float AngleToTarget()
            {
                var thisRotation = transform.rotation.eulerAngles;
                var targetLookRotation = target.position - transform.position;
                return Vector3.Angle(thisRotation, targetLookRotation);
            }
        }
        void CalculateRotation()
        {
            var rotateVector = (target.transform.position - transform.position).normalized;
            RaycastHit hit;
            var isAvoidingObstacle = Physics.Raycast(transform.position, rotateVector, out hit, ObstacleDetectingDistance, 1 << 11);
            rotateVector = isAvoidingObstacle ? CalculateAvoidRotate(hit.distance) : rotateVector;
            Rotate(rotateVector);

            #region SubMethods
            Vector3 CalculateAvoidRotate(float distanceToObstacle)
            {
                int rotateTries = 0;
                Vector3 currentTarget = Vector3.zero;
                do
                {
                    var rotateAngle = Mathf.Lerp(ObstacleAvoidingAngle, 0, distanceToObstacle / ObstacleDetectingDistance);
                    rotateAngle *= RotateDirection;
                    currentTarget = Quaternion.Euler(0, rotateAngle, 0) * transform.forward;
                } while (rotateTries++ <= ObstacleAvoidRotateTries &&
                    Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized, ObstacleDetectingDistance, 1 << 11));
                return currentTarget;
            }
            #endregion
        }
        private void FixedUpdate()
        {
            MoveForvard();
            CheckDashBoost();
            CalculateRotation();

            onFixedStep?.Invoke();
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(targetMarker.position, 0.5f);
        }
    }
}
