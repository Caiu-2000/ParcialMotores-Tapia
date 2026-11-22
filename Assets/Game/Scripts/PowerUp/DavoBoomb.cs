using System.Collections;
using UnityEngine;
[DefaultExecutionOrder(15000000)]
public class DavoBoomb : Bomb
{

    [SerializeField] int BulletCuantity;
    [SerializeField] float TimeBetween = 0.02f;

    private BulletPool pool;
    [SerializeField]
    private Bullet buletprefab;

    public override void Activate()
    {
        print("Se activo la bomba");
        float range = manager.GetlimitRange() / 2;
        StartCoroutine(explodeRutine(range));
    }


    private IEnumerator explodeRutine(float range)
    {
        if (pool == null) pool = new BulletPool(buletprefab, 20);
        for (int x = 0; x < BulletCuantity; x++)
        {
            Bullet bullet = pool.GetPrefab();
            bullet.RestartBullet(player , player.BulletSprite);
            bullet.Spawned(new Vector3(Random.Range(-range , range) , 0, 0));
            bullet.transform.Rotate(new Vector3 (0,0,180));
            EventManager<CombatEvents>.Publish<Bullet>(CombatEvents.PlayerFired, bullet);

            yield return new WaitForSeconds(TimeBetween);
        }
    }

    private void Start()
    {
        pool = player.getPool();
        manager = RunManager.instance;
    }


}
