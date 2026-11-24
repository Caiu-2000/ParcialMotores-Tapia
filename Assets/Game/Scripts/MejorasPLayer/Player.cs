using System.Collections;
using UnityEngine;

// MEJORAS PLAYER
// Versión mejorada del Player original (el original quedó comentado en Entidades/Player/Player.cs).
// Usa el mismo GUID que el original, así los prefabs Davo y Cobra no pierden ningún valor.
//
// Qué cambia respecto del original:
//  - MoveBy: movimiento táctil 1:1 (lo usa JoyController). El personaje nunca sale de la pantalla
//    ni se mete debajo de la franja de abajo del HUD.
//  - Invencibilidad después de recibir un golpe, con parpadeo (antes solo había con la bomba).
//  - Con el potenciador ahora dispara la bala especial (antes siempre usaba BulletSprite).
//  - Los estados usan temporizadores en vez de corrutinas: agarrar dos potenciadores seguidos
//    ya no corta el segundo antes de tiempo.
//  - Punto de hitbox visible (HitboxVisual) del tamaño real de "size".
//  - Si no hay skin elegida (por ejemplo, al dar Play directo en la escena de combate) usa la primera
//    skin de CharacterData en vez de tirar un error.
[DefaultExecutionOrder(15)]
public class Player : Entity
{
    [SerializeField] CharacterStats CharacterData;
    public static Player Instance { get; private set; }

    [SerializeField] float speed = 2.0f;
    [SerializeField] Bullet bulletPrefab;

    [SerializeField] protected Bomb bomb;
    [SerializeField] protected int power = 1;
    [SerializeField] protected int reach = 1;
    [SerializeField] protected int duration = 1;
    [SerializeField] protected int stability = 1; // Estabilidad de disparos y control de personaje

    [SerializeField] protected int SellectedBuild = 0;
    [SerializeField] protected IPasive pasive;

    protected Sprite BaseSprite;
    Sprite InvincibleSprite;
    Sprite BuffedSprite;

    [SerializeField] int PoolSize = 1;

    [SerializeField] SpriteRenderer characterRenderer;

    [SerializeField] float FireTime = 0.25f;
    protected BulletPool bulletPool;
    protected int bombCuantity = 3;

    [SerializeField] public Sprite BulletSprite;
    [SerializeField] protected Sprite SpecialBulletSprite;

    [Header("Mejoras de movimiento")]
    [Tooltip("Multiplicador de velocidad con Shift (modo lento, para esquivar con precisión).")]
    [SerializeField, Range(0.1f, 1f)] float focusSpeedMultiplier = 0.45f;
    [Tooltip("Distancia mínima a los bordes de la pantalla.")]
    [SerializeField] float screenMargin = 0.3f;
    [Tooltip("Desde dónde salen las balas, relativo al personaje.")]
    [SerializeField] Vector2 muzzleOffset = new Vector2(0f, 0.4f);

    [Header("Mejoras de daño")]
    [Tooltip("Segundos de invencibilidad (con parpadeo) después de recibir un golpe.")]
    [SerializeField] float invincibilityAfterHit = 1.5f;
    [Tooltip("Segundos de invencibilidad al tirar una bomba.")]
    [SerializeField] float bombInvincibility = 1.0f;
    [Tooltip("Muestra el punto de la hitbox real.")]
    [SerializeField] bool showHitbox = true;

    Skinset currentSet;
    float invincibleUntil;
    float buffedUntil;

    public bool Buffed => Time.time < buffedUntil;
    public bool invincible => Time.time < invincibleUntil;
    public bool IsFocused { get; set; }

    // Este mueve al personaje (teclado). Ahora respeta los bordes de la pantalla.
    public void Move(Vector2 dir)
    {
        float currentSpeed = speed * (IsFocused ? focusSpeedMultiplier : 1f);
        MoveBy(dir * (currentSpeed * Time.deltaTime));
    }

    // Este es para que se dirija a un punto especifico.
    public void MoveTowards(Vector3 objective)
    {
        if (Vector3.Distance(objective, transform.position) < 0.1f) return;
        Vector3 direction = (objective - transform.position).normalized;
        Move(direction);
    }

