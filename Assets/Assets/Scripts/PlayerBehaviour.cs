using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    Rigidbody playerRb;

    public float playerSpeed = 40f;
    public float playerNormalSpeed = 40f;
    public float maximumSpeed = 60f;
    public float minimumSpeed = 20f;
    public float incrementSpeed = 10f;

    private float respawnTimer = 0f;
    public bool respawnTimerActivation;
    [SerializeField]
    GameObject playerCrosshair;

    public Vector2 turn;
    public float turnSensitivity = 0.5f;

    public AudioSource audioMovement;
    bool needAudioMovement = false;

    public AudioSource audioExplosion;
    bool needAudioExplosion = false;

    [SerializeField]
    GameObject turboVFX1;
    [SerializeField]
    GameObject turboVFX2;

    public ParticleSystem playerExplosionVFX;

    public EndGameScreen gameEnding;

    //PowerUp SpeedBoost
    public float playerPowerUpSpeed = 40f;
    bool needPlayerBoost;
    float playerBoostDuration;

    public bool maxSpeedReached = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        respawnTimerActivation = false;
        needPlayerBoost = false;

        if (needAudioMovement == true)
        {
            audioMovement.Play();
        }
    }
    void Update()
    {
        if (respawnTimerActivation == false)
        {
            transform.position += transform.forward * playerSpeed * Time.deltaTime;
            turn.x += Input.GetAxis("Mouse X") * turnSensitivity;
            turn.y += Input.GetAxis("Mouse Y") * turnSensitivity;
            transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        }

        if (Input.GetKey(KeyCode.W)) //Aceleración
        {
            if (playerSpeed <= maximumSpeed && maxSpeedReached == false) //si su velocidad es menor que la máxima y no la ha alcanzado (con el powerup por ejemplo)
            {
                playerSpeed += incrementSpeed * Time.deltaTime; //pues que aumente
            }
            if (playerSpeed >= maximumSpeed) //si su velocidad es mayor que la maxima
            {
                maxSpeedReached = true; //indicamos que ha llegado al max
                if(maxSpeedReached == true)
                {
                    playerSpeed -= incrementSpeed * Time.deltaTime; //asi que se reduzca hasta la maxima (si la tecla W está pulsada)
                    maxSpeedReached = false;
                }
            }
        }
        else //Vuelta a velocidad normal
        {
            if (playerSpeed >= playerNormalSpeed) //si se suelta la W pues que vuelva a su velocidad normal
            {
                playerSpeed -= incrementSpeed * Time.deltaTime;
            }
        }

        //Power-Up boost Speed
        if (needPlayerBoost == true)
        {
            playerSpeed += playerPowerUpSpeed * Time.deltaTime;
            playerBoostDuration += Time.deltaTime;
        }
        if (playerBoostDuration >= 1f) //duración del boost
        {
            needPlayerBoost = false;
            playerBoostDuration = 0f;
            if (playerSpeed >= playerNormalSpeed) //vuelve a la velocidad normal
            {
                playerSpeed -= playerPowerUpSpeed * Time.deltaTime;
            }
        }
       

        if (Input.GetKey(KeyCode.S)) //Freno
        {
            if (playerSpeed >= minimumSpeed)
            {
                playerSpeed -= incrementSpeed * Time.deltaTime;
            }
        }
        else //Vuelta a velocidad normal
        {
            if (playerSpeed <= playerNormalSpeed)
            {
                playerSpeed += incrementSpeed * Time.deltaTime;
            }
        }
        if (respawnTimerActivation == true)
        {
            respawnTimer += Time.deltaTime;

            if (respawnTimer >= 3)
            {
                respawnTimer = 0;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain") || collision.gameObject.CompareTag("Enemy"))
        {
            respawnTimerActivation = true; //activa timer 
            Destroy(playerCrosshair); //desactiva el canvas del crosshair
            playerRb.GetComponent<Renderer>().enabled = false; //desactiva jugador (desactiva la malla porque si desactivo con Destroy o SetActive deja de funcionar)
            playerRb.isKinematic = true; //deja estático el player para que la cámara no se vaya (junto con el primer if del update)

            needAudioMovement = false;
            if (needAudioMovement == false)
            {
                audioMovement.Stop();
            }

            needAudioExplosion = true;
            if (needAudioExplosion == true)
            {
                audioExplosion.Play();
            }
            
            playerExplosionVFX.Play();
            playerExplosionVFX.transform.position = transform.position;

            Destroy(turboVFX1);
            Destroy(turboVFX2);

            gameEnding.LoseGame();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Power-Up"))
       {
            needPlayerBoost = true;
       }
    }
}