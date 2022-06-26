using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject enemyPrefab;
    private List<Transform> spawnPoints;

    private float maxSpawnInterval = 12.5f;
    private float minSpawnInterval = 6.25f;
    private float spawnInterval;

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

    private void Update()
    {

    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnInterval);
    }
}
