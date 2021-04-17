using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField]
    List<Transform> waypoints;

    float moveSpeed = 2f;

    int wayPointIndex = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[wayPointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (wayPointIndex <= waypoints.Count-1)
        {
            var targetWaypoint = waypoints[wayPointIndex].position;
            var frameSpeed = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, frameSpeed);

            if(transform.position == targetWaypoint)
            {
                wayPointIndex++;
            }
        }
        else
        {
            wayPointIndex = 0;
        }
    }
}
