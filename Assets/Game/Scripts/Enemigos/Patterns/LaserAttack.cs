using UnityEngine;

[CreateAssetMenu(fileName = "LaserAttack", menuName = "Enemigos/Patterns/LaserAttack")]
public class LaserAttack : AttackPatterns
{
    [SerializeField] float chargeTime = 1.5f;
    [SerializeField] float fireTime = 2f;
    float timer;
    bool firing;
    
    bool FireLaser()
    {
        Boss boss = owner as Boss;
        if (boss == null) return false;
        timer += 0.2f;
        if (firing == false)
        {
            boss.SetChargingVisual();
            if (timer >= chargeTime)
            {
                Debug.Log("TurnOnTheLaser");
                timer = 0f;
                firing = true;
                boss.laserBeam.Activate(owner);
                boss.SetFiringVisual();
            }
            return true;
        }
        if (timer >= fireTime)
        {
            Debug.Log("TurnOffTheLaser");
            boss.laserBeam.Deactivate();
            boss.SetIdleVisual();
            firing = false;
            return false;
        }
        return true;
    }
    public override bool Shoot()
    {
        return FireLaser();
    }
}
