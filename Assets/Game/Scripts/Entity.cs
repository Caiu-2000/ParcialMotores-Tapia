
using System;

using UnityEngine;

public class Entity : MonoBehaviour , IHittable
{
    [SerializeField] Healtcomponent healtcomponent= new Healtcomponent();

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void OnHit(DamageData data)
    {
        healtcomponent.Damage(data);
    }
}
