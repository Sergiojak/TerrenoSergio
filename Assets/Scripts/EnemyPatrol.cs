using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;
    float enemyMovementSpeed = 1f;

    Transform enemy;
    void Start()
    {
        enemy = GetComponent<Transform>();
        transform.position = waypoint1.position;
    }

    void Update()
    {
        transform.position += waypoint2.position * Time.deltaTime * enemyMovementSpeed;
    }
}
