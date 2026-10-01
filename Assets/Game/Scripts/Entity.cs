
using System;

using UnityEngine;

[DefaultExecutionOrder(5)]
public class Entity : MonoBehaviour , IHittable
{
    [SerializeField] protected Healtcomponent healtcomponent= new Healtcomponent();

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void OnHit(DamageData data)
    {
        healtcomponent.Damage(data);
    }

    public Healtcomponent getHealt() => healtcomponent;


    internal virtual void BulletFired(Bullet bullet)
    {
        SoundManager.instance.PlayRandom(SoundTypes.FiredPlayer);
    }
}
