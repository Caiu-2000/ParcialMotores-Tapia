using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //--------------Enemy Factories---------------//
    [SerializeField] EnemyFactory[] enemyFactory;
    [SerializeField] EnemyFactory tapia;
    [SerializeField] EnemyFactory yuuki;
    [SerializeField] EnemyFactory riquelme;
    [SerializeField] Transform[] spawnPositions;
    //-----------------Bullets-------------------//
    public static BulletPool mainPool;
    [SerializeField]  Bullet bullet;
    //-----------------Settings------------------//
    [SerializeField] float spawnTimer = 7.5f;
    [SerializeField] float minSpawnTimer = 2f;
    [SerializeField, Range(60, 120)] float secondsToReachMinSpawnTimer = 120f;
    [SerializeField] int killsToSpawnBoss;
    [SerializeField] int killsToSpawnMidBoss; //GET THE REJUVENATOR
    //---------------Private vars--------------//
    List<Enemy> enemiesSpawned = new List<Enemy>();
    float startInterval;
    float elapsed;
    float cooldwon;
    bool spawnReady = true;
    bool bossSpawned;
    bool midBossSpawned;
    int kills;
    
    int NextTarget => midBossSpawned ? killsToSpawnBoss : killsToSpawnMidBoss;
    bool EnoughAliveToReachTarget => kills + enemiesSpawned.Count >= NextTarget;
    
    private void Awake()
    {
        startInterval = spawnTimer;
        mainPool = new BulletPool(bullet, 500);
    }
    private void OnEnable()
    {
        EventManager<GameEvent>.Subscribe<Enemy>(GameEvent.EnemyKilled, OnEnemyKilled);
        EventManager<GameEvent>.Subscribe<Enemy>(GameEvent.MidBossKilled, MidbossKilled);
    }
    private void OnDisable()
    {
        EventManager<GameEvent>.Unsubscribe<Enemy>(GameEvent.EnemyKilled, OnEnemyKilled);
        EventManager<GameEvent>.Unsubscribe<Enemy>(GameEvent.MidBossKilled, MidbossKilled);
    }
    private void Update()
    {
        elapsed += Time.deltaTime;
        if (bossSpawned) return;
        cooldwon -= Time.deltaTime;
        if(cooldwon > 0) return;
        if (EnoughAliveToReachTarget) return;
        Spawn(enemyFactory[Random.Range(0, enemyFactory.Length)], RandomSpawnPoint());
        cooldwon = CurrentInterval();
    }

    Vector3 RandomSpawnPoint()
    {
        return spawnPositions[Random.Range(0, spawnPositions.Length)].position;
    }
       
    Enemy Spawn(EnemyFactory factory, Vector3 position)
    {
        Enemy enemy = factory.SpawnObject(position, Quaternion.identity);
        enemiesSpawned.Add(enemy);
        RunManager.instance.enemies.Add(enemy);
        return enemy;
    }

    float CurrentInterval()
    {
        float t = Mathf.Clamp01(elapsed/secondsToReachMinSpawnTimer);
        return Mathf.Lerp(startInterval, minSpawnTimer, t);
    }
    void OnEnemyKilled(Enemy enemy)
    {
        kills++;
        enemiesSpawned.Remove(enemy);
        EventManager<GameEvent>.Publish<int>(GameEvent.BossProgress, kills);

        if (!midBossSpawned && kills >= killsToSpawnMidBoss)
        {
            midBossSpawned = true;
            bossSpawned = true;
            var factory = GlobalData.SelectedCharacter == CharacterNames.Cobra ? yuuki : riquelme;
            Spawn(factory, spawnPositions[0].position).SetBoss();
        }
        else if (midBossSpawned && !bossSpawned && kills >= killsToSpawnBoss)
        {
            bossSpawned = true;
            Spawn(tapia, spawnPositions[0].position);
        }
    }
    void MidbossKilled(Enemy enemy)
    {
        bossSpawned = false;

    }
}