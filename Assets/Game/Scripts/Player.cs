
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Player : Entity
{
    [SerializeField] float speed = 2.0f;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Bullet SpecialBulletPreffab;
    
    

    [SerializeField] protected Bomb bomb;
    [SerializeField] protected int power = 1;
    [SerializeField] protected int reach = 1;
    [SerializeField] protected int duration = 1;
    [SerializeField] protected int stability = 1; // Estabilidad de disparos y control de personaje

    [SerializeField] protected int SellectedBuild = 0;
    [SerializeField] protected IPasive pasive;


    [SerializeField] protected Collectable collectable;

    [SerializeField] protected Sprite BaseSprite;
    [SerializeField] Sprite InvincibleSprite;
    [SerializeField] Sprite BuffedSprite;

    [SerializeField] SpriteRenderer characterRenderer;
    protected BulletPool bulletPool;

    public bool Buffed { private set; get; } = false;
    public bool invincible { private set; get; } = false;



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

    public void LaunchBomb()
    {

    }
    public void BuffPlayer()
    {

    }
    private IEnumerator BuffRoutine(float Bufftime)
    {
        Buffed = true;
        yield return new WaitForSeconds(Bufftime);
        Buffed = false;
    }
    private IEnumerator InvincibleTime(float time)
    {
        invincible = true;
        yield return new WaitForSeconds(time);
        invincible = false;
    }

    private void Update()
    {
        if (invincible) characterRenderer.sprite = InvincibleSprite;
        else if (Buffed) characterRenderer.sprite = BuffedSprite;
        else characterRenderer.sprite = BaseSprite;
    } 

}

public abstract class Collectable : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TryGetComponent<Player>(out Player player))
        {
            applySelf(player);
        }
    }
    public abstract void applySelf(Player player);


}
public class BuffCollectable : Collectable
{
    public override void applySelf(Player player)
    {
        throw new System.NotImplementedException();
    }
}