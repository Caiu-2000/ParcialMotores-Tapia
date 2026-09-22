using UnityEngine;

public class Diagonal3 : AttackPatterns
{
    [SerializeField] int bulletsToShoot = 12;
    int bulletsShoot;
    bool DiagonalShoot()
    {
        for (int i = 0; i < 3; i++)
        {
            float angleDeg = 225f + i * 45; //This is to get the angles of rotation so they move in a diagonal
            float angleRad = angleDeg * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0f); //This is mostly maths, but basically "Cos" it's the proyection of x meanwhile "sin" is the proyection of y
            SpawnBullet(dir);
        }
        bulletsShoot += 3;
        if (bulletsShoot >= bulletsToShoot)
        {
            bulletsShoot = 0;
            return false;
        }
        return true;

    }
    public override bool Shoot()
    {
        Debug.Log("Selected Diagonal");
        return DiagonalShoot();
    }
}
