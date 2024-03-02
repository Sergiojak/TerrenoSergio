using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class PowerUpBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject limitMinX;
    [SerializeField]
    GameObject limitMaxX;
    [SerializeField]
    GameObject limitMinZ;
    [SerializeField]
    GameObject limitMaxZ;
    [SerializeField]
    GameObject heightlimit1;
    [SerializeField]
    GameObject heightlimit2;

    float minPositionX;
    float maxPositionX;

    float minPositionZ;
    float maxPositionZ;

    float minHeight;
    float maxHeight;

    public AudioSource powerUpSoundEffect;

    private void Start()
    {
        minPositionX = limitMinX.transform.position.x;
        maxPositionX = limitMaxX.transform.position.x;
        minPositionZ = limitMinZ.transform.position.z;
        maxPositionZ = limitMaxZ.transform.position.z;

        minHeight = heightlimit1.transform.position.y;
        maxHeight = heightlimit2.transform.position.y;

    }

    private void Update()
    {
        transform.Rotate(0f, 0f, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = new Vector3(Random.Range(minPositionX, maxPositionX), Random.Range(minHeight, maxHeight), Random.Range(minPositionZ, maxPositionZ));
            powerUpSoundEffect.Play();
        }
    }
}
