
using System;
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


    

    [SerializeField] protected Sprite BaseSprite;
    [SerializeField] Sprite InvincibleSprite;
    [SerializeField] Sprite BuffedSprite;

    [SerializeField] int PoolSize = 1;

    [SerializeField] SpriteRenderer characterRenderer;

    [SerializeField] float FireTime = 0.25f;
    protected BulletPool bulletPool;
    protected int bombCuantity = 3;
    public bool Buffed { private set; get; } = false;
    public bool invincible { private set; get; } = false;


    // Este mueve al personaje 
    public void Move(Vector2 dir)
    {
        
        transform.position += new Vector3(dir.x , dir.y , 0.0f) * speed * Time.deltaTime;
    }
    // Este es para que se dirija a un punto especifico. 
    public void MoveTowards(Vector3 objective)
    {
        if (Vector3.Distance(objective, transform.position) < 0.1f) return;
        Vector3 direction = (objective - transform.position).normalized;
  
        Move(direction);
    }

    private void Start()
    {
        bulletPool = new BulletPool(bulletPrefab, PoolSize);
        StartCoroutine(FireRutine());
        EventManager<GameEvent>.Publish<int>(GameEvent.DamagePlayer, healtcomponent.CurrentHealth);
        EventManager<GameEvent>.Publish<int>(GameEvent.BombTroued, bombCuantity);
        healtcomponent.onDead += deadHandling;

    }
    private IEnumerator FireRutine()
    {
        bool switchbullet = false;
        while (true)
        {
            SoundManager.instance.PlayRandom(SoundTypes.FiredPlayer);
            Bullet bullet = bulletPool.GetPrefab();
            bullet.RestartBullet(this , switchbullet , Buffed);
            bullet.Spawned(this.transform.position);
            switchbullet = !switchbullet;
            yield return new WaitForSeconds(FireTime);
        }
    }

    // Con el eventmanager no se poner eventos sin valores asi que queda con variable x solo para poder pasar. Por eso no se usa
    public void LaunchBomb(int x)
    {
        
        if (bombCuantity > 0) 
        {
            bombCuantity -= 1;
            EventManager<GameEvent>.Publish<int>(GameEvent.BombTroued, bombCuantity);
            StartCoroutine(InvincibleTime(1.0f));
            bomb.Activate(); 
        }
    }
    public void BuffPlayer(float buffDuration = 2.0f)
    {
        SoundManager.instance.PlayPitched(SoundTypes.PowerUp);
        StartCoroutine(BuffRoutine(buffDuration));
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
    public override void OnHit(DamageData data)
    {
        if (invincible) return;
        base.OnHit(data);
        SoundManager.instance.PlayRandom(SoundTypes.HittedPlayer);
        EventManager<GameEvent>.Publish<int>(GameEvent.DamagePlayer, healtcomponent.CurrentHealth);
    }

    public BulletPool getPool() => bulletPool;


    private void OnEnable()
    {
        EventManager<InputEvents>.Subscribe<int>(InputEvents.BombPressed, LaunchBomb);
    }
    private void OnDisable()
    {
        EventManager<InputEvents>.Unsubscribe<int>(InputEvents.BombPressed, LaunchBomb);
    }

    public void  deadHandling()
    {
        Gamemanager.instance.PlayerDied();
    }
}
