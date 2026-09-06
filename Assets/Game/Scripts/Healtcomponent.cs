using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class Healtcomponent : IObservable<HealthData>
{
    protected List<IObserver<HealthData>> healtObservers = new();
    [SerializeField] private int MaxHealth = 3;
    protected int CurrentHealth;

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
        CurrentHealth -= data.Damage;
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

    public void Die()
    {

    }
}
