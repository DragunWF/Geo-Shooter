using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject enemyPrefab;
    private List<Transform> spawnPoints;

    private float maxSpawnInterval = 12.5f;
    private float minSpawnInterval = 6.25f;

    private void Awake()
    {
        enemyPrefab = Resources.Load("Prefabs/Enemy") as GameObject;

        const int pointIndexLimit = 8;
        for (int i = 0; i < pointIndexLimit; i++)
        {
            GameObject point = GameObject.Find(string.Format("Point {0}", i));
            spawnPoints.Add(point.transform);
        }
    }

    private void Start()
    {

    }

    private float GetRandomSpawnInterval()
    {
        List<float> spawnIntervals = new List<float>();
        float startInterval = minSpawnInterval;

        while (startInterval < maxSpawnInterval)
        {
            startInterval += 0.1f;
            spawnIntervals.Add(startInterval);
        }

        return spawnIntervals[Random.Range(0, spawnIntervals.Count - 1)];
    }

    private IEnumerator SpawnEnemy()
    {
        float spawnInterval = GetRandomSpawnInterval();
        yield return new WaitForSeconds(spawnInterval);
    }
}
