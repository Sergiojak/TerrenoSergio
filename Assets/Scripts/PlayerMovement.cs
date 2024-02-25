using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    public float playerSpeed = 40f;
    public float playerNormalSpeed = 40f;
    public float maxSpeed = 60f;
    public float minSpeed = 20f;
    public float incrementSpeed = 10f;

    public Vector2 turn;
    public float turnSensitivity = 0.5f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        transform.position += transform.forward * playerSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            if (playerSpeed <= maxSpeed) 
            {
                playerSpeed += incrementSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (playerSpeed >= playerNormalSpeed)
            {
                playerSpeed -= incrementSpeed * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (playerSpeed >= minSpeed)
            {
                playerSpeed -= incrementSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (playerSpeed <= playerNormalSpeed)
            {
                playerSpeed += incrementSpeed * Time.deltaTime;
            }
        }

        turn.x += Input.GetAxis("Mouse X") * turnSensitivity;
        turn.y += Input.GetAxis("Mouse Y") * turnSensitivity;
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Chocaste, has perdido");
        Object.Destroy(player);

        //añadir excepción de mi propia bala
    }
}