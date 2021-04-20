using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject explosionParticleVFX;

    [SerializeField]
    float shotCounter;

    [SerializeField]
    GameObject enemyLaser;

    EnemySpawner enemySpawner;

    float minShotTime = 0.2f;

    float maxShotTime = 3f;

    WaveConfig waveConfig;

    List<Transform> waypoints;

    private int health = 100;

    int wayPointIndex = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        shotCounter = Random.Range(minShotTime, maxShotTime);
        transform.position = waypoints[wayPointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Fire();
    }

    private void Fire()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0)
        {
            GameObject newEnemyShot = Instantiate(enemyLaser, transform.position + new Vector3(0, -0.65f, 0), Quaternion.identity);
            newEnemyShot.transform.parent = enemySpawner.CleanUpContainer();
            shotCounter = Random.Range(minShotTime, maxShotTime);
        }
    }

    public void SetWaveConfig(WaveConfig pathConfig)
    {
        waveConfig = pathConfig;
        waypoints = waveConfig.GetWaypoints();
    }

    private void Movement()
    {
        if (wayPointIndex <= waypoints.Count - 1)
        {
            var targetWaypoint = waypoints[wayPointIndex].position;
            var frameSpeed = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, frameSpeed);

            if (transform.position == targetWaypoint)
            {
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damaging = other.GetComponent<DamageDealer>();

        if(damaging)
        {
            health -= damaging.Damage();

            if (health <= 0)
            {
                GameObject newVFX = Instantiate(explosionParticleVFX, transform.position, Quaternion.identity);
                newVFX.transform.parent = enemySpawner.CleanUpContainer();
                Destroy(newVFX, 1f);
                Destroy(this.gameObject);
            }
        }
    }
}
