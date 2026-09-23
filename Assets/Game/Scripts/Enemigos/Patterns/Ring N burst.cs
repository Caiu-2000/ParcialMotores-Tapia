using UnityEngine;
[CreateAssetMenu(fileName = "RingNBurst", menuName = "Enemigos/Patterns/RingNBurst")]
public class RingNburst : AttackPatterns
{
    [SerializeField] int bulletsPerCircle = 12;
    [SerializeField] int bulletsToShoot = 3;
    int burstsFired;

    bool FireCircle()
    {
        float stepDeg = 360f / bulletsPerCircle;
        for (int i = 0; i < bulletsPerCircle; i++)
        {
            float angleRad = Mathf.Rad2Deg * stepDeg * i;
            Debug.Log(stepDeg * i);
            Vector3 dir = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0f);
            SpawnBullet(dir);
        }
        burstsFired++;
        if (burstsFired >= bulletsToShoot)
        {
            burstsFired = 0;
            return false;
        }
        return true;
    }
    public override bool Shoot()
    {
        Debug.Log("Selected Circle");
        return FireCircle();
    }
}
