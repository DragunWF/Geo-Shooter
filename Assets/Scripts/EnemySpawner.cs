using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private List<Transform> spawnPoints;
    private GameObject[] enemyPrefabs;
    private GameUI gameUI;

    private const float scaleDifficultyTime = 18.5f;
    private float[] initialSpawnIntervals;

    private const int maxDifficultyLevel = 20;
    private int difficultyLevel = 0;

    private const float minSpawnIntervalLimit = 0.5f;
    private const float maxSpawnIntervalLimit = 1f;
    private float maxSpawnInterval = 8.5f;
    private float minSpawnInterval = 4.25f;

    public int GetMaxDifficultyLevel() { return maxDifficultyLevel; }
    public int GetDifficultyLevel() { return difficultyLevel; }

    private void Awake()
    {
        enemyPrefabs = new GameObject[3] {
            Resources.Load("Prefabs/Enemy [Square]") as GameObject,
            Resources.Load("Prefabs/Enemy [Circle] Variant") as GameObject,
            Resources.Load("Prefabs/Enemy [Triangle] Variant") as GameObject
        };
        spawnPoints = new List<Transform>();
        gameUI = FindObjectOfType<GameUI>();

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

        minSpawnInterval = initialSpawnIntervals[0] - difficultyLevel * 0.45f;
        maxSpawnInterval = initialSpawnIntervals[1] - difficultyLevel * 0.45f;

        if (minSpawnInterval < minSpawnIntervalLimit)
            minSpawnInterval = minSpawnIntervalLimit;
        if (maxSpawnInterval < maxSpawnIntervalLimit)
            maxSpawnInterval = maxSpawnIntervalLimit;

        gameUI.UpdateDifficultyText(difficultyLevel);
        if (difficultyLevel < maxDifficultyLevel)
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
        const float timeDelayBeforeStart = 2.25f;
        yield return new WaitForSeconds(timeDelayBeforeStart);

        while (true)
        {
            GameObject chosenEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            float spawnInterval = GetRandomSpawnInterval() / 2;
            yield return new WaitForSeconds(spawnInterval);

            Vector2 chosenPosition = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
            Instantiate(chosenEnemy, chosenPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
