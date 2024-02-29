using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform[] waypoints;
    int currentWaypoint;
    public float enemyMovementSpeed = 40f;

    public float rotationSpeed = 5f;

    public AudioSource audioEnemyExplosion;
    bool needAudioEnemyExplosion;

    public ParticleSystem enemyExplosionVFX;


    void Start()
    {
        currentWaypoint = 0;
        needAudioEnemyExplosion = false;
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
            enemyExplosionVFX.Play();
            enemyExplosionVFX.transform.position = transform.position;

            needAudioEnemyExplosion = true;
            if(needAudioEnemyExplosion == true)
            {
                audioEnemyExplosion.Play();
            }    
        }
    }
}
