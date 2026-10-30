using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName = "Wave Malam";
        public GameObject enemyPrefab;
        public int count = 5;
        public float rate = 1f;
    }

    [Header("Wave Settings")]
    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;

    [Header("Day/Night Integration")]
    public bool isNight = false;

    private int currentWaveIndex = 0;
    private float waveCountdown;
    private bool isSpawning = false;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (!isNight) return;

        if (waveCountdown <= 0f)
        {
            if (!isSpawning && currentWaveIndex < waves.Length)
            {
                StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        isSpawning = true;
        Debug.Log("Malam Tiba! Memulai " + _wave.waveName);

        for (int i = 0; i < _wave.count; i++)
        {
            if (!isNight) yield break;

            SpawnEnemy(_wave.enemyPrefab);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        currentWaveIndex++;
        waveCountdown = timeBetweenWaves;
        isSpawning = false;
    }

    void SpawnEnemy(GameObject _enemy)
    {
        if (spawnPoints.Length == 0) return;
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }

    public void SetNightState(bool nightStatus)
    {
        isNight = nightStatus;
    }
}