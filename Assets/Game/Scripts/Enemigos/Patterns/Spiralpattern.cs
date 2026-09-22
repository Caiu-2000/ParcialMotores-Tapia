using UnityEngine;
public class Spiralpattern : AttackPatterns
{
    private float spiralAngle;
    [SerializeField] int bulletsToShoot = 20;
    int bulletsShoot = 0;

    bool FireSpiral(float stepDeg = 12f)
    {
        float rad = spiralAngle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        SpawnBullet(dir);
        spiralAngle += stepDeg;
        bulletsShoot++;
        if (bulletsShoot >= bulletsToShoot)
        {
            bulletsShoot = 0;
            return false;
        }
        return true;
    }

    public override bool Shoot()
    {
        Debug.Log("Selected Spiral");
        return FireSpiral();
    }
}
