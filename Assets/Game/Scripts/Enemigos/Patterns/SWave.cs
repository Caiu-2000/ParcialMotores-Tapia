using UnityEngine;

[CreateAssetMenu(fileName = "SWave", menuName = "Enemigos/Patterns/SWave")]
public class SWave : AttackPatterns
{
    [SerializeField] float baseAngle = 270;
    [SerializeField] float amplitude = 40f;
    [SerializeField] float frequency = 20f;
    [SerializeField] int bulletsToShoot = 30;
    int bulletsShoot;

    bool FireWave()
    {
        float offset=amplitude*Mathf.Sin(bulletsShoot*frequency*Mathf.Deg2Rad);
        float angleRad = (baseAngle+offset)*Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Cos(angleRad),Mathf.Sin(angleRad),0f);
        SpawnBullet(dir);
        bulletsShoot++;
        if(bulletsShoot > bulletsToShoot)
        {
            bulletsShoot = 0;
            return false;
        }
        return true;
    }
    public override bool Shoot()
    {
        Debug.Log("Selected SWave");
        return FireWave();
    }
}
