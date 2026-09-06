using UnityEngine;

public class Enemy : Entity
{

    public override void OnHit(DamageData data)
    {
        if (data.FromWho is Player) base.OnHit(data);

    }
}
