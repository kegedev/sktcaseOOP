using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
public class EnemyWaveController : MonoBehaviour
{
    [Header("Wave Settings")]
    public int initialEnemyCount = 5;
    public int enemiesPerWaveIncrement = 2; 
    public float timeBetweenWaves = 10f; 


    [Header("Enemy Prefabs")]
    public GameObject[] enemyPrefabs;

    private int currentWave = 0;
    private bool isSpawning = false;

    private List<Transform> enemies = new List<Transform>();

    void Start()
    {
        ActionManager.EnemyKilled += OnEnemyKilled;
        ActionManager.GetClosestEnemy += GetClosestEnemy;
        StartCoroutine(WaveSpawner());
    }

    private void OnEnemyKilled(Enemy enemy)
    {
        if(enemies.Contains(enemy.transform))
        {
            enemies.Remove(enemy.transform);
            Destroy(enemy.gameObject);
        }
    }

    IEnumerator WaveSpawner()
    {
        while (true)
        {
            currentWave++;

            int enemyCount = initialEnemyCount + (currentWave - 1) * enemiesPerWaveIncrement;

            SpawnEnemies(enemyCount);

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnEnemies(int count)
    {
        Vector3 spawnPosition = MapBorderChecker.GetRandomPointInMap();
        for (int i = 0; i < count; i++)
        {
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            spawnPosition += new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));

            enemies.Add(Instantiate(enemyPrefab, spawnPosition, Quaternion.identity).transform);
        }
    }

    private Enemy GetClosestEnemy(Vector3 segmentPos)
    {
        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            float distance = Vector3.Distance(enemy.position, segmentPos);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.GetComponent<Enemy>();
            }
        }

        return closestEnemy;
    }
  
}