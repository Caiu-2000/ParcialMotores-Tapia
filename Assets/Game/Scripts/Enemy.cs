using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Enemy : Entity, ISpawnable
{
    [SerializeField] Transform muzzlePos;
    [SerializeField] Bullet bulletPrefab;
 //   [SerializeField] int bulletPoolSize = 20;
    [SerializeField] int scoreValue = 100;
    [SerializeField] List<AttackPatterns> attackPatterns = new List<AttackPatterns> ();
    BulletPool bulletPool;
    BaseEnemyMovement enemyMovement;
    BaseEnemyAttack enemyAttack;
    List<AttackPatterns> runtimePatterns = new List<AttackPatterns> ();


    bool shootReady = true;
    private void Awake()
    {
        healtcomponent.onDead += Die;
        //debug
        OnCreated(muzzlePos.position, Quaternion.identity);
    }
    private void OnEnable()
    {
      //  bulletPool = new BulletPool(bulletPrefab, bulletPoolSize);
    }
    public void OnCreated(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
       
        foreach (AttackPatterns pattern in attackPatterns)
        {
            runtimePatterns.Add(Instantiate(pattern));
        }
        enemyMovement = new BaseEnemyMovement(transform);
        enemyAttack = new BaseEnemyAttack(bulletPool, muzzlePos, this, runtimePatterns);
        shootReady = true;
    }
    protected void Update()
    {
        enemyMovement.move();
        if (shootReady)
        {
            StartCoroutine(ShootSequence());
        }
        enemyAttack.UpdateTimer();
    }
    protected IEnumerator ShootSequence()
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

    public override void Die()
    {
        EventManager<GameEvent>.Publish<int>(GameEvent.AddPoints, scoreValue);
        EventManager<GameEvent>.Publish<int>(GameEvent.EnemyKilled, 1);
        SoundManager.instance.PlayPitched(SoundTypes.EnemyDead);
        base.Die();
    }


}
