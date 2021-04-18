using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    WaveConfig waveConfig;

    List<Transform> waypoints;

    private int health = 100;

    int wayPointIndex = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[wayPointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
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
                Destroy(this.gameObject);
            }
        }
    }
}
