using UnityEngine;
using System.Collections.Generic;

public class BaseEnemyAttack
{
    private List<AttackPatterns> attacksPatterns = new List<AttackPatterns>();
    private float cooldown;
    private bool firing = false;
    private float attackCooldown = 5f;
    private int selectPattern;

    public BaseEnemyAttack(BulletPool bulletPool, Transform muzzlePos, Entity owner, List<AttackPatterns> attacks)
    {
        foreach (AttackPatterns attackPattern in attacks)
        {
            attackPattern.Init(bulletPool, muzzlePos, owner);
            attacksPatterns.Add(attackPattern);
        }
    }
    public void Shoot()
    {
        bool readyToShoot = true;
        if (cooldown > 0) return;
        if (firing == false)
        {
            selectPattern = attacksPatterns.Count == 1 ? 0 : Random.Range(0, attacksPatterns.Count);
            firing = true;
        }
    
        readyToShoot = attacksPatterns[selectPattern].Shoot();
    
        if (readyToShoot == false)
        {
            cooldown = attackCooldown;
            firing = false;
        }
        
    }
    public void UpdateTimer()
    {
        cooldown -= Time.deltaTime;
    }


}
