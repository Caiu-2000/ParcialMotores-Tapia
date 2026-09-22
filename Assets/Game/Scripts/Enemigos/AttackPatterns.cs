using UnityEngine;

public abstract class AttackPatterns : IShoot
{
    protected BulletPool bulletPool;
    protected Transform muzzlePos;
    protected Entity owner;
    public void Init(BulletPool bulletPool, Transform muzzlePos, Entity owner)
    {
        this.bulletPool = bulletPool;
        this.muzzlePos = muzzlePos;
        this.owner = owner;
    }
    virtual public bool Shoot()
    {
        return false;
    }
    protected void SpawnBullet(Vector3 dir)
    {
        Bullet bullet = bulletPool.GetPrefab();
        bullet.Spawned(muzzlePos.position);
        bullet.transform.up = dir;
        bullet.RestartBullet(owner);
    }
}
