using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    List<WaveConfig> waveConfigs;

    private Disposable disposable;

    private GameObject enemy;

    private Transform spawnPosition;

    private int enemiesCount;

    private float spawnGap;

    // Start is called before the first frame update
    void Start()
    {
        disposable = FindObjectOfType<Disposable>();
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemy(int waveIndex)
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            GameObject newEnemy = Instantiate(enemy, spawnPosition.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfigs[waveIndex]);
            newEnemy.transform.parent = disposable.transform;
            yield return new WaitForSeconds(spawnGap);
        }
    }

    IEnumerator SpawnWave()
    {
        for (int waveIndex = 0; waveIndex < waveConfigs.Count; waveIndex++)
        {
            enemy = waveConfigs[waveIndex].GetEnemyPrefab();
            spawnPosition = waveConfigs[waveIndex].GetWaypoints()[0];
            enemiesCount = waveConfigs[waveIndex].GetNumberOfEnemies();
            spawnGap = waveConfigs[waveIndex].GetTimeBetweenSpawns();
            yield return StartCoroutine(SpawnEnemy(waveIndex));
        }
        StartCoroutine(SpawnWave());
    }
}
