using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float spawnDelay = 0.5f;
    [SerializeField] float randomSpawnFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();

        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }

    public float GetSpawnDelay()
    {
        return spawnDelay;
    }

    public float GetRandomFactor()
    {
        return randomSpawnFactor;
    }

    public int GetEnemiesInWave()
    {
        return numberOfEnemies;
    }

    public float GetEnemyMoveSpeed()
    {
        return moveSpeed;
    }

}
