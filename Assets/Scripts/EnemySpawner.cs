using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private List<Transform> spawnPoints;
    private GameObject[] enemyPrefabs;

    private const float scaleDifficultyTime = 10.5f;
    private float[] initialSpawnIntervals;

    private const int maxDifficultyLevel = 20;
    private int difficultyLevel = 0;

    private const float minSpawnIntervalLimit = 0.25f;
    private const float maxSpawnIntervalLimit = 0.5f;
    private float maxSpawnInterval = 8.5f;
    private float minSpawnInterval = 4.25f;

    private void Awake()
    {
        enemyPrefabs = new GameObject[2] {
            Resources.Load("Prefabs/Enemy [Square]") as GameObject,
            Resources.Load("Prefabs/Enemy [Circle]") as GameObject
        };
        spawnPoints = new List<Transform>();

        const int pointIndexLimit = 15;
        for (int i = 0; i <= pointIndexLimit; i++)
        {
            GameObject point = GameObject.Find(string.Format("Point ({0})", i));
            spawnPoints.Add(point.transform);
        }

        initialSpawnIntervals = new float[2] { minSpawnInterval, maxSpawnInterval };
    }

    private void Start()
    {
        ScaleDifficulty();
        StartCoroutine(SpawnEnemies());
    }

    private void ScaleDifficulty()
    {
        difficultyLevel++;

        minSpawnInterval = initialSpawnIntervals[0] - difficultyLevel * 0.25f;
        maxSpawnInterval = initialSpawnIntervals[1] - difficultyLevel * 0.25f;

        if (minSpawnInterval < minSpawnIntervalLimit)
            minSpawnInterval = minSpawnIntervalLimit;
        if (maxSpawnInterval < maxSpawnIntervalLimit)
            maxSpawnInterval = maxSpawnIntervalLimit;

        // For testing purposes (Delete later)
        Debug.Log(string.Format("MinSpawnInterval: {0} MaxSpawnInterval: {1}", minSpawnInterval, maxSpawnInterval));
        Debug.Log(string.Format("Difficulty Level: {0}", difficultyLevel));

        Invoke("ScaleDifficulty", scaleDifficultyTime);
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

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            GameObject chosenEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length - 1)];

            float spawnInterval = GetRandomSpawnInterval() / 2;
            yield return new WaitForSeconds(spawnInterval);

            Vector2 chosenPosition = spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position;
            Instantiate(chosenEnemy, chosenPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
