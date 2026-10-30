using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    public List<GameObject> enemiesPrefab = new List<GameObject>();
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    public int spawnAttemptsPerNight;
    public bool debounce;
    public float minEnemyDistance;
    public float minSpawnRadius;
    public float maxSpawnRadius;
    public float minSpawnDelay;
    public float maxSpawnDelay;

    private void Awake() {
        Instance = this;
    }

    public void SpawnEnemies() {
        StartCoroutine(SpawnCorountine());
    }


    public IEnumerator SpawnCorountine()
    {
        debounce = true;

        for (int i = 0; i <= spawnAttemptsPerNight * CycleManager.Instance.cycleNumber; i++)
        {
            if (Random.Range(1, 10) > 4)
                continue;

            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            float distance = Random.Range(minSpawnRadius, maxSpawnRadius);

            Vector2 randomPosition = direction * distance;

            bool tooClose = false;

            foreach (GameObject enemy in spawnedEnemies)
            {
                if (enemy == null)
                    continue;

                if (Vector2.Distance(randomPosition, enemy.transform.position) < minEnemyDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                int randomIndex = Random.Range(0, enemiesPrefab.Count);
                GameObject randomEnemy = enemiesPrefab[randomIndex];
                Vector3 spawnPosition = new Vector3(randomPosition.x, randomPosition.y, 0);
                GameObject spawnedEnemy = Instantiate(randomEnemy, spawnPosition, Quaternion.identity, transform);

                spawnedEnemies.Add(spawnedEnemy);
            }

            // Wait a randomized amount of time before the next attempt
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }

        debounce = false;

        if (spawnedEnemies.Count == 0)
            CycleManager.Instance.ToggleDaytime(true);
    }


    public void RegisterDeath(GameObject enemyObject) {
        int index = spawnedEnemies.IndexOf(enemyObject);

        if (index == -1)
            return;

        spawnedEnemies.RemoveAt(index);

        if (spawnedEnemies.Count <= 0 && !debounce) {
            CycleManager.Instance.ToggleDaytime(true);
        }
    }
}
