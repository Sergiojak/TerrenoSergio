using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform[] waypoints;
    int currentWaypoint;
    float enemyMovementSpeed = 40f;

    public float rotationSpeed = 5f;

    void Start()
    {
        currentWaypoint = 0;
    }

    void Update()
    {
        if (transform.position != waypoints[currentWaypoint].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, enemyMovementSpeed * Time.deltaTime);
            //rotar hacia waypoint
            Vector3 waypointLocation = (waypoints[currentWaypoint].position - transform.position).normalized;
            Quaternion waypointTargetRotation = Quaternion.LookRotation(waypointLocation);
            transform.rotation = Quaternion.Lerp(transform.rotation, waypointTargetRotation, rotationSpeed * Time.deltaTime);
        }
        else 
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        { 
            Destroy(gameObject);
        }
    }
}
