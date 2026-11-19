using System.Collections;
using UnityEngine;

public class DavoBoomb : Bomb
{

    [SerializeField] int BulletCuantity;
    [SerializeField] float TimeBetween = 0.02f;

    private BulletPool pool;
    public override void Activate()
    {
        print("Se activo la bomba");
        float range = manager.GetlimitRange() / 2;
        StartCoroutine(explodeRutine(range));
    }


    private IEnumerator explodeRutine(float range)
    {
        for (int x = 0; x < BulletCuantity; x++)
        {
            Bullet bullet = pool.GetPrefab();
            bullet.RestartBullet(player);
            bullet.Spawned(new Vector3(Random.Range(-range , range) , 0, 0));
            bullet.transform.Rotate(new Vector3 (0,0,180));
            print(x);
            yield return new WaitForSeconds(TimeBetween);
        }
    }

    private void Start()
    {
        pool = player.getPool();
        manager = RunManager.instance;
    }


}
