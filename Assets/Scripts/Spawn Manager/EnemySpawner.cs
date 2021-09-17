using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Config
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave;
    [SerializeField] bool looping;

    // Start has been turned into a Coroutine that loops endlessly. This allows the same series of waves to continue to spawn forever.
    IEnumerator Start()
    {
        // Starts the SpawnAllWaves() Coroutine and loops it when it finishes
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    //This Coroutine spawns all of the enemies in the wave that it is passed
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++ )
        {
            //Creates an instances of the enemy based on the parameters passed.
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);

            //Sets the wave config in EnemyPathing.cs
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            //Yields based on the time between spawns in the waveconfig
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    // This Coroutine spawns all of the waves assigned to the Enemy Spawner object
    private IEnumerator SpawnAllWaves()
    {
        // Sets waveCount to the starting wave and INCRIMENTS it on every pass. Thus accessing all of the waves stored in the object
        for (int waveCount = startingWave; waveCount < waveConfigs.Count; waveCount++)
        {
            // Sets the current wave and updates it
            var currentWave = waveConfigs[waveCount];

            // Waits until all the enemies in this wave have been spawned before starting the next. Passes currentWave to SpawnAllEnemiesInWave()
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
}
