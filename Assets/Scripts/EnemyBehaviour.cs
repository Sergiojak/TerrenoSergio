using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform[] waypoints;
    int currentWaypoint;
    float enemyMovementSpeed = 40f;

    void Start()
    {
        currentWaypoint = 0;
    }

    void Update()
    {
        if (transform.position != waypoints[currentWaypoint].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, enemyMovementSpeed * Time.deltaTime);
            transform.LookAt(waypoints[currentWaypoint].position);
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