    // Movimiento táctil 1:1: desplaza al personaje exactamente lo que se movió el dedo.
    public void MoveBy(Vector2 delta)
    {
        if (Time.timeScale <= 0f) return; // en pausa no se mueve
        Vector2 target = ScreenBounds.ClampToPlayArea((Vector2)transform.position + delta, screenMargin);
        transform.position = new Vector3(target.x, target.y, transform.position.z);
    }

    private void Start()
    {
        Instance = this;
        bulletPool = new BulletPool(bulletPrefab, PoolSize);
        StartCoroutine(FireRutine());
        EventManager<GameEvent>.Publish<int>(GameEvent.DamagePlayer, healtcomponent.CurrentHealth);
        EventManager<GameEvent>.Publish<int>(GameEvent.BombTroued, bombCuantity);
        healtcomponent.onDead += deadHandling;

        RunManager.instance.PlayerReady(this);

        currentSet = GlobalData.CurrentSkin;
        if (currentSet == null && CharacterData != null && CharacterData.Skins != null && CharacterData.Skins.Length > 0)
            currentSet = CharacterData.Skins[0];
        if (currentSet != null) characterRenderer.sprite = currentSet.NormalSprite;

        if (showHitbox) HitboxVisual.Create(transform, size);
        ScreenBounds.DetectBottomHud();
    }

    private IEnumerator FireRutine()
    {
        WaitForSeconds wait = new WaitForSeconds(FireTime);
        while (true)
        {
            Bullet bullet = bulletPool.GetPrefab();

            // Antes se calculaba currentSprite pero se pasaba siempre BulletSprite.
            Sprite currentSprite = Buffed && SpecialBulletSprite != null ? SpecialBulletSprite : BulletSprite;

            bullet.Spawned(transform.position + (Vector3)muzzleOffset);
            bullet.RestartBullet(this, currentSprite);
            BulletFired(bullet);
            yield return wait;
        }
    }

    internal override void BulletFired(Bullet bullet)
    {
        base.BulletFired(bullet);
        EventManager<CombatEvents>.Publish<Bullet>(CombatEvents.PlayerFired, bullet);
    }

    // Con el eventmanager no se poner eventos sin valores asi que queda con variable x solo para poder pasar. Por eso no se usa
    public void LaunchBomb(int x)
    {
        if (bomb != null && bombCuantity > 0 && Time.timeScale > 0f)
        {
            bombCuantity -= 1;
            EventManager<GameEvent>.Publish<int>(GameEvent.BombTroued, bombCuantity);
            SetInvincible(bombInvincibility);
            bomb.Activate();
        }
    }

    public void BuffPlayer(float buffDuration = 2.0f)
    {
        SoundManager.instance.PlayPitched(SoundTypes.PowerUp);
        buffedUntil = Mathf.Max(buffedUntil, Time.time + buffDuration);
    }

    public void SetInvincible(float time)
    {
        invincibleUntil = Mathf.Max(invincibleUntil, Time.time + time);
    }

    private void Update()
    {
        if (currentSet == null || characterRenderer == null) return;

        if (invincible) characterRenderer.sprite = currentSet.InvincibleSprite;
        else if (Buffed) characterRenderer.sprite = currentSet.BuffedSprite;
        else characterRenderer.sprite = currentSet.NormalSprite;

        // Parpadeo mientras es invencible, para que se note que no le pegan.
        Color c = characterRenderer.color;
        c.a = invincible && Mathf.Repeat(Time.time * 12f, 1f) < 0.5f ? 0.35f : 1f;
        characterRenderer.color = c;
    }

    public override void OnHit(DamageData data)
    {
        if (invincible) return;
        base.OnHit(data);

        EventManager<GameEvent>.Publish<int>(GameEvent.DamagePlayer, healtcomponent.CurrentHealth);

        // Segundos de gracia después de un golpe (como en Touhou).
        if (healtcomponent.CurrentHealth > 0) SetInvincible(invincibilityAfterHit);
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

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void deadHandling()
    {
        StopAllCoroutines(); // deja de disparar al morir
        Gamemanager.instance.PlayerDied();
    }
}
