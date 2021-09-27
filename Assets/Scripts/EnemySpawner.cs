using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Config
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            //Created a temp variable that stores a waveconfig from the list of waveconfigs
            var currentWave = waveConfigs[waveIndex];

            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    //Coroutine that spawns all of the enemies in the wave. Requires the current wave config to be passed in
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        //Iterates once for each enemy specified in the wave config
        for (int enemyCount = 0; enemyCount < waveConfig.GetEnemiesInWave(); enemyCount++)
        {
            //Creates a temp variable to reference the new enemy that is being instantiated
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);

            //Uses the referece to call a public method and pass the waveConfig to the new enemy
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            //Pauses for the spawn delay specified in the waveConfig Scriptable Object before spawning a new enemy
            yield return new WaitForSeconds(waveConfig.GetSpawnDelay());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
