using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarBase: MonoBehaviour, ISmashable
{
    int MaxHealth;
    int Health;
    float MaxBooster;
    float Booster;
    float XP;
    int collisionDamage;
    public GameObject GameObj;
    
    public Action<CarBase> OnDeath;
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
    }

    void DamageOtherCar(Collider otherCarCollider) => DamageOtherCar(otherCarCollider.gameObject.GetComponentInParent<CarBase>());
    void DamageOtherCar(CarBase otherCar)
    {
        otherCar.TakeDamage(collisionDamage);
    }
    public void TakeDamage(int damage)
    {
        Health = Mathf.Max(0, Health - damage);
        if (Health == 0)
        {
            KillImmediately();
        }
    }

    public void KillImmediately()
    {
        OnDeath?.Invoke(this);
        GetComponentInChildren<Animator>().SetTrigger("Death");
    }
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

    public void OnSmash(Collider collider)
    {
        throw new NotImplementedException();
    }
}
