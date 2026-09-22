using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Entity, ISpawnable
{
    [SerializeField] Transform muzzlePos;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] int bulletPoolSize = 20;
    [SerializeField] int scoreValue = 100;
    [SerializeField] List<AttackPatterns> attackPatterns = new List<AttackPatterns> ();
    BulletPool bulletPool;
    BaseEnemyMovement enemyMovement;
    BaseEnemyAttack enemyAttack;
    //bandAid delete later
    Spiralpattern spiral;
    Diagonal3 diagonal;

    bool shootReady = true;
    private void Awake()
    {
        healtcomponent.onDead += Die;
        BandAidSolution();
    }
    public void OnCreated(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
        BandAidSolution();
        bulletPool = new BulletPool(bulletPrefab, bulletPoolSize);
        enemyMovement = new BaseEnemyMovement(transform);
        enemyAttack = new BaseEnemyAttack(bulletPool, muzzlePos, this, attackPatterns);
        shootReady = true;
    }
    void Update()
    {
        enemyMovement.move();
        if (shootReady)
        {
            StartCoroutine(ShootSequence());
        }
        enemyAttack.UpdateTimer();
    }
    IEnumerator ShootSequence()
    {
        shootReady = false;
        enemyAttack.Shoot();
        yield return new WaitForSeconds(0.2f);
        shootReady = true;
    }
    public override void OnHit(DamageData data)
    {
        if (data.FromWho is Player) base.OnHit(data);

    }
    public void BandAidSolution()
    {
        //band aid solution to the attacks pattern, ask the prof how to solve it
        spiral = new Spiralpattern();
        spiral.Init(bulletPool, muzzlePos, this);
        diagonal = new Diagonal3();
        diagonal.Init(bulletPool, muzzlePos, this);
        attackPatterns.Add(spiral);
        attackPatterns.Add(diagonal);
    }
    public override void Die()
    {
        EventManager<GameEvent>.Publish<int>(GameEvent.AddPoints, scoreValue);
        base.Die();
    }
}
