using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombBehaviour : MonoBehaviour
{
    BombPool bombPool;
    public GameObject playerBomb;

    public bool bombUsed = false;
    public float bombTimer = 0;

    public AudioSource bombFalling;

    void Start()
    {
        bombPool = BombPool.instance;
    }

    void Update()
    {
        if (bombUsed == false)
        {
            if (Input.GetButtonUp("Fire2"))
            {
                //Creamos de la pool el objeto (usando la función que saca la bala de la pila,  script BulletPool)
                GameObject bomb = bombPool.ObtenerObjeto();
                //que su posición sea la misma que el cañón/torreta del jugador
                bomb.transform.position = playerBomb.transform.position;
                bomb.GetComponent<Rigidbody>().velocity = Vector3.forward;
                bombFalling.Play();
                bombUsed = true;
            }
        }
        else if (bombUsed == true)
        {
            bombTimer += Time.deltaTime;
            if (bombTimer >= 5f)
            {
                bombUsed = false;
                bombTimer = 0f;
            }
        }
    }
}
