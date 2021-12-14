using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWayPoint = 0;

    [SerializeField] private float speed = 1f;

    void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWayPoint].transform.position) < .1f)
        {
            if (currentWayPoint < waypoints.Length - 1)
            {
                currentWayPoint++;
            }
            else
            {
                currentWayPoint = 0;
            }
        } 
        
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWayPoint].transform.position,
            speed * Time.deltaTime);
    }
}
