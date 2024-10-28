using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;        // Assign your enemy prefab in the Inspector
    public Transform spawnPoint;          // Assign the spawn point in the Inspector
    public int totalEnemiesToSpawn = 10;  // Total number of enemies to spawn
    private int enemiesSpawned = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < totalEnemiesToSpawn)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            enemiesSpawned++;

            float spawnInterval = Random.Range(5f, 10f);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}