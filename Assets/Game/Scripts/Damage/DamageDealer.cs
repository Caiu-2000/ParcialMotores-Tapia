using UnityEngine;


public class DamageDealer : PlayerAffecter

{
    [SerializeField] int damage = 1;

    public override void applySelf(Player player)
    {
        player.OnHit(new DamageData(damage, null));
    }

}
