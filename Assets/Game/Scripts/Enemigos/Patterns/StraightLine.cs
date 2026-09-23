using UnityEngine;

[CreateAssetMenu(fileName = "StraightLine", menuName = "Enemigos/Patterns/StraightLine")]
public class StraightLine : AttackPatterns
{
    [SerializeField] int bulletsToShoot = 6;
    int bulletsShoot;
    
    bool ShootBullets()
    {
        Vector3 dir = new Vector3(0, -1, 0);
        SpawnBullet(dir);
        bulletsShoot++;
        if(bulletsShoot >= bulletsToShoot)
        {
            bulletsShoot = 0;
            return false;
        }
        return true;
    }
    public override bool Shoot()
    {
        Debug.Log("Selected Straight");
        return ShootBullets();
    }
}
