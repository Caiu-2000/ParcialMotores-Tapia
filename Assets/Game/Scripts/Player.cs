
using System.Collections;
using UnityEngine;


public class Player : Entity
{
    [SerializeField] float speed = 2.0f;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] protected Bomb bomb;
    [SerializeField] protected int power = 1;
    [SerializeField] protected int reach = 1;
    [SerializeField] protected int duration = 1;
    [SerializeField] protected int stability = 1; // Estabilidad de disparos y control de personaje

    [SerializeField] protected int SellectedBuild = 0;
    [SerializeField] protected IPasive pasive;
   

    protected BulletPool bulletPool;
    public void Move(Vector2 dir)
    {
        
        transform.position += new Vector3(dir.x , dir.y , 0.0f) * speed * Time.deltaTime;
    }
    private void Start()
    {
        bulletPool = new BulletPool(bulletPrefab, 1);
        StartCoroutine(FireRutine());
        
    }
    private IEnumerator FireRutine()
    {
        bool switchbullet = false;
        while (true)
        {
            Bullet bullet = bulletPool.GetPrefab();
            bullet.RestartBullet(this , switchbullet);
            bullet.Spawned(this.transform.position);
            switchbullet = !switchbullet;
            yield return new WaitForSeconds(0.25f);
        }
    }
}


