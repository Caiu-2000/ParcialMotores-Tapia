using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyFactory enemyFactory;
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] float spawnTimer = 7.5f;
    float spawnTimerNotModified;
    [SerializeField] float minSpawnTimer = 2f;
    [SerializeField, Range(60, 120)] float secondsToReachMinSpawnTimer = 120f;
    bool spawnReady = true;
    List<Enemy> enemiesSpawned = new List<Enemy>();
    private void Awake()
    {
        spawnTimerNotModified = spawnTimer;
    }
    private void Update()
    {
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
        spawnReady = false;
        int spawnPoint = Random.Range(0, spawnPositions.Length);
        Enemy enemy = enemyFactory.SpawnObject(spawnPositions[spawnPoint].position, Quaternion.identity);
        enemiesSpawned.Add(enemy);
        yield return new WaitForSeconds(spawnTime);
        spawnReady = true;
    }
}
