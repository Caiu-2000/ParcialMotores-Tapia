using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyFactory[] enemyFactory;
    [SerializeField] EnemyFactory tapia;
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] float spawnTimer = 7.5f;
    [SerializeField] int killsToSpawnBoss;
    float spawnTimerNotModified;
    [SerializeField] float minSpawnTimer = 2f;
    [SerializeField, Range(60, 120)] float secondsToReachMinSpawnTimer = 120f;
    bool spawnReady = true;
    List<Enemy> enemiesSpawned = new List<Enemy>();
    bool bossSpawned;
    int kills;

    public static BulletPool mainPool;
    [SerializeField]
    Bullet bullet;
    private void Awake()
    {
        spawnTimerNotModified = spawnTimer;
    }
    private void OnEnable()
    {
        EventManager<GameEvent>.Subscribe<int>(GameEvent.EnemyKilled, OnEnemyKilled);
    }
    private void OnDisable()
    {
        EventManager<GameEvent>.Unsubscribe<int>(GameEvent.EnemyKilled, OnEnemyKilled);
    }
    private void Update()
    {
        if (bossSpawned) return;
        if (spawnReady == false) return;
        StartCoroutine(SpawnEnemy(spawnTimer));
        LinearRampUp();
    }
    private void LinearRampUp()
    {
        float linearFunctionSlope = (minSpawnTimer - spawnTimerNotModified) / secondsToReachMinSpawnTimer;// M = (Y2-Y1)/(X2-X1)
        float possibleSpawnTimer = linearFunctionSlope * Time.time + spawnTimerNotModified; //Y = mx+b
        spawnTimer = Mathf.Max(minSpawnTimer, possibleSpawnTimer);
    }
    IEnumerator SpawnEnemy(float spawnTime)
    {
        int random = enemyFactory.Length == 1 ? 0 : Random.Range(0, enemyFactory.Length);
        spawnReady = false;
        int spawnPoint = Random.Range(0, spawnPositions.Length);
        Enemy enemy = enemyFactory[random].SpawnObject(spawnPositions[spawnPoint].position, Quaternion.identity);
        enemiesSpawned.Add(enemy);
    
        yield return new WaitForSeconds(spawnTime);
        spawnReady = true;
    }
    private void OnEnemyKilled(int x)
    {
        kills++;
        EventManager<GameEvent>.Publish<int>(GameEvent.BossProgress, kills);
        if (kills >= killsToSpawnBoss && bossSpawned == false)
        {
            bossSpawned = true;
            enemiesSpawned.Add(tapia.SpawnObject(spawnPositions[0].position, Quaternion.identity));
        }
    }

    private void Start()
    {
        mainPool = new BulletPool(bullet, 40);
    }
}
