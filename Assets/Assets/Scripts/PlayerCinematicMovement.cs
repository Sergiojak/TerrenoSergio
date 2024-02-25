using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinematicMovement : MonoBehaviour
{
    public float playerSpeedCinematic = 20f;


    void Start()
    {
        
    }

    void Update()
    {
        transform.position += transform.forward * playerSpeedCinematic * Time.deltaTime;
    }
}
