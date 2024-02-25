using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    Rigidbody rigidbody;

    public float playerSpeed;
    public float playerNormalSpeed = 40f;
    public float maximumSpeed = 60f;
    public float minimumSpeed = 20f;
    public float incrementSpeed = 10f;

    [SerializeField]
    GameObject respawnPoint;
    private float respawnTimer = 0f;
    public bool respawnTimerActivation;
    [SerializeField]
    GameObject playerCrosshair;

    public Vector2 turn;
    public float turnSensitivity = 0.5f;

    public AudioSource audioMovement;
    bool needAudioMovement = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        respawnTimerActivation = false;
        playerSpeed = 40f;

        if (needAudioMovement == true)
        {
            audioMovement.Play();
        }
    }
    void Update()
    {
        transform.position += transform.forward * playerSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            if (playerSpeed <= maximumSpeed) 
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
            if (playerSpeed >= minimumSpeed)
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
        if (respawnTimerActivation == true)
        {
            respawnTimer += Time.deltaTime;
            playerSpeed = 0f;
            Debug.Log(respawnTimer);
            if (respawnTimer >= 2)
            {
                respawnTimer = 0;
                respawnTimerActivation = false;
                Respawn();
            }
        }

        turn.x += Input.GetAxis("Mouse X") * turnSensitivity;
        turn.y += Input.GetAxis("Mouse Y") * turnSensitivity;
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain") || collision.gameObject.CompareTag("Enemy"))
        {
            respawnTimerActivation = true;
            rigidbody.isKinematic = true;  //desactiva las físicas
            rigidbody.GetComponent<Renderer>().enabled = false; //desactiva la malla para que no se vea
            playerCrosshair.SetActive(false); //desactiva el canvas del crosshair
            needAudioMovement = false;
            if (needAudioMovement == false)
            {
                audioMovement.Stop();
            }
            /*Explosion.Play();
            Explosion.transform.position = transform.position;*/
        }
    }
    private void Respawn()
    {
        playerSpeed = 40f;
        transform.position = respawnPoint.transform.position;
        transform.rotation = respawnPoint.transform.rotation;
        gameObject.SetActive(true);
        rigidbody.isKinematic = false;
        rigidbody.GetComponent<Renderer>().enabled = true;
        playerCrosshair.SetActive(true);
        needAudioMovement = true;
        if (needAudioMovement == true)
        {
            audioMovement.Play();
        }
    }
}