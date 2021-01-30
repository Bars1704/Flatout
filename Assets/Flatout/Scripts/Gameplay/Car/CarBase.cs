﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
    /// <summary>
    /// Основа машинки
    /// </summary>
    public abstract class CarBase : MonoBehaviour
    {
        /// <summary>
        /// Максимальное количествово здоровья
        /// </summary>
        int MaxHealth;
        /// <summary>
        /// Текущее количествово здоровья
        /// </summary>
        int Health;
        /// <summary>
        /// Максимальное количество заряда бустера
        /// </summary>
        float MaxBooster;
        /// <summary>
        /// Текущий заряд бустера
        /// </summary>
        float Booster;
        /// <summary>
        /// Урон, наносимый при столкновении
        /// </summary>
        int collisionDamage;
        /// <summary>
        /// <see cref="GameObject"/> компонент машинки
        /// </summary>
        public GameObject GameObj;
        /// <summary>
        /// Событие смерти машинки
        /// </summary>
        public Action<CarBase> OnDeath;
        /// <summary>
        /// Инициализация машинки
        /// </summary>
        /// <param name="carTier">Конфигурация машинки - отсюда берутся все ее стартовые характеристики</param>
        /// <param name="gameObj"><see cref="GameObject"/> компонент машинки</param>
        public void Init(CarTier carTier, GameObject gameObj)
        {
            MaxHealth = carTier.MaxHealth;
            Health = MaxHealth;
            MaxBooster = carTier.BoosterAmount;
            Booster = MaxBooster;
            GameObj = gameObj;
            collisionDamage = carTier.Damage;
            foreach (var damageZone in GetComponentsInChildren<TriggerCollider>())
            {
                damageZone.OnTriggered += DamageOtherCar;
            }
            SpawnFloatingNickName();
        }
        /// <summary>
        /// Нанесение урона другой машинке
        /// </summary>
        /// <param name="otherCarCollider"><see cref="Collider"/> машинки</param>
        void DamageOtherCar(Collider otherCarCollider) => DamageOtherCar(otherCarCollider.gameObject.GetComponentInParent<CarBase>());
        /// <summary>
        /// Нанесение урона другой машинке
        /// </summary>
        /// <param name="otherCar">Управляющий компонент машинки</param>
        void DamageOtherCar(CarBase otherCar)
        {
            otherCar?.TakeDamage(collisionDamage);
        }
        /// <summary>
        /// Получение урона
        /// </summary>
        /// <param name="damage">Количество полученного урона</param>
        public void TakeDamage(int damage)
        {
            Health = Mathf.Max(0, Health - damage);
            if (Health == 0)
            {
                KillImmediately();
            }
        }
        /// <summary>
        /// Спавнит UI-обьект с ником
        /// </summary>
        private void SpawnFloatingNickName()
        {
            var x = Instantiate(GlobalSettings.Instance.NickNamePrefab).GetComponent<NameBar>();
            x.Target = transform;
            x.NickName = GetCarNickName();
        }
        /// <summary>
        /// Возвращает никнейм машинки
        /// </summary>
        /// <returns>Никнейм</returns>
        protected abstract string GetCarNickName();
        /// <summary>
        /// Смерть
        /// </summary>
        public void KillImmediately()
        {
            OnDeath?.Invoke(this);
            GetComponentInChildren<Animator>().SetTrigger("Death");
        }
        /// <summary>
        /// Пытается взять заданное количество заряда ускорения
        /// </summary>
        /// <param name="boosterAmount">Максимально количество заряда, которое берется за эту команду</param>
        /// <returns>Количество реально взятого бустера (0 если заряд пустой)</returns>
        public float TakeBooster(float boosterAmount)
        {
            Booster = Booster - boosterAmount;
            var receivedBooster = boosterAmount;
            if (Booster < 0)
            {
                receivedBooster += Booster;
                Booster = 0;
            }
            return receivedBooster;
        }
    }

}