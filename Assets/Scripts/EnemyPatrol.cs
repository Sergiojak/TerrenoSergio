using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject waypoint1;
    public GameObject waypoint2;
    public GameObject enemy;

    [SerializeField]
    float enemyMovementSpeed = 20.0f;

    bool enemyIsAtWaypoint1 = false;
    bool enemyIsAtWaypoint2 = true;

    void Start()
    {
      
    }

    void Update()
    {
        if (enemyIsAtWaypoint1 == false)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, waypoint1.transform.position, enemyMovementSpeed * Time.deltaTime);        
            transform.LookAt(waypoint1.transform.position);

            /*Quaternion rotationTarget = Quaternion.LookRotation(enemy.transform.position, waypoint1.transform.position);
            enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, rotationTarget, rotationSpeed * Time.deltaTime);*/

            if (enemy.transform.position == waypoint1.transform.position) 
            { 
                enemyIsAtWaypoint1 = true;
                enemyIsAtWaypoint2 = false;
            }
        }
        if (enemyIsAtWaypoint2 == false)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, waypoint2.transform.position, enemyMovementSpeed * Time.deltaTime);
            transform.LookAt(waypoint2.transform.position);

            /*Quaternion rotationTarget2 = Quaternion.LookRotation(enemy.transform.position, waypoint2.transform.position);
            enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, rotationTarget2, rotationSpeed * Time.deltaTime);*/

            if (enemy.transform.position == waypoint2.transform.position)
            {
                enemyIsAtWaypoint1 = false;
                enemyIsAtWaypoint2 = true;
            }

        }
    }
}
