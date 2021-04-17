using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    List<WaveConfig> waveConfigs;

    private Disposable disposable;

    private int waveIndex = 0;

    private GameObject enemy;

    private Transform spawnPosition;

    private int enemiesCount;

    private float spawnGap;

    // Start is called before the first frame update
    void Start()
    {
        disposable = FindObjectOfType<Disposable>();
        SpawnNextWave();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            GameObject newEnemy = Instantiate(enemy, spawnPosition.position, Quaternion.identity);
            newEnemy.transform.parent = disposable.transform;
            yield return new WaitForSeconds(spawnGap);
        }
        waveIndex++;
        SpawnNextWave();
    }

    private void SpawnNextWave()
    {
        if(waveIndex <= waveConfigs.Count - 1)
        {
            enemy = waveConfigs[waveIndex].GetEnemyPrefab();
            spawnPosition = waveConfigs[waveIndex].GetWaypoints()[0];
            enemiesCount = waveConfigs[waveIndex].GetNumberOfEnemies();
            spawnGap = waveConfigs[waveIndex].GetTimeBetweenSpawns();
            StartCoroutine(SpawnEnemy());
        }
        else
        {
            waveIndex = 0;
            SpawnNextWave();
        }
    }
}
