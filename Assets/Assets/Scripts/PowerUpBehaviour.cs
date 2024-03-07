using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject minimumXPowerUpSpawnArea;
    [SerializeField]
    GameObject maximumXPowerUpSpawnArea;
    [SerializeField]
    GameObject minimumZPowerUpSpawnArea;
    [SerializeField]
    GameObject maximumZPowerUpSpawnArea;
    [SerializeField]
    GameObject minimumYPowerUpSpawnArea;
    [SerializeField]
    GameObject maximumYPowerUpSpawnArea;

    float minPositionX;
    float maxPositionX;

    float minPositionZ;
    float maxPositionZ;

    float minPositionY;
    float maxPositionY;

    public AudioSource powerUpSoundEffect;

    private void Start()
    {
        minPositionX = minimumXPowerUpSpawnArea.transform.position.x;
        maxPositionX = maximumXPowerUpSpawnArea.transform.position.x;
        minPositionZ = minimumZPowerUpSpawnArea.transform.position.z;
        maxPositionZ = maximumZPowerUpSpawnArea.transform.position.z;

        minPositionY = minimumYPowerUpSpawnArea.transform.position.y;
        maxPositionY = maximumYPowerUpSpawnArea.transform.position.y;

    }

    private void Update()
    {
        transform.Rotate(0f, 0f, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = new Vector3(Random.Range(minPositionX, maxPositionX), Random.Range(minPositionY, maxPositionY), Random.Range(minPositionZ, maxPositionZ));
            powerUpSoundEffect.Play();
        }
    }
}
