using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //NOTA: EL SCRIPT DE DISPARO (PlayerShotBehaviour) ESTÁ EN LA TORRETA DEL JUGADOR (playerTurret, hijo del Player)

    [SerializeField]
    GameObject player;
    Rigidbody playerRb;

    public float playerSpeed = 40f;
    public float playerNormalSpeed = 40f;
    public float playerMaximumSpeed = 60f;
    public float playerMinimumSpeed = 20f;
    public float playerIncrementSpeed = 10f;

    public bool playerIsDead;
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
        playerIsDead = false;
        needPlayerBoost = false;

        if (needAudioMovement == true)
        {
            audioMovement.Play();
        }
    }
    void Update()
    {
        if (playerIsDead == false)
        {
            transform.position += transform.forward * playerSpeed * Time.deltaTime;
            turn.x += Input.GetAxis("Mouse X") * turnSensitivity;
            turn.y += Input.GetAxis("Mouse Y") * turnSensitivity;
            transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        }

        if (Input.GetKey(KeyCode.W)) //Aceleración
        {
            if (playerSpeed <= playerMaximumSpeed && maxSpeedReached == false) //si su velocidad es menor que la máxima y no la ha alcanzado (con el powerup por ejemplo)
            {
                playerSpeed += playerIncrementSpeed * Time.deltaTime; //pues que aumente
            }
            if (playerSpeed >= playerMaximumSpeed) //si su velocidad es mayor que la maxima
            {
                maxSpeedReached = true; //indicamos que ha llegado al max
                if(maxSpeedReached == true)
                {
                    playerSpeed -= playerIncrementSpeed * Time.deltaTime; //asi que se reduzca hasta la maxima (si la tecla W está pulsada)
                    maxSpeedReached = false;
                }
            }
        }
        else //Vuelta a velocidad normal
        {
            if (playerSpeed >= playerNormalSpeed) //si se suelta la W pues que vuelva a su velocidad normal
            {
                playerSpeed -= playerIncrementSpeed * Time.deltaTime;
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
            if (playerSpeed >= playerMinimumSpeed)
            {
                playerSpeed -= playerIncrementSpeed * Time.deltaTime;
            }
        }
        else //Vuelta a velocidad normal
        {
            if (playerSpeed <= playerNormalSpeed)
            {
                playerSpeed += playerIncrementSpeed * Time.deltaTime;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain") || collision.gameObject.CompareTag("Enemy"))
        {
            playerIsDead = true; //activa bool para deshabilitar movimiento
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

    //NOTA: EL SCRIPT DE DISPARO (PlayerShotBehaviour) ESTÁ EN LA TORRETA DEL JUGADOR (playerTurret, hijo del Player)
}