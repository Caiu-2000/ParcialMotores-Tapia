using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class Healtcomponent : IObservable<HealthData> , ITimable
{
    public delegate void Death();

    public Death onDead = delegate { };
    protected List<IObserver<HealthData>> healtObservers = new();
    [SerializeField] private int MaxHealth = 3;
    [SerializeField] private float CdTime = 0.0f;
    public int CurrentHealth;

    bool onCD = false;

    public Healtcomponent()
    {
        CurrentHealth = MaxHealth;
    }
    public Healtcomponent(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
    }
    public void Suscribe(IObserver<HealthData> observer)
    {
        if ( healtObservers != null && !healtObservers.Contains(observer) ) { healtObservers.Add(observer); }
    }

    public void UnSuscribe(IObserver<HealthData> observer) => healtObservers.Remove(observer);

    public void Damage(DamageData data)
    {
        if (onCD) return;
        CurrentHealth -= data.Damage;
        Gamemanager.instance.UniversalTimer(CdTime, this);
        onCD = true;
        if (CurrentHealth <= 0)
        {
            Die();
            return;
        }

        HealthData healthData = new HealthData(CurrentHealth , MaxHealth); // Example values
        foreach (var observer in healtObservers)
        {
            observer.Notify(healthData);
        }
    }


    public  void Die()
    {
        SoundManager.instance.Play(SoundTypes.EnemyDead);

        onDead?.Invoke();
    }

    public void TimeStopped()
    {
        onCD = false;
    }
}


public interface ITimable
{
    public void TimeStopped();
}
